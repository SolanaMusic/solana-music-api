using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SolanaMusicApi.Application.Services.PaymentServices.TransactionService;
using SolanaMusicApi.Application.Services.SubscriptionServices.SubscriptionService;
using SolanaMusicApi.Application.Services.TrackServices.TracksService;
using SolanaMusicApi.Application.Services.UserServices.UserProfileService;
using SolanaMusicApi.Domain.DTO.Currency;
using SolanaMusicApi.Domain.DTO.Dashboard.Overview;
using SolanaMusicApi.Domain.Entities;
using SolanaMusicApi.Domain.Entities.Transaction;
using SolanaMusicApi.Domain.Enums;
using SolanaMusicApi.Domain.Enums.Transaction;

namespace SolanaMusicApi.Application.Services.DashboardService;

public class DashboardService(ITracksService tracksService, ISubscriptionService subscriptionService, 
    ITransactionService transactionService, IUserProfileService userProfileService, IConfiguration configuration) 
    : IDashboardService
{
    public async Task<DashboardOverviewResponseDto> GetOverviewAsync()
    {
        var completedTransactions = transactionService
            .GetAll()
            .Where(x => x.Status == TransactionStatus.Completed && x.CreatedDate.Year == DateTime.UtcNow.Year);

        var revenue = await GetRevenueAsync(completedTransactions
            .Where(x => x.TransactionType == TransactionType.NftMint || x.TransactionType == TransactionType.SubscriptionPurchase));

        return new DashboardOverviewResponseDto
        {
            Revenue = revenue,
            TotalSongs = await GetChangeStatsAsync(tracksService.GetAll()),
            SubscriptionStats = await GetSubscriptionStatsResponseAsync(),
            ActiveUsers = await GetStatsAsync(userProfileService.GetAll()),
            NftSales = await GetMonthlyStatsAsync(completedTransactions.Where(x => x.TransactionType == TransactionType.NftMint))
        };
    }

    private async Task<SubscriptionStatsResponseDto> GetSubscriptionStatsResponseAsync()
    {
        var subscriptions = subscriptionService.GetAll();
        
        var stats = new SubscriptionStatsResponseDto
        {
            StatsChange = await GetChangeStatsAsync(subscriptions),
            SubscriptionStats = await GetSubscriptionStatsAsync()
        };

        stats.StatsChange.Count = stats.SubscriptionStats.Sum(x => x.Count);
        return stats;
    }

    private async Task<List<SubscriptionStats>> GetSubscriptionStatsAsync()
    {
        var subscriptionData = subscriptionService
            .GetAll()
            .Select(s => new { s.OwnerId, s.SubscriptionPlan.Type });
        
        var grouped = await subscriptionData
            .GroupBy(x => x.Type)
            .Select(g => new SubscriptionStats
            {
                SubscriptionType = g.Key,
                Count = g.Count()
            })
            .ToListAsync();
        
        var usersWithSubscription = await subscriptionData
            .Select(x => x.OwnerId)
            .Distinct()
            .CountAsync();
        
        var totalUsers = await userProfileService.GetAll().CountAsync();
        var noneCount = totalUsers - usersWithSubscription;

        if (noneCount > 0)
        {
            grouped.Add(new SubscriptionStats
            {
                SubscriptionType = SubscriptionType.Basic,
                Count = noneCount
            });
        }

        return grouped;
    }
    
    private async Task<StatsResponseDto> GetRevenueAsync(IQueryable<Transaction> transactions)
    {
        var solPrice = await GetSolPriceAsync();

        var monthly = await transactions
            .GroupBy(t => new { t.CreatedDate.Year, t.CreatedDate.Month })
            .OrderBy(g => g.Key.Month)
            .Select(g => new MonthlyStatsResponseDto
            {
                Date = new DateOnly(g.Key.Year, g.Key.Month, 1),
                Items = g.Sum(t => t.CurrencyId == 2 ? solPrice * t.Amount : t.Amount)
            })
            .ToListAsync();

        return new StatsResponseDto
        {
            Monthly = monthly,
            Change = CalculateChange(monthly)
        };
    }

    private async Task<decimal> GetSolPriceAsync()
    {
        var url = configuration.GetSection("SolanaTicker").Value;
        using var client = new HttpClient();
        var response = await client.GetStringAsync(url);
        var ticker = JsonConvert.DeserializeObject<BinancePriceResponse>(response);
        
        if (ticker == null || ticker.Price == 0)
            throw new Exception("Failed to get $SOL price from Binance API");
        
        return ticker.Price;
    }

    private static async Task<StatsResponseDto> GetStatsAsync<T>(IQueryable<T> items) where T : BaseEntity
    {
        var monthly = await GetMonthlyStatsAsync(items);
        var change = await GetChangeStatsAsync(items);

        return new StatsResponseDto
        {
            Monthly = monthly,
            Change = change
        };
    }

    private static async Task<List<MonthlyStatsResponseDto>> GetMonthlyStatsAsync<T>(IQueryable<T> items) where T : BaseEntity
    {
        var groupedData = await items
            .Where(x => x.CreatedDate.Year == DateTime.UtcNow.Year)
            .GroupBy(x => new { x.CreatedDate.Year, x.CreatedDate.Month })
            .Select(g => new
            {
                g.Key.Year,
                g.Key.Month,
                Count = g.Count()
            })
            .OrderBy(x => x.Month)
            .ToListAsync();
        
        return groupedData
            .Select(g => new MonthlyStatsResponseDto
            {
                Date = new DateOnly(g.Year, g.Month, 1),
                Items = g.Count
            })
            .ToList();
    }

    private static async Task<StatsChangeResponseDto> GetChangeStatsAsync<T>(IQueryable<T> items) where T : BaseEntity
    {
        var now = DateTime.UtcNow;
        var currentMonthStart = new DateTime(now.Year, now.Month, 1);
        var previousMonthStart = currentMonthStart.AddMonths(-1);

        var count = await items.CountAsync();
        var currentMonthCount = await items.CountAsync(t => t.CreatedDate >= currentMonthStart && t.CreatedDate <= now);
        var previousMonthCount = await items.CountAsync(t => t.CreatedDate >= previousMonthStart && t.CreatedDate < currentMonthStart);

        decimal percentageChange = 0;

        if (previousMonthCount > 0)
            percentageChange = (decimal)(currentMonthCount - previousMonthCount) / previousMonthCount * 100;
        else if (currentMonthCount > 0)
            percentageChange = 100;

        return new StatsChangeResponseDto
        {
            Count = count,
            Change = currentMonthCount - previousMonthCount,
            PercentageChange = Math.Round(percentageChange, 2)
        };
    }

    private static StatsChangeResponseDto CalculateChange(List<MonthlyStatsResponseDto> monthly)
    {
        switch (monthly.Count)
        {
            case 0:
                return new StatsChangeResponseDto();
            case 1:
                return new StatsChangeResponseDto
                {
                    Count = monthly[0].Items,
                    Change = 0,
                    PercentageChange = 0
                };
        }

        var last = monthly[^1];
        var prev = monthly[^2];

        if (prev.Date != last.Date.AddMonths(-1))
            return new StatsChangeResponseDto
            {
                Count = monthly.Sum(x => x.Items),
                Change = 0,
                PercentageChange = 0
            };

        var change = last.Items - prev.Items;
        var percentageChange = prev.Items != 0 ? change / prev.Items * 100 : last.Items != 0 ? 100 : 0;

        return new StatsChangeResponseDto
        {
            Count = monthly.Sum(x => x.Items),
            Change = change,
            PercentageChange = Math.Round(percentageChange, 2)
        };
    }
}

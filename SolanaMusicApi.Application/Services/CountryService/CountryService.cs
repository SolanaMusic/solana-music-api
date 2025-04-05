using Microsoft.EntityFrameworkCore;
using SolanaMusicApi.Application.Services.BaseService;
using SolanaMusicApi.Domain.Entities.General;
using SolanaMusicApi.Infrastructure.Repositories.BaseRepository;

namespace SolanaMusicApi.Application.Services.CountryService;

public class CountryService(IBaseRepository<Country> baseRepository) : BaseService<Country>(baseRepository), ICountryService
{
    public async Task<Country> GetCountryByNameAsync(string countryName)
    {
        return await GetAll().FirstOrDefaultAsync(c => c.Name == countryName) 
               ?? throw new ArgumentNullException(nameof(countryName), "Country not found");
    }
}

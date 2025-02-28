using Microsoft.EntityFrameworkCore;
using SolanaMusicApi.Application.Services.BaseService;
using SolanaMusicApi.Domain.Entities.General;
using SolanaMusicApi.Infrastructure.Repositories.BaseRepository;

namespace SolanaMusicApi.Application.Services.CountryService;

public class CountryService : BaseService<Country>, ICountryService
{
    public CountryService(IBaseRepository<Country> baseRepository) : base(baseRepository) { }

    public async Task<Country?> GetCountryByNameAsync(string countryName)
    {
        return await GetAll().FirstOrDefaultAsync(c => c.Name == countryName);
    }
}

using SolanaMusicApi.Application.Services.BaseService;
using SolanaMusicApi.Domain.Entities.General;

namespace SolanaMusicApi.Application.Services.CountryService;

public interface ICountryService : IBaseService<Country>
{
    Task<Country> GetCountryByNameAsync(string countryName);
}

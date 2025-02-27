using SolanaMusicApi.Application;
using SolanaMusicApi.Domain.Entities.General;
using SolanaMusicApi.Infrastructure.Repositories.BaseRepository;

namespace SolanaMusicApi.Infrastructure.Repositories.CountryRepository;

public class CountryRepository : BaseRepository<Country>, ICountryRepository
{
    public CountryRepository(ApplicationDbContext context) : base(context) { }
}

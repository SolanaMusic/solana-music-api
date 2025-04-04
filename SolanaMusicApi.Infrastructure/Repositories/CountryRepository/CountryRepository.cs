using SolanaMusicApi.Domain.Entities.General;
using SolanaMusicApi.Infrastructure.Repositories.BaseRepository;

namespace SolanaMusicApi.Infrastructure.Repositories.CountryRepository;

public class CountryRepository(ApplicationDbContext context) : BaseRepository<Country>(context), ICountryRepository;

using SolanaMusicApi.Domain.Entities.Performer;

namespace SolanaMusicApi.Domain.Entities.General;

public class Country : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string CountryCode { get; set; } = string.Empty;

    public ICollection<Artist> Artists { get; set; } = [];
}

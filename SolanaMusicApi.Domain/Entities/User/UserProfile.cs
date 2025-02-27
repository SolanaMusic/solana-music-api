using SolanaMusicApi.Domain.Entities.General;

namespace SolanaMusicApi.Domain.Entities.User;

public class UserProfile : BaseEntity
{
    public long UserId { get; set; }
    public long CountryId { get; set; }
    public string? AvatarUrl { get; set; }
    public long TokensAmount { get; set; }

    public ApplicationUser User { get; set; } = null!;
    public Country Country { get; set; } = null!;
}

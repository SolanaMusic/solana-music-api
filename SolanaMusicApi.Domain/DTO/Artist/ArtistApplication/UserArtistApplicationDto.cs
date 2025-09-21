using Microsoft.AspNetCore.Identity;
using SolanaMusicApi.Domain.Enums;

namespace SolanaMusicApi.Domain.DTO.Artist.ArtistApplication;

public class UserArtistApplicationDto : BaseResponseDto
{
    public string ResourceUrl { get; set; } = string.Empty;
    public string ContactLink { get; set; } = string.Empty;
    public ArtistApplicationStatus Status { get; set; }
    public DateTime CreatedDate { get; set; }
    
    public IdentityUser<long>? Reviewer { get; set; }
}
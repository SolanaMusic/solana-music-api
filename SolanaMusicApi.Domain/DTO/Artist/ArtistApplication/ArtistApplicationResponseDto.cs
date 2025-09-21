using Microsoft.AspNetCore.Identity;
using SolanaMusicApi.Domain.DTO.User;
using SolanaMusicApi.Domain.Entities;
using SolanaMusicApi.Domain.Enums;

namespace SolanaMusicApi.Domain.DTO.Artist.ArtistApplication;

public class ArtistApplicationResponseDto : BaseEntity
{
    public string ResourceUrl { get; set; } = string.Empty;
    public string ContactLink { get; set; } = string.Empty;
    public ArtistApplicationStatus Status { get; set; } = ArtistApplicationStatus.Pending;
    
    public UserResponseDto User { get; set; } = null!;
    public IdentityUser<long>? Reviewer { get; set; }
}
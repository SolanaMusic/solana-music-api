using SolanaMusicApi.Domain.Enums;

namespace SolanaMusicApi.Domain.DTO.Artist.ArtistApplication;

public class UpdateArtistApplicationDto
{
    public long? ReviewerId { get; set; }
    public string? ArtistName { get; set; }
    public string? Bio { get; set; }
    public long? CountryId { get; set; }
    public ArtistApplicationStatus? Status { get; set; }
}
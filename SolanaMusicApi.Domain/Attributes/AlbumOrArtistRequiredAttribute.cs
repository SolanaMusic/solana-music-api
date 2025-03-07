using SolanaMusicApi.Domain.DTO.Track;
using System.ComponentModel.DataAnnotations;

namespace SolanaMusicApi.Domain.Attributes;

public class AlbumOrArtistRequiredAttribute : ValidationAttribute
{
    public AlbumOrArtistRequiredAttribute() : base() { }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var trackRequestDto = (TrackRequestDto)validationContext.ObjectInstance;

        if (!trackRequestDto.GenreIds.Any())
            throw new Exception("Genres must be specified");

        if (trackRequestDto.Image != null && trackRequestDto.AlbumId.HasValue)
            throw new Exception("Both Image and AlbumId cannot be specified together");

        if (trackRequestDto.Image == null && !trackRequestDto.AlbumId.HasValue)
            throw new Exception("Image or AlbumId must be specified");

        return ValidationResult.Success;
    }
}


using SolanaMusicApi.Domain.DTO.Track;
using System.ComponentModel.DataAnnotations;

namespace SolanaMusicApi.Domain.Attributes;

public class AlbumOrArtistRequiredAttribute : ValidationAttribute
{
    private const string message = "Either AlbumId or ArtistIds must be provided";

    public AlbumOrArtistRequiredAttribute() : base(message) { }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var trackRequestDto = (TrackRequestDto)validationContext.ObjectInstance;

        if (trackRequestDto.Image == null && !trackRequestDto.AlbumId.HasValue)
            throw new Exception("Either Image should be provided or AlbumId must be specified");

        if (trackRequestDto.Image != null && trackRequestDto.AlbumId.HasValue)
            throw new Exception("Both Image and AlbumId cannot be specified together");

        if (trackRequestDto.AlbumId.HasValue || (trackRequestDto.ArtistIds != null && trackRequestDto.ArtistIds.Any()))
            return ValidationResult.Success;

        throw new Exception(message);
    }
}


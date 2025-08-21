using System.ComponentModel.DataAnnotations;

namespace CinemaSolutionApi.Dtos.Movie;

public record class MovieDto(
    [Required(ErrorMessage = "Title can't be empty")]
    string Title,
    [Required(ErrorMessage = "Duration can't be empty.")]
    [Range(1, int.MaxValue, ErrorMessage = "Duration must be a positive number.")]
    int Duration,
    [Required(ErrorMessage = "Nationality can't be empty")]
    bool IsInternational,
    [Required(ErrorMessage = "Image can't be empty")]
    [Url]
    string Image,
    [Required(ErrorMessage = "Director can't be empty")]
    [Range(1, int.MaxValue, ErrorMessage = "Duration must be a positive number")]
    int Director
);
using System;

namespace Application.Dtos;

public class VideoGameDto
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public required string Description { get; set; }
    public required string Genre { get; set; }
    public required string ImageUrl { get; set; }
    public required string Platform { get; set; }
    public required string Title { get; set; }
    public DateTime ReleaseDate { get; set; } = DateTime.UtcNow;
    public string? Url { get; set; }
    public string? Publisher { get; set; }

}

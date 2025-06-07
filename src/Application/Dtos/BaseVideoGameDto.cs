using System;

namespace Application.Dtos;

public class BaseVideoGameDto
{    public string Description { get; set; } = "";
    public string Genre { get; set; } = "";
    public string ImageUrl { get; set; }= "";
    public string Platform { get; set; } = "";
    public string Title { get; set; } = "";
    public DateTime ReleaseDate { get; set; }
    public string Url { get; set; } = "";
    public string Publisher { get; set; } = "";

}

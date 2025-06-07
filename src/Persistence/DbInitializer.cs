using System;
using Domain;

namespace Persistence;

public class DbInitializer
{
    public static async Task SeedData(AppDbContext context)
    {
        if (context.VideoGames.Any())
        {
            return; // Data already seeded
        }

        var videoGames = new List<VideoGame>
        {
            new VideoGame
            {
                Title = "The Legend of Zelda: Breath of the Wild",
                Description = "An open-world action-adventure game set in the kingdom of Hyrule.",
                Genre = "Action-Adventure",
                ImageUrl = "https://example.com/zelda.jpg",
                Platform = "Nintendo Switch",
                ReleaseDate = new DateTime(2017, 3, 3),
                Url = "https://www.zelda.com/breath-of-the-wild/",
                Publisher = "Nintendo"
            },
            new VideoGame
            {
                Title = "The Witcher 3: Wild Hunt",
                Description = "An open-world RPG set in a fantasy world filled with monsters and magic.",
                Genre = "RPG",
                ImageUrl = "https://example.com/witcher3.jpg",
                Platform = "PC, PS4, Xbox One, Nintendo Switch",
                ReleaseDate = new DateTime(2015, 5, 19),
                Url = "https://thewitcher.com/en/witcher3",
                Publisher = "CD Projekt"
            },
            new VideoGame
            {
                Title = "God of War",
                Description = "An action-adventure game that follows Kratos, a former Greek god, on his journey through Norse mythology.",
                Genre = "Action-Adventure",
                ImageUrl = "https://example.com/godofwar.jpg",
                Platform = "PS4, PC",
                ReleaseDate = new DateTime(2018, 4, 20),
                Url = "https://www.playstation.com/en-us/games/god-of-war/",
                Publisher = "Sony Interactive Entertainment"
            },
            new VideoGame
            {
                Title = "Minecraft",
                Description = "A sandbox game that allows players to build and explore virtual worlds made of blocks.",
                Genre = "Sandbox",
                ImageUrl = "https://example.com/minecraft.jpg",
                Platform = "PC, PS4, Xbox One, Nintendo Switch, Mobile",
                ReleaseDate = new DateTime(2011, 11, 18),
                Url = "https://www.minecraft.net/",
                Publisher = "Mojang Studios"
            },
            new VideoGame
            {
                Title = "Cyberpunk 2077",
                Description = "An open-world RPG set in a dystopian future where players assume the role of V, a mercenary in Night City.",
                Genre = "RPG",
                ImageUrl = "https://example.com/cyberpunk2077.jpg",
                Platform = "PC, PS4, Xbox One, PS5, Xbox Series X/S",
                ReleaseDate = new DateTime(2020, 12, 10),
                Url = "https://www.cyberpunk.net/",
                Publisher = "CD Projekt"
            }
            // Add more video games as needed

        };

        context.VideoGames.AddRange(videoGames);
        await context.SaveChangesAsync();
    }

}

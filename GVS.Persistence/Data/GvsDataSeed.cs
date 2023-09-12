using GVS.Domain;
using GVS.Domain.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GVS.Persistence.Data
{
    public class GvsDataSeed
    {
        public static async Task AddAsync(ApplicationDbContext context, ILogger<GvsDataSeed> logger)
        {
            await context.Database.EnsureCreatedAsync();
            if (!context.Leagues.Any())
            {
                await context.Leagues.AddRangeAsync(CreateLeagueList());
                await context.SaveChangesAsync();
                logger.LogInformation("Seed Leagues table associated with context {DbContextName}", typeof(ApplicationDbContext).Name);
            }

            if (!context.Providers.Any())
            {
                await context.Providers.AddRangeAsync(CreateProviderList());
                await context.SaveChangesAsync();
                logger.LogInformation("Seed Providers table associated with context {DbContextName}", typeof(ApplicationDbContext).Name);
            }

            if (!context.Games.Any())
            {
                context.Games.AddRangeAsync(CreateGameList());
                context.SaveChangesAsync();
                logger.LogInformation("Seed Providers table associated with context {DbContextName}", typeof(ApplicationDbContext).Name);
            }
        }

        private static IList<League> CreateLeagueList() => new List<League>()
        {
            new League() {Id = 1, LeagueName = "Israeli Premier League"},
            new League() {Id = 2 ,LeagueName = "La Liga"},
            new League() {Id = 3 ,LeagueName = "england Premier League"},
        };


        private static IList<Provider> CreateProviderList() => new List<Provider>()
        {
            new Provider() {Id = 1, ProviderName = "Sport 5"},
            new Provider() {Id = 2, ProviderName = "BBC"},
            new Provider() {Id = 3, ProviderName = "Sport 1"},
        };


        private static IList<Game> CreateGameList() => new List<Game>()
        {
            new Game() {Language = "Hebrew", ProviderId = 1, Description = "blalalala", UploadTime = DateTime.UtcNow, LeagueId = 2,
                Link = "http://lalalala", Title = "Maccabi vs Hapoel", Location = "Tel Aviv", },
            new Game() {Language = "Hebrew", ProviderId = 2, Description = "blalalala", UploadTime = DateTime.UtcNow, LeagueId = 2,
                Link = "http://lalalala", Title = "Maccabi vs Haifa", Location = "Tel Aviv", },
                        new Game() {Language = "Hebrew", ProviderId = 1, Description = "blalalala", UploadTime = DateTime.UtcNow, LeagueId = 3,
                Link = "http://lalalala", Title = "Maccabi vs Beer Sheva", Location = "Beer Sheva", },
        };
    }
}

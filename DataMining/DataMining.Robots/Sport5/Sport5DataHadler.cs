using DataMining.Domain.Entities;
using DataMining.Infrastructure.Services.GamesService;
using DataMining.Shared.Interfaces;

namespace DataMining.Robots.Sport5
{
    public class Sport5DataHadler : IHandleData<List<Sport5GameModel>>
    {
        private readonly IGameService _gamesService;

        public Sport5DataHadler(IGameService gamesService)
        {
            _gamesService = gamesService;
        }
        public Task Handle(List<Sport5GameModel> data)
        {
            List<Game> games = data.Select(d => new Game()
            {
                Description = d.Description,
                Language = d.Language,
                LeagueId = d.LeagueId,
                Link = d.Link,
                Location = d.Location,
                ProviderId = d.ProviderId,
                Title = d.Title,
                UploadTime = d.UploadTime,
            }).ToList();

            _gamesService.AddGames(games);
            return Task.CompletedTask;
        }
    }
}
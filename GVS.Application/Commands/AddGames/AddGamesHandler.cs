using GVS.Domain;
using GVS.Domain.Repositories;
using MediatR;

namespace GVS.Application.Commands.AddGames
{
    public class AddGamesHandler :
        IRequestHandler<AddGamesRequest, Unit>
    {
        private readonly IGamesRepository _gamesRepo;
        public AddGamesHandler(IGamesRepository gamesRepo)
        {
            _gamesRepo = gamesRepo;
        }


        public async Task<Unit> Handle(AddGamesRequest request, CancellationToken cancellationToken)
        {
            var games = MapGames(request.Games);
            await _gamesRepo.AddGames(games);

            return Unit.Value;
        }

        private List<Game> MapGames(List<GameRequest> games) => games.Select(g => new Game()
        {
            Description = g.Description,
            LeagueId = g.LeagueId,
            Link = g.Link,
            Location = g.Location,
            ProviderId = g.ProviderId,
            Title = g.Title,
            UploadTime = g.UploadTime.ToUniversalTime(),
            Language = g.Language
        }).ToList();
    }
}

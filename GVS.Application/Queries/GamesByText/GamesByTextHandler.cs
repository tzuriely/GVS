using GVS.Domain;
using GVS.Domain.Repositories;
using GVS.Persistence.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GVS.Application.Queries.GamesByText
{
    public class GamesByTextHandler :
        IRequestHandler<GamesByTextRequest, GamesByTextResponse>
    {
        private readonly IGamesRepository _gamesRepo;
        public GamesByTextHandler(IGamesRepository gamesRepo)
        {
            _gamesRepo = gamesRepo;
        }

        public async Task<GamesByTextResponse> Handle(GamesByTextRequest request, CancellationToken cancellationToken)
        {
            var gamesList = await _gamesRepo.GetGamesByText(request.Text);
            if (gamesList is null || !gamesList.Any())
            {
                return null;
            }
            var response = MapGamesResponse(gamesList);

            return response;
        }

        private GamesByTextResponse MapGamesResponse(List<Game> gamesList) => new GamesByTextResponse()
        {
            Games = gamesList.Select(g => new GameResponse()
            {
                Description = g.Description,
                GameId = g.GameId,
                Language = g.Language,
                LeagueId = g.LeagueId,
                Link = g.Link,
                Location = g.Location,
                ProviderId = g.ProviderId,
                Title = g.Title,
                UploadTime = g.UploadTime
            }).ToList()
        };
    }
}

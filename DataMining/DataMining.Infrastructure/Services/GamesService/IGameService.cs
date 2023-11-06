using DataMining.Domain.Entities;

namespace DataMining.Infrastructure.Services.GamesService
{
    public interface IGameService
    {
        Task AddGames(List<Game> games);
    }
}
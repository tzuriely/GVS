using DataMining.Domain.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DataMining.Infrastructure.Services.GamesService
{
    public class GameService : IGameService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public GameService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task AddGames(List<Game> games)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("gamesApi");

                PostGamesRequest gamesRequest = new PostGamesRequest(games);
                var json = JsonConvert.SerializeObject(gamesRequest);
                var data = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync("", data);
            }
            catch (Exception ex)
            {

                throw;
            }

        }
    }

    internal record PostGamesRequest(List<Game> games);
}

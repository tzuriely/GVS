using GVS.Domain;
using GVS.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GVS.Persistence.Repositories
{
    public class GamesRepository
        : IGamesRepository
    {
        private readonly ApplicationDbContext _context;

        public GamesRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddGames(List<Game> games)
        {
            await _context.Games.AddRangeAsync(games);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Game>> GetGamesByText(string text)
        {
            var games = await _context.Games
                .Where(g => g.Title.Contains(text)).ToListAsync();

            return games;
        }
    }
}

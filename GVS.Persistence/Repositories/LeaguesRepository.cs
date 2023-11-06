using GVS.Domain.Entities;
using GVS.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GVS.Persistence.Repositories
{
    public class LeaguesRepository
        : ILeaguesRepository
    {
        private readonly ApplicationDbContext _context;


        public LeaguesRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<League>> GetAllLeagues()
        {
            return await _context.Leagues.ToListAsync();
        }
    }
}

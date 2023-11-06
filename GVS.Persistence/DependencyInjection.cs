using GVS.Domain.Repositories;
using GVS.Persistence.Data;
using GVS.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace GVS.Persistence
{
    public static class DependencyInjection
    {
        public static async Task<IServiceCollection> AddPersistence(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options
                    .UseNpgsql(configuration.GetConnectionString("Database"), a => a.MigrationsAssembly("GVS.Persistence"))
                    .UseSnakeCaseNamingConvention());

            services.AddScoped<IGamesRepository, GamesRepository>();
            services.AddScoped<ILeaguesRepository, LeaguesRepository>();

            var serviceProvider = services.BuildServiceProvider();
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
            var logger = serviceProvider.GetService<ILogger<GvsDataSeed>>();

            await GvsDataSeed.AddAsync(context, logger);

            return services;
        }
    }
}

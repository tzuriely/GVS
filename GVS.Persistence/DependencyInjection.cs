﻿using GVS.Domain.Repositories;
using GVS.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GVS.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options
                    .UseNpgsql(configuration.GetConnectionString("Database"), a => a.MigrationsAssembly("GVS.Persistence"))
                    .UseSnakeCaseNamingConvention());

            services.AddScoped<IGamesRepository, GamesRepository>();

            return services;
        }
    }
}

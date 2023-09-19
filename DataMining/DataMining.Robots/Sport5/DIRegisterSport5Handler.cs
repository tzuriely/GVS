using DataMining.Shared;
using DataMining.Shared.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DataMining.Robots.Sport5
{
    public static class DIRegisterSport5Handler
    {
        public static void RegisterSport5Robot(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCronJob<Sport5Handler, Sport5GameModel>(c =>
            {
                c.CronExpression = $"{configuration.GetValue<string>("sport5Robot:CronExpression")}";
                c.TimeZoneInfo = TimeZoneInfo.Local;
            });

            services.AddTransient<IHandleData<List<Sport5GameModel>>, Sport5DataHadler>();
            services.AddTransient<IRobot<List<Sport5GameModel>>, Sport5Robot>();
        }
    }
}
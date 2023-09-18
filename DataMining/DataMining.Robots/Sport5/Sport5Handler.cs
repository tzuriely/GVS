using DataMining.Shared;
using DataMining.Shared.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DataMining.Robots.Sport5
{
    public class Sport5Handler : CronJobService<Sport5GameModel>
    {
        public Sport5Handler(string cronExpression,
            IHandleData<List<Sport5GameModel>> dataHandler,
            IRobot<List<Sport5GameModel>> robot, 
            ILoggerFactory loggerFactory)
            :base(cronExpression,
                 dataHandler,
                 robot,
                 loggerFactory)
        {

        }
    }

    public static class DIRegisterSport5Handler
    {
        public static void RegisterAmazonActivationHandlerServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCronJob<Sport5Handler, Sport5GameModel>(c =>
            {
                c.CronExpression = $"{configuration.GetValue<string>("sport5Robot:CronExpression")}";
            });

            services.AddTransient<IHandleData<List<Sport5GameModel>>, Sport5DataHadler>();
            services.AddTransient<IRobot<List<Sport5GameModel>>, Sport5Robot>();
        }
    }
}
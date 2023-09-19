using DataMining.Shared;
using DataMining.Shared.Interfaces;
using Microsoft.Extensions.Logging;

namespace DataMining.Robots.Sport5
{
    public class Sport5Handler : CronJobService<Sport5GameModel>
    {
        public Sport5Handler(ICronJobSettings<Sport5Handler> config,
            IHandleData<List<Sport5GameModel>> dataHandler,
            IRobot<List<Sport5GameModel>> robot, 
            ILoggerFactory loggerFactory)
            :base(config.CronExpression,
                 dataHandler,
                 robot,
                 loggerFactory)
        {

        }
    }
}
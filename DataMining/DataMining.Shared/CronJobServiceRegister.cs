using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMining.Shared
{
    public static class CronJobServiceRegister
    {
        public static IServiceCollection AddCronJob<T, TGame>(this IServiceCollection services, Action<ICronJobSettings<T>> options) where T : CronJobService<TGame>
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options), @"Please provide Schedule Configurations.");
            }
            var config = new CronJobSettings<T>();
            options.Invoke(config);

            if (string.IsNullOrWhiteSpace(config.CronExpression))
            {
                throw new ArgumentNullException(nameof(CronJobSettings<T>.CronExpression), @"Empty Cron Expression is not allowed.");
            }

            if (config.TimeZoneInfo == null || config.TimeZoneInfo != TimeZoneInfo.Local)
            {
                throw new ArgumentNullException(nameof(CronJobSettings<T>.TimeZoneInfo), $"invalid timezone: {config.TimeZoneInfo}");
            }

            services.AddSingleton<ICronJobSettings<T>>(config);
            services.AddHostedService<T>();
            return services;
        }
    }
}

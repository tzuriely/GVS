using Cronos;
using DataMining.Shared.Interfaces;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;

namespace DataMining.Shared
{
    public abstract class CronJobService<TGame> : IHostedService, IDisposable
    {
        private readonly CronExpression _expression;
        private readonly IHandleData<List<TGame>> _dataHandler;
        private readonly IRobot<List<TGame>> _robot;
        private readonly ILogger _logger;
        private System.Timers.Timer _timer;

        protected CronJobService(
            string cronExpression,
            IHandleData<List<TGame>> dataHandler,
            IRobot<List<TGame>> robot,
            ILoggerFactory loggerFactory)
        {
            _expression = CronExpression.Parse(cronExpression);
            _dataHandler = dataHandler;
            _robot = robot;
            _logger = loggerFactory.CreateLogger(nameof(CronJobService<TGame>));
        }

        protected virtual async Task ScheduleJob(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"[*] {nameof(CronJobService<TGame>)} => ScheduleJob started.");

            var next = _expression.GetNextOccurrence(DateTimeOffset.Now, TimeZoneInfo.Local);

            if (next.HasValue)
            {
                var delay = next.Value - DateTimeOffset.Now;
                if (delay.TotalMilliseconds <= 0)   // prevent non-positive values from being passed into Timer
                {
                    await ScheduleJob(cancellationToken);
                }

                _timer = new System.Timers.Timer(delay.TotalMilliseconds);
                _timer.Elapsed += async (sender, args) =>
                {
                    _timer.Dispose();  // reset and dispose timer
                    _timer = null;

                    if (!cancellationToken.IsCancellationRequested)
                    {
                        await StartWork(cancellationToken, "");
                    }

                    if (!cancellationToken.IsCancellationRequested)
                    {
                        await ScheduleJob(cancellationToken);    // reschedule next
                    }
                };
                _timer.Start();
            }
            _logger.LogInformation($"[*] {nameof(CronJobService<TGame>)} => ScheduleJob finished.");

            await Task.CompletedTask;
        }

        public virtual async Task StartWork(CancellationToken cancellationToken, string FileName)
        {
            _logger.LogInformation($"{nameof(CronJobService<TGame>)} => StartWork started.");

            var games = await _robot.Run();

            _logger.LogInformation($"{nameof(CronJobService<TGame>)} => Robot work finished. started at {games.StartTime}, " +
                $"finished: {games.EndTime}. Number of games: {games.NumOfGames}");

            await _dataHandler.Handle(games.Content);
            _logger.LogInformation($"{nameof(CronJobService<TGame>)} => Data processing finished");

            await Task.CompletedTask;
        }

        public virtual async Task StartAsync(CancellationToken cancellationToken)
        {
        _logger.LogInformation($"[*] {nameof(CronJobService<TGame>)} => StartAsync started.");

        await ScheduleJob(cancellationToken);

        _logger.LogInformation($"[*] {nameof(CronJobService<TGame>)} => StartAsync finished.");
    }

        public virtual async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"[*] {nameof(CronJobService<TGame>)} => StopAsync started.");

            _timer?.Stop();
            await Task.CompletedTask;

            _logger.LogInformation($"[*] {nameof(CronJobService<TGame>)} => StopAsync finished.");
        }

        public virtual void Dispose()
        {
            _logger.LogInformation($"[*] {nameof(CronJobService<TGame>)} => Dispose started.");

            _timer?.Dispose();

            _logger.LogInformation($"[*] {nameof(CronJobService<TGame>)} => Dispose finished.");
        }
    }
}
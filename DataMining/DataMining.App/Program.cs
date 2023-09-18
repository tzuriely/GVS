using DataMining.Robots.Sport5;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DataMining.App
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
        }


        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) =>
            {
                var configuration = hostContext.Configuration;
                //services.AddLogging(loggingBuilder =>
                //{
                //    loggingBuilder.AddConfiguration(configuration.GetSection("Logging"));
                //    loggingBuilder.AddNLog(configuration);
                //    loggingBuilder.AddConsole();
                //});

                //services.AddRabbitMQSettings(configuration);
                //services.RegisterBLServices(configuration);
                //services.RegisterHelperServices(configuration);
                //services.RegisterDALServices(configuration);
                //services.RegisterActivesHandlersServices(configuration);
                //services.RegisterUpdaterInfraQueServices(configuration);

                services.RegisterAmazonActivationHandlerServices(configuration);
            });
    }
}
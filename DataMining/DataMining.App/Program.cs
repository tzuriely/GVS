using DataMining.Infrastructure.Services.GamesService;
using DataMining.Robots.Sport5;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Text;

namespace DataMining.App
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");


            ConfigEncoding();
            CreateHostBuilder(args).Build().Run();
        }


        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) =>
            {
                var configuration = hostContext.Configuration;
                services.AddLogging(loggingBuilder =>
                {
                    loggingBuilder.AddConfiguration(configuration.GetSection("Logging"));
                    //loggingBuilder.AddNLog(configuration);
                    loggingBuilder.AddConsole();
                });

                services.AddScoped<IGameService, GameService>();

                services.RegisterSport5Robot(configuration);

                services.AddHttpClient("gamesApi", client =>
                {
                    client.BaseAddress = new Uri(configuration.GetValue<string>("GamesApi:BaseUrl"));
                });
            });


        public static void ConfigEncoding()
        {
            System.Text.EncodingProvider ppp = System.Text.CodePagesEncodingProvider.Instance;
            Encoding.RegisterProvider(ppp);
        }
    }
}
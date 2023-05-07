// See https://aka.ms/new-console-template for more information

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SignalRClient;
using SignalRClient.Logconfig;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddHostedService<Worker>();
        services.AddSingleton<ConfigurationHelper>();
    })
    .ConfigureAppConfiguration( configurationBuilder =>
    {
        configurationBuilder.AddJsonFile("appSettings.json", false, true);
        configurationBuilder.AddLoggingConfiguration("logging");
    }).ConfigureLogging((_, logging) =>
    {
        logging.ClearProviders();
        logging.AddConsole();

    })
    .Build();

host.Run();


using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SignalRClient.Logconfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace SignalRClient
{
    public class Worker : BackgroundService
    {
        private readonly HubConnection hubConnection;
        private readonly PeriodicTimer timer = new PeriodicTimer(TimeSpan.FromSeconds(5));
        private readonly ILogger<Worker> logger;
        private readonly ConfigurationHelper configurationHelper;

        public Worker(ILogger<Worker> logger, ConfigurationHelper configurationHelper)
        {
            hubConnection = new HubConnectionBuilder()
                           .WithUrl("http://localhost:5189/notify")
                           .WithAutomaticReconnect()
                           .Build();
            this.logger = logger;
            this.configurationHelper = configurationHelper;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            hubConnection.On<string>("ReceiveMessage", (message) =>
            {
                LogHelper.Log($"Received-Message From server '{message}'", ConsoleColor.Green);
            });

            hubConnection.On<string>("ChangeLogLevel", (message) =>
            {
                LogHelper.Log($"Received-Message From server to ChangeLogLevel '{message}'", ConsoleColor.Green);
                string[] items = message.Split('|');
                var logLevel = items[0];
                var logCategory = items[1];

                if (Enum.TryParse<LogLevel>(logLevel, out var enumlogLevel))
                {
                    LogHelper.Log($"Changing the log level '{message}'", ConsoleColor.Yellow);
                    configurationHelper.ChangeLogLevel(enumlogLevel, logCategory);
                }
                else
                {
                    LogHelper.Log($"Invalid log level '{message}'", ConsoleColor.Green);
                }


            });
            await hubConnection.StartAsync();
            LogHelper.Log($"Joining group 'configchange'", ConsoleColor.Yellow);
            await hubConnection.InvokeAsync("JoinGroup", "configchange");


            while (await timer.WaitForNextTickAsync())
            {
                //Business logic
                logger.LogInformation("This is information logging");
                logger.LogTrace("This is trace logging");
                logger.LogDebug("This is debug logging");
            }

            await hubConnection.InvokeAsync("LeaveGroup", "configchange");
        }
    }
}

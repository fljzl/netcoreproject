﻿using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace webwindows.bacservice
{
    public class ServiceA : BackgroundService
    {
        public ServiceA(ILoggerFactory loggerFactory)
        {
            Logger = loggerFactory.CreateLogger<ServiceA>();
        }

        public ILogger Logger { get; }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Logger.LogInformation("ServiceA is starting.");

            stoppingToken.Register(() => Logger.LogInformation("ServiceA is stopping."));

            while (!stoppingToken.IsCancellationRequested)
            {
                Logger.LogInformation("ServiceA is doing background work.");

                await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
            }

            Logger.LogInformation("ServiceA has stopped.");
        }

      
    }

    public class ServiceB : BackgroundService
    {
        public ServiceB(ILoggerFactory loggerFactory)
        {
            Logger = loggerFactory.CreateLogger<ServiceB>();
        }

        public ILogger Logger { get; }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Logger.LogInformation("ServiceB is starting.");

            stoppingToken.Register(() => Logger.LogInformation("ServiceB is stopping."));

            while (!stoppingToken.IsCancellationRequested)
            {
                Logger.LogInformation("ServiceB is doing background work.");

                await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
            }

            Logger.LogInformation("ServiceB has stopped.");
        }
    }
}

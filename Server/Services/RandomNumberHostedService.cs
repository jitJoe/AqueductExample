using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Aqueduct.Server.ServiceProvider;
using AqueductExample.Shared;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AqueductExample.Server.Services
{
    public class RandomNumberHostedService : IHostedService
    {
        private readonly IServerServiceProvider _serverServiceProvider;
        private readonly ILogger<RandomNumberHostedService> _logger;
        private readonly Random _random = new();
        private Timer _timer;

        public RandomNumberHostedService(IServerServiceProvider serverServiceProvider, ILogger<RandomNumberHostedService> logger)
        {
            _serverServiceProvider = serverServiceProvider;
            _logger = logger;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(Tick, null, TimeSpan.Zero, 
                TimeSpan.FromSeconds(5));

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }
        
        private void Tick(object state)
        {
            var message = new FancyChatMessage
            {
                Sender = "RandomNumberHostedService",
                Message = $"Random number (1-100): {_random.Next(1, 100)}",
                SentAt = DateTimeOffset.Now,
                Colour = "red"
            };
            
            var sendMessageTasks = _serverServiceProvider.GetClientServiceForAllConnectionsAsync<IChatClientService>().Result
                .Select(clientService => clientService.SendMessageAsync(message));

            try
            {
                Task.WhenAll(sendMessageTasks).Wait();
            }
            catch (Exception exception)
            {
                _logger.LogError("Something went wrong sending a message", exception);
            }
        }
    }
}
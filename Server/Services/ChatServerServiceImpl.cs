using System;
using System.Linq;
using System.Threading.Tasks;
using Aqueduct.Server;
using AqueductExample.Shared;
using Microsoft.Extensions.Logging;

namespace AqueductExample.Server.Services
{
    public class ChatServerServiceImpl : ServerService, IChatServerService
    {
        private readonly ILogger<ChatServerServiceImpl> _logger;

        public ChatServerServiceImpl(ILogger<ChatServerServiceImpl> logger)
        {
            _logger = logger;
        }

        public async Task SendMessage(ChatMessage message)
        {
            var sendMessageTasks = (await ServerServiceProvider.GetClientServiceForAllConnectionsAsync<IChatClientService>())
                .Select(clientService => clientService.SendMessageAsync(message));

            try
            {
                await Task.WhenAll(sendMessageTasks);
            }
            catch (Exception exception)
            {
                _logger.LogError("Something went wrong sending a message", exception);
                return;
            }
        }
    }
}
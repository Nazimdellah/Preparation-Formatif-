using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using signalr.backend.Data;
using signalr.backend.Hubs;
using signalr.backend.Models;
using static System.Formats.Asn1.AsnWriter;

namespace signalr.backend.Services
{
    public class ChatBackgroundService : BackgroundService
    {
        private IServiceScopeFactory _serviceScopeFactory;
        private IHubContext<ChatHub> _chatHubContext;

        public ChatBackgroundService(IHubContext<ChatHub> chatHubContext, IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _chatHubContext = chatHubContext;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while(!stoppingToken.IsCancellationRequested)
            {
                using (IServiceScope scope = _serviceScopeFactory.CreateScope())
                {
                    ApplicationDbContext dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                    // On trouve le nombre de message du canal le plus populaire (il pourrait y en avoir plusieurs avec le même nombre)
                    int nbMessagesOfMostPopularChannels = dbContext.Channel.Select(c => c.NbMessages).Max();

                    if(nbMessagesOfMostPopularChannels > 0)
                    {
                        // On fait une requête pour les channels qui ont AU MOINS cette quantité.
                        // (Il y a peut-être eu un nouveau message pendant la fraction de seconde qui vient de passer!)
                        var mostPopularChannels = dbContext.Channel
                            .Where(c => c.NbMessages >= nbMessagesOfMostPopularChannels)
                            .ToList();

                        var mostPopularChannelGroups = mostPopularChannels.Select(c => ChatHub.CreateChannelGroupName(c.Id));

                        // Avec Groups on peut envoyer a plusieurs groupes en specifiant une liste de group names
                        await _chatHubContext.Clients.Groups(mostPopularChannelGroups).SendAsync("MostPopularChannel", nbMessagesOfMostPopularChannels, stoppingToken);
                    }
                }

                // On attend 30 secondes
                await Task.Delay(30* 1000);
            }
            throw new NotImplementedException();
        }
    }
}

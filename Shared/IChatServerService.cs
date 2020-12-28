using System.Threading.Tasks;

namespace AqueductExample.Shared
{
    public interface IChatServerService
    {
        Task SendMessage(ChatMessage chatMessage);
    }
}
using System.Threading.Tasks;

namespace AqueductExample.Shared
{
    public interface IChatClientService
    {
        Task SendMessageAsync(ChatMessage message);
    }
}
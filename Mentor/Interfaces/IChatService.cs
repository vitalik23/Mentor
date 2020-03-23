using Mentor.Models;
using System.Threading.Tasks;

namespace Mentor.Interfaces
{
    public interface IChatService
    {

        Task<Chat> CreateChat(User user1, User user2);
        Task<Chat> CreateChat(string userId1, string userId2);

        void DeleteChat(User user1, User user2);
        void DeleteChat(string userId1, string userId2);
        void DeleteChat(Chat chat);
        void DeleteChat(int chatId);

        void SendMessage(Chat chat, string message);

        Task<Chat> GetOrCreateChat(User user1, User user2);
        Task<Chat> GetOrCreateChat(string userId1, string userId2);

    }
}

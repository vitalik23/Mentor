using Mentor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mentor.Interfaces
{
    public interface IChatService
    {

        void CreateChat(User user1, User user2);
        void CreateChat(string userId1, string userId2);

        void DeleteChat(User user1, User user2);
        void DeleteChat(string userId1, string userId2);
        void DeleteChat(Chat chat);
        void DeleteChat(int chatId);

        void SendMessage(Chat chat, string message);

        Chat GetOrCreateChat(User user1, User user2);
        Chat GetOrCreateChat(string userId1, string userId2);

    }
}

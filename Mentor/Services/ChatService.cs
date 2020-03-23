using Mentor.Interfaces;
using Mentor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mentor.Services
{
    public class ChatService : IChatService
    {
        public void CreateChat(User user1, User user2)
        {
            throw new NotImplementedException();
        }

        public void CreateChat(string userId1, string userId2)
        {
            throw new NotImplementedException();
        }

        public void DeleteChat(User user1, User user2)
        {
            throw new NotImplementedException();
        }

        public void DeleteChat(string userId1, string userId2)
        {
            throw new NotImplementedException();
        }

        public void DeleteChat(Chat chat)
        {
            throw new NotImplementedException();
        }

        public void DeleteChat(int chatId)
        {
            throw new NotImplementedException();
        }

        public Chat GetOrCreateChat(User user1, User user2)
        {
            throw new NotImplementedException();
        }

        public Chat GetOrCreateChat(string userId1, string userId2)
        {
            throw new NotImplementedException();
        }

        public void SendMessage(Chat chat, string message)
        {
            throw new NotImplementedException();
        }

        Chat IChatService.CreateChat(User user1, User user2)
        {
            throw new NotImplementedException();
        }

        Chat IChatService.CreateChat(string userId1, string userId2)
        {
            throw new NotImplementedException();
        }
    }
}

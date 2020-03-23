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

        private DataBaseContext _dataBaseContext;
        private IFileService _fileService;

        public ChatService(DataBaseContext dataBaseContext, IFileService fileService) 
        {
            _dataBaseContext = dataBaseContext;
            _fileService = fileService;
        }

        public async Task<Chat> CreateChat(User user1, User user2)
        {
            return await CreateChat(user1.Id, user2.Id);
        }

        public async Task<Chat> CreateChat(string userId1, string userId2)
        {
            Chat chat = new Chat
            { 
                User1Id = userId1,
                User2Id = userId2
            };

            await _dataBaseContext.Chat.AddAsync(chat);
            await _dataBaseContext.SaveChangesAsync();

            _fileService.CreateChatFile(chat);

            return chat;
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

        public async Task<Chat> GetOrCreateChat(User user1, User user2)
        {
            return await GetOrCreateChat(user1.Id, user2.Id);
        }

        public async Task<Chat> GetOrCreateChat(string userId1, string userId2)
        {

            // check if chat exists
            Chat chat = _dataBaseContext.Chat
                .FirstOrDefault(x => (x.User1Id == userId1 && x.User2Id == userId2) 
                                   || x.User1Id == userId2 && x.User2Id == userId1);

            if (chat == null) 
            {
                chat = await CreateChat(userId1, userId2);
            }

            // create folder

            return chat;
        }

        public void SendMessage(Chat chat, string message)
        {
            throw new NotImplementedException();
        }

        
    }
}

using Mentor.Interfaces;
using Mentor.Models;
using Mentor.ViewModels.ChatRelated;
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

         //   _fileService.CreateChatFile(chat);

            return chat;
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
            Chat chat = GetChat(userId1, userId2);

            if (chat == null) 
            {
                chat = await CreateChat(userId1, userId2);
            }

            return chat;
        }


        public List<Message> GetMessagesByChat(Chat chat)
        {
            List<Message> messages = new List<Message>(_dataBaseContext.Message.Where(x => x.ChatId == chat.Id).ToList());
            return messages;
        }

        public Chat GetChat(User user1, User user2) => GetChat(user1.Id, user2.Id);

        public Chat GetChat(string userId1, string userId2)
        {
            return _dataBaseContext.Chat
                .FirstOrDefault(x => (x.User1Id == userId1 && x.User2Id == userId2)
                                   || x.User1Id == userId2 && x.User2Id == userId1);
        }

        public async Task<Message> AddMessage(Chat chat, MessageViewModel model)
        {
            Message message = new Message
            {
                Text = model.Message,
                CreationTime = DateTime.Now,
                ChatId = chat.Id,
                Direction = chat.User1Id == model.CurrentUserId
                
            };

            await _dataBaseContext.AddAsync(message);
            await _dataBaseContext.SaveChangesAsync();

            return message;
        }
    }
}

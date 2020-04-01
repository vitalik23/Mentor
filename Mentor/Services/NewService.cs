using Mentor.Interfaces;
using Mentor.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mentor.Services
{
    public class NewService : INewService
    {
        private DataBaseContext _dataBaseContext;

        public NewService(DataBaseContext dataBaseContext)
        {
            _dataBaseContext = dataBaseContext;
        }

        public async System.Threading.Tasks.Task  AddNew(New news)
        {
            await _dataBaseContext.New.AddAsync(news);
            await _dataBaseContext.SaveChangesAsync();
        }

        public void DeleteNew(int newId)
        {
            New nw = _dataBaseContext.New.FirstOrDefault(p => p.Id == newId);

            _dataBaseContext.New.Remove(nw);
            _dataBaseContext.SaveChanges();
        }
    }
}

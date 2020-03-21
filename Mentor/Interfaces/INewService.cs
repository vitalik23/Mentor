using Mentor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mentor.Interfaces
{
    public interface INewService
    {
        System.Threading.Tasks.Task AddNew(New news);
        void DeleteNew(int newId);
        
    }
}

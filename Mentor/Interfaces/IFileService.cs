using Mentor.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mentor.Interfaces
{
    public interface IFileService
    {
        System.Threading.Tasks.Task<bool> UploadNewAvatar(User user, IFormFile uploadFile);
        System.Threading.Tasks.Task DeleteAvatar(User user);

        System.Threading.Tasks.Task UploadTaskFile(Models.Task task, IFormFile uploadFile);
    }
}

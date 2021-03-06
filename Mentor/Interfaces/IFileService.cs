﻿using Mentor.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        System.Threading.Tasks.Task UploadTaskSolutionFile(N_To_N_TaskStudent model, IFormFile uploadFile);

        void CreateChatFile(Chat chat);

        void DeleteFile(string path);
        byte[] Download(string path);

    }
}

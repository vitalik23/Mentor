using Mentor.Interfaces;
using Mentor.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Mentor.Services
{
    public class FileService : IFileService
    {

        private IWebHostEnvironment _appEnvironment;
        private UserManager<User> _userManager;

        public FileService(IWebHostEnvironment appEnvironment, UserManager<User> userManager) 
        {
            _appEnvironment = appEnvironment;
            _userManager = userManager;
        }

        public async System.Threading.Tasks.Task DeleteAvatar(User user)
        {

            string lastAvatar = _appEnvironment.WebRootPath + user.AvatarPath;
            if (user.AvatarPath != "" && File.Exists(lastAvatar))
            {
                File.Delete(lastAvatar);
            }

            user.AvatarPath = "";
            await _userManager.UpdateAsync(user);

        }

        public async System.Threading.Tasks.Task<bool> UploadNewAvatar(User user, IFormFile uploadedFile)
        {

            if (uploadedFile != null)
            {

                string path = _appEnvironment.WebRootPath + "/files/" + user.Id + "/";

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                string lastAvatar = _appEnvironment.WebRootPath + user.AvatarPath;

                if (user.AvatarPath != "" && File.Exists(lastAvatar)) 
                {

                    File.Delete(lastAvatar);

                }
                
                using (FileStream fileStream = new FileStream(path + uploadedFile.FileName, FileMode.Create))
                {
                    await uploadedFile.CopyToAsync(fileStream);
                }

                user.AvatarPath = "/files/" + user.Id + "/" + uploadedFile.FileName;

                IdentityResult result = await _userManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    return true;
                }
                else 
                {
                    return false;
                }

            }

            return false;
        }
    }
}

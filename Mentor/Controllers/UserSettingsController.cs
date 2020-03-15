using Mentor.Interfaces;
using Mentor.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mentor.Controllers
{


    public class UserSettingsController : Controller
    {

        IAuthentication _authentication;
        IFileService _fileService;


        public UserSettingsController(IAuthentication authentication, IFileService fileService)
        {
            _authentication = authentication;
            _fileService = fileService;
        }

        public async Task<IActionResult> AvatarSettings()
        {
            User currentUser = await _authentication.GetCurrentUserAsync();
            return View(currentUser);
        }

        [HttpPost]
        public async Task<IActionResult> UploadAvatar(IFormFile uploadedFile) {
            User currentUser = await _authentication.GetCurrentUserAsync();

            bool res = await _fileService.UploadNewAvatar(currentUser, uploadedFile);

            return RedirectToAction("AvatarSettings");
        }

        [HttpGet]
        public async Task<IActionResult> DeleteAvatar()
        {
            User currentUser = await _authentication.GetCurrentUserAsync();

            await _fileService.DeleteAvatar(currentUser);
            return RedirectToAction("AvatarSettings");
        }

    }
}

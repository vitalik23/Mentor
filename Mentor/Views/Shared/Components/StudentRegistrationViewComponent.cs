using Mentor.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mentor.Views.Shared.Components
{
    public class StudentRegistrationViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            // await db
             return View(new RegisterStudentViewModel());
        }
    }
}

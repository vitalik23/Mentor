using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mentor.ViewModels
{
    public class EditUserViewModel
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string Surname { get; set; }

        public string Name { get; set; }

        public string Patronymic { get; set; }

        public string PhoneNumber { get; set; }

        public bool IsAccepted { get; set; }
    }
}

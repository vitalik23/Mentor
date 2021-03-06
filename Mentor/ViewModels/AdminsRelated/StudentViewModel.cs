﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Mentor.ViewModels.AdminsRelated
{
    public class StudentViewModel
    {
        public string Email { get; set; }

        [Required]
        [Display(Name = "Имя")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Фамилия")]
        public string Surname { get; set; }

        [Required]
        [Display(Name = "Отчество")]
        public string Patronymic { get; set; }

        [Required]
        [Display(Name = "Телефонный номер")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        public string AvatarPath { get; set; }

        public string GroupName { get; set; }

        public int GroupId { get; set; }

        public List<SelectListItem> GroupItems { get; set; }

        public bool IsAccepted { get; set; }
    }
}

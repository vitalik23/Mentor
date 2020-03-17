using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Mentor.Models
{
    public class Subject
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Название курса")]
        [StringLength(100, ErrorMessage = "Поле {0} должно иметь минимум {2}.", MinimumLength = 5)]
        public string Name { get; set; }
    }
}

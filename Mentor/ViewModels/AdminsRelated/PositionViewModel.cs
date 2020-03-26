using Mentor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mentor.ViewModels.AdminsRelated
{
    public class PositionViewModel
    {
        public string Name { get; set; }

        public IEnumerable<Position> AllPosition { get; set; }
    }
}

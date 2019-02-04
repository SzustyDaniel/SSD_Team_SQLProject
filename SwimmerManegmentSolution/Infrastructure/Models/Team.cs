using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Models
{
    public class Team
    {
        public int TeamID { get; set;}
        public string TeamName { get; set; }
        public int MinimumAge { get; set; }
        public int MaximumAge { get; set; }
        public string Competitive { get; set; }
    }
}

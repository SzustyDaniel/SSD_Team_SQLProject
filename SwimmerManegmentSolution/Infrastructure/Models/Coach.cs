using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Models
{
    public class Coach
    {
        public int CoachID { get; set; }
        public string CoachName { get; set; }
        public string Address { get; set; }
        public int Achievement { get; set; }
        public int Salary { get; set; }
        public DateTime StartDateOfWork { get; set; }
        public string TrainingDiploma { get; set; }
    }
}

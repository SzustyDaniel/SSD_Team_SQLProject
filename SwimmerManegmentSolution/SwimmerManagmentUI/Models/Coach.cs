using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwimmerManagmentUI.Models
{
    public class Coach
    {
        public int CoachID { get; set; }
        public string CoachName { get; set; }
        public string Address { get; set; }
        public int Salary { get; set; }
        public DateTime StartDateOfWork { get; set; }
        public string TrainingDiploma { get; set; }

        public Coach(int id, string name, string address, int salary, DateTime startdate, string training)
        {
            CoachID = id;
            CoachName = name;
            Address = address;
            Salary = salary;
            StartDateOfWork = startdate;
            TrainingDiploma = training;
        }

        public override string ToString()
        {
            return $"Coach info: ID - {CoachID}, Name - {CoachName}, Address - {Address}, Salary - {Salary}, Start date of work - {StartDateOfWork.ToShortDateString()}, Training Diploma - {TrainingDiploma}.";
        }
    }
}

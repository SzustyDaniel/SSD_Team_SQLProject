using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure;
using SwimmerManagmentUI.Commands;

namespace SwimmerManagmentUI.ViewModels
{
    public class MainWindowViewModel: BaseViewModel
    {
        private string constring = @"Data Source=DESKTOP-OJCM7B5\SQLEXPRESS;Initial Catalog = ISwim; Integrated Security = True";
        private string query1 = @"SELECT CoachID,CoachName,C_Address AS [Address],Achievement,Salary,StartDateOfWork,TrainingDiploma FROM tblCoach;";

        private ObservableCollection<Coach> _coaches;
        public ObservableCollection<Coach> Coaches { get { return _coaches; } set {if(_coaches == value)return; _coaches = value; OnPropertyChanged(); } }

        private ObservableCollection<Team> _teams;
        public ObservableCollection<Team> Teams { get { return _teams; } set { if (_teams == value) return; _teams = value; OnPropertyChanged(); } }

        private RelayCommand<int> getTeamsCommand = null;
        public RelayCommand<int> GetTeamsCmd => getTeamsCommand ?? (getTeamsCommand = new RelayCommand<int>(GetTeamsCommand));

        private void GetTeamsCommand(int id)
        {
            string teamq = $"SELECT TeamID,TeamName,MinimumAge,MaximumAge,Competitive FROM tblTeam WHERE(Coach ={id});";

            Teams = new ObservableCollection<Team>(SqlHelper.GetAllRowsFromDb<Team>(constring,teamq));
        }

        public MainWindowViewModel()
        {
            Coaches = new ObservableCollection<Coach>(SqlHelper.GetAllRowsFromDb<Coach>(constring, query1)); 
        }


    }
}

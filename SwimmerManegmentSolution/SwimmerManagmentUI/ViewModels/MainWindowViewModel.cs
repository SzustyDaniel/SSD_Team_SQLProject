using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure;
using SwimmerManagmentUI.Commands;
using System.Data.SqlClient;
using SwimmerManagmentUI.Queries;
using System.Windows.Data;

namespace SwimmerManagmentUI.ViewModels
{
    public class MainWindowViewModel: BaseViewModel
    {
        private readonly string connectionString;
        public string ConnectionString => connectionString;

        private ObservableCollection<Coach> _coaches;
        public ObservableCollection<Coach> Coaches { get { return _coaches; } set {if(_coaches == value)return; _coaches = value; OnPropertyChanged(); } }

        private ObservableCollection<Team> _teams;
        public ObservableCollection<Team> Teams {
            get { return _teams; }
            set
            {
                if (_teams == value) return;
                _teams = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<StupidClass> _specialSwimmers;
        public ObservableCollection<StupidClass> SpecialSwimmers
        {
            get { return _specialSwimmers; }
            set
            {
                if (_specialSwimmers == value) return;
                _specialSwimmers = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<RegularSwimmer> _regularSwimmers;
        public ObservableCollection<RegularSwimmer> RegularSwimmers
        {
            get { return _regularSwimmers; }
            set
            {
                if (_regularSwimmers == value) return;
                _regularSwimmers = value;
                OnPropertyChanged();
            }
        }

        /*
        private RelayCommand<Coach> getTeamsCommand = null;
        public RelayCommand<Coach> GetTeamsCmd => getTeamsCommand ?? (getTeamsCommand = new RelayCommand<Coach>(GetTeamsCommand));

        private void GetTeamsCommand(Coach selected)
        {
            if(selected != null)
            {
                SqlParameter parameter = new SqlParameter("coachId", selected.CoachID);
                Teams = new ObservableCollection<Team>(SqlHelper.GetAllRowsFromDb<Team>(ConnectionString, QueriesManager.GetTeamsForCoach, parameter));
            }
        }

        private RelayCommand getAllCoacheCommand = null;
        public RelayCommand GetAllCoachesCmd => getAllCoacheCommand ?? (getAllCoacheCommand = new RelayCommand(GetAllCoachesCommand));

        
        private void GetAllCoachesCommand()
        {
            Coaches = new ObservableCollection<Coach>(SqlHelper.GetAllRowsFromDb<Coach>(ConnectionString, QueriesManager.GetAllCoaches));
        }
        */

        /// <summary>
        /// Base constructor for the view model
        /// </summary>
        public MainWindowViewModel()
        {
            connectionString = SqlConnectionStringHelper.ConnectionStringInAppConfig;
        }

    }
}

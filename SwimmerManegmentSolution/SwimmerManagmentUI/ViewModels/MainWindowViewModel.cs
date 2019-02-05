using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure;
using SwimmerManagmentUI.Commands;
using Infrastructure.Queries;
using System.Data.SqlClient;
using System.Windows.Data;

namespace SwimmerManagmentUI.ViewModels
{
    public class MainWindowViewModel: BaseViewModel
    {
        private readonly string connectionString;
        private ListCollectionView collectionView;
        public ListCollectionView CollectionView { get { return collectionView; } set {if(collectionView == value)return; collectionView = value; OnPropertyChanged(); } }

        private ObservableCollection<Coach> _coaches;
        public ObservableCollection<Coach> Coaches { get { return _coaches; } set {if(_coaches == value)return; _coaches = value; OnPropertyChanged(); } }

        private ObservableCollection<Team> _teams;
        public ObservableCollection<Team> Teams {
            get { return _teams; }
            set
            {
                if (_teams == value) return;
                _teams = value;
                OnPropertyChanged("Teams");
            }
        }

        private RelayCommand<Coach> getTeamsCommand = null;
        public RelayCommand<Coach> GetTeamsCmd => getTeamsCommand ?? (getTeamsCommand = new RelayCommand<Coach>(GetTeamsCommand));

        private void GetTeamsCommand(Coach selected)
        {
            if(selected != null)
            {
                SqlParameter parameter = new SqlParameter("coachId", selected.CoachID);
                Teams = new ObservableCollection<Team>(SqlHelper.GetAllRowsFromDb<Team>(connectionString, QueriesManager.GetTeamsForCoach, parameter));
                CollectionView = (ListCollectionView)CollectionViewSource.GetDefaultView(Teams);
            }
        }

        private RelayCommand getAllCoacheCommand = null;
        public RelayCommand GetAllCoachesCmd => getAllCoacheCommand ?? (getAllCoacheCommand = new RelayCommand(GetAllCoachesCommand));

        private void GetAllCoachesCommand()
        {
            Coaches = new ObservableCollection<Coach>(SqlHelper.GetAllRowsFromDb<Coach>(connectionString, QueriesManager.GetAllCoaches));
            CollectionView = (ListCollectionView)CollectionViewSource.GetDefaultView(Coaches);
        }


        /// <summary>
        /// Base constructor for the view model
        /// </summary>
        public MainWindowViewModel()
        {
            connectionString = SqlConnectionStringHelper.ConnectionStringInAppConfig;
        }

    }
}

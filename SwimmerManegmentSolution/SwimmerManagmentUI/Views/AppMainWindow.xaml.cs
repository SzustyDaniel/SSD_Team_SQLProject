using Infrastructure;
using Infrastructure.Models;
using SwimmerManagmentUI.Queries;
using SwimmerManagmentUI.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SwimmerManagmentUI.Views
{
    /// <summary>
    /// Interaction logic for AppMainWindow.xaml
    /// </summary>
    public partial class AppMainWindow : Window
    {
        public ViewModels.MainWindowViewModel ViewModel { get; set; }

        public AppMainWindow()
        {
            InitializeComponent();
            ViewModel = new ViewModels.MainWindowViewModel();
            this.DataContext = ViewModel;
        }

        private async void GetAllCoachesClick(object sender, RoutedEventArgs e)
        {
            List<Coach> result;
            try
            {
                result = await SqlClientHelper.Get<Coach>();
                ViewModel.Coaches = new ObservableCollection<Coach>(result);
                dgCoaches.ItemsSource = ViewModel.Coaches;
            }
            catch
            {
                MessageBox.Show("Error!!");
            }
        }

        private void DgCoaches_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Coach coach = dgCoaches.SelectedItem as Coach;
            if (coach != null)
            {
                SqlParameter parameter = new SqlParameter("coachId", coach.CoachID);
                ViewModel.Teams = new ObservableCollection<Team>(SqlHelper.GetAllRowsFromDb<Team>(ViewModel.ConnectionString, QueriesManager.GetTeamsForCoach, parameter));
                dgTeams.ItemsSource = ViewModel.Teams;
            }
        }
    }
}

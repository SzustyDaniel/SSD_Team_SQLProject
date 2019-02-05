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

        //Get all the coaches for the data gird
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

        //Get all the teams that a selected coach has
        private async void DgCoaches_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Coach coach = dgCoaches.SelectedItem as Coach;
            if (coach != null)
            {
                try
                {
                    SqlParameter parameter = new SqlParameter("coachId", coach.CoachID);
                    List<Team> teams = await SqlClientHelper.Get<Team>(parameter);
                    ViewModel.Teams = new ObservableCollection<Team>(teams);
                    dgTeams.ItemsSource = ViewModel.Teams;
                }
                catch
                {
                    MessageBox.Show("Error!!");
                }
            }
        }

        // Get all the Regular females and Potential from the last 6 months
        private async void GetRPSwimmers_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                List<StupidClass> stupids = await SqlClientHelper.Get<StupidClass>(new SqlParameter("monthsToadd",-6),new SqlParameter("gender","female"));
                ViewModel.SpecialSwimmers = new ObservableCollection<StupidClass>(stupids);
                dgFemalePotential.ItemsSource = ViewModel.SpecialSwimmers;
            }
            catch
            {
                MessageBox.Show("Error!");
            }
        }

        // Get all the Regulars that didn't had any training 
        private async void GetLazyRegulars_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                List<RegularSwimmer> regulars = await SqlClientHelper.Get<RegularSwimmer>();
                ViewModel.RegularSwimmers = new ObservableCollection<RegularSwimmer>(regulars);
                dgLazyRegulars.ItemsSource = ViewModel.RegularSwimmers;
            }
            catch
            {
                MessageBox.Show("Error!");
            }
        }

        private void BtnSearchCoach_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(txbSearchCoach.Text, out int id) && dgCoaches.Items.Count > 0)
            {
                Coach item = dgCoaches.Items.OfType<Coach>().FirstOrDefault(c => c.CoachID == id);
                if (item == null) return;
                dgCoaches.ScrollIntoView(item);
                dgCoaches.SelectedItem = item;
            }
        }
    }
}

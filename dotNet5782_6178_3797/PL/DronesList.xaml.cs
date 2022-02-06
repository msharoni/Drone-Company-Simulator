using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PL
{
    /// <summary>
    /// Interaction logic for DronesList.xaml
    /// </summary>
    public partial class DronesList : Page
    {
        private BO.DroneStatuses? OptionOne = null;
        private BO.WeightCategories? OptionTwo = null;
        private BlApi.IBL logic;
        public DronesList(BlApi.IBL _logic)
        {
            InitializeComponent();

            logic = _logic;

            DroneListView.ItemsSource = logic.DisplayDrones(null,null);

            FirstCombo.ItemsSource = Enum.GetValues(typeof(BO.DroneStatuses));
            SecondCombo.ItemsSource = Enum.GetValues(typeof(BO.WeightCategories));

        }
        private void FirstCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)//combolist for drone statuses
        {
            OptionOne = (BO.DroneStatuses)FirstCombo.SelectedItem;
            DroneListView.ItemsSource = logic.DisplayDrones(OptionOne, OptionTwo);
        }

        private void SecondCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            OptionTwo = (BO.WeightCategories)SecondCombo.SelectedItem;
            DroneListView.ItemsSource = logic.DisplayDrones(OptionOne, OptionTwo);
        }

        private void DroneListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
           BO.DroneForList drone = (BO.DroneForList)((ListView)sender).SelectedItem;
           Drone droneWindow = new Drone(drone.Id);
           droneWindow.Show();
        }

        private void AddDroneButton_Click(object sender, RoutedEventArgs e)
        {
            Drone win = new Drone(); //without a paramter => add mode!
            win.parentRefresh = () => { DroneListView.Items.Refresh(); };
            win.Show();
        }
    }
}// wait a mintute

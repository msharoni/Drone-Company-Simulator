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
        private BL.DroneStatuses? OptionOne = null;
        private BL.WeightCategories? OptionTwo = null;
        private BL.IBL logic;
        public DronesList(BL.IBL _logic)
        {
            InitializeComponent();

            logic = _logic;

            DroneListView.ItemsSource = logic.DisplayDrones();

            FirstCombo.ItemsSource = Enum.GetValues(typeof(BL.DroneStatuses));
            SecondCombo.ItemsSource = Enum.GetValues(typeof(BL.WeightCategories));

        }
        private void FirstCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            OptionOne = (BL.DroneStatuses)FirstCombo.SelectedItem;
            DroneListView.ItemsSource = logic.FilteredDisplayDrones(OptionOne, OptionTwo);
        }

        private void SecondCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            OptionTwo = (BL.WeightCategories)SecondCombo.SelectedItem;
            DroneListView.ItemsSource = logic.FilteredDisplayDrones(OptionOne, OptionTwo);
        }

        private void DroneListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
           BL.DroneForList drone = (BL.DroneForList)((ListView)sender).SelectedItem;
            Drone droneWindow = new Drone(drone);
            droneWindow.Show();
        }

        private void AddDroneButton_Click(object sender, RoutedEventArgs e)
        {
            new Drone(); //without a paramter => add mode!
        }
    }
}// wait a mintute

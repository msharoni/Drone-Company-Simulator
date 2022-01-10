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
using System.Windows.Shapes;
using BL;
namespace PL
{
    /// <summary>
    /// Interaction logic for Drone.xaml
    /// </summary>
    public partial class Drone : Window
    {
        BlApi.IBL blObject;
        int Id;
        BL.WeightCategories Weight;
        string Model;
        int StationId;
        public Drone(BO.DroneForList drone = null)
        {
            InitializeComponent();
            FirstCombo.DataContext = Enum.GetValues(typeof(WeightCategories));
            if (drone == null) // add mode
            {
                //yes. thts it.
            }
            else // options mode
            {
                MainGrid.DataContext = blObject.DisplayDrone(drone.Id); //emm mor should take care of that
            }
        }
        private void IdTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if ((int.TryParse(IdTextBox.Text, out Id) && Id > 0))
            {
                IdTextBox.Background = Brushes.White;
            }
            else
            {
                IdTextBox.Background = Brushes.Red;
            }
        }
        private void StationIdTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if ((int.TryParse(StationIdTextBox.Text, out StationId) && StationId >= 0))
            {
                StationIdTextBox.Background = Brushes.White;
            }
            else
            {
                StationIdTextBox.Background = Brushes.Red;
            }
        }
        private void FirstCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Weight = (WeightCategories)((ComboBox)sender).SelectedItem;
        }
        private void ModelTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            Model = ModelTextBox.Text;
        }

        private void AddDroneButton_Click(object sender, RoutedEventArgs e)
        {
            blObject.AddDrone(Id, Model, Weight, StationId);
        }
    }
}
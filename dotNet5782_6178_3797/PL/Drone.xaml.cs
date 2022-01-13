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
using BO;
namespace PL
{
    /// <summary>
    /// Interaction logic for Drone.xaml
    /// </summary>
    public partial class Drone : Window
    {
        BlApi.IBL blObject = BlApi.BlFactory.GetBl();
        int Id;
        BO.WeightCategories Weight;
        string Model;
        int StationId;
        BO.DroneForList OurDrone;
        public Drone(BO.DroneForList drone = null)
        {
            InitializeComponent();
            OurDrone = drone;
            FirstCombo.DataContext = Enum.GetValues(typeof(WeightCategories));
            if (drone == null) // add mode
            {
                MainGrid.Visibility = Visibility.Visible;
                DroneGrid.Visibility = Visibility.Collapsed;
            }
            else // options mode
            {
                DroneGrid.DataContext = blObject.DisplayDrone(drone.Id);
                DroneGrid.Visibility = Visibility.Visible;
                MainGrid.Visibility = Visibility.Collapsed;//emm mor should take care of that
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

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ChargeDroneButton_Click(object sender, RoutedEventArgs e)
        {
            blObject.ChargeDrone(OurDrone.Id);
        }

        private void Model_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (OptionsModelTextBox.Text != OurDrone.Model)
            {
                Model = OptionsModelTextBox.Text;
                UpdateModel.Visibility = Visibility.Visible;
            }
        }

        private void UpdateModel_Click(object sender, RoutedEventArgs e)
        {
            blObject.UpdateDrone(OurDrone.Id, OptionsModelTextBox.Text);
        }
    }
}
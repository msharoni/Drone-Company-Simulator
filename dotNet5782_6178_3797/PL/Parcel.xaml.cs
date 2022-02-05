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
using BO;
namespace PL
{
    /// <summary>
    /// Interaction logic for Parcel.xaml
    /// </summary>
    public partial class Parcel : Window
    {
        BlApi.IBL blObject = BlApi.BlFactory.GetBl();
        BO.Parcel OurParcel;
        public WeightCategories? Weight = null;
        public Priorities? Priority = null;
        int AddParcelid;
        public Parcel(int? parcelId = null)
        {
            InitializeComponent();
            if(parcelId == null)
            {
                AddParcel.Visibility = Visibility.Visible;
                MainGrid.Visibility = Visibility.Collapsed;
                AddWeightValueComboBox.DataContext = Enum.GetValues(typeof(BO.WeightCategories));
                AddPriorityValueComboBox.DataContext = Enum.GetValues(typeof(BO.Priorities));
                SenderIdValueComboBox.DataContext = blObject.DisplayCustomers();
                TargetIdValueComboBox.DataContext = blObject.DisplayCustomers();
            }
            else
            {
                AddParcel.Visibility = Visibility.Collapsed;
                MainGrid.Visibility = Visibility.Visible;
                OurParcel = blObject.DisplayParcel((int)parcelId);
                MainGrid.DataContext = blObject.DisplayParcel((int)parcelId);
                WeightValueComboBox.ItemsSource = Enum.GetValues(typeof(BO.WeightCategories));
                PriorityValueComboBox.ItemsSource = Enum.GetValues(typeof(BO.Priorities));
            }
            
        }
        private void SenderButton_Click(object sender, RoutedEventArgs e)
        {
            new Customer(OurParcel.Sender.Id).Show();
        }
        private void ReceiverButton_Click(object sender, RoutedEventArgs e)
        {
            new Customer(OurParcel.Sender.Id).Show();
        }
        private void WeightValueComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Weight = (WeightCategories)((ComboBox)sender).SelectedItem;

        }
        private void PriorityValueComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Priority = (Priorities)((ComboBox)sender).SelectedItem;
        }
        private void DroneButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                new Drone(OurParcel.Drone.Id).Show();
            }
            catch(Exception me)
            {
                MessageBox.Show(me.ToString());
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void AddWeightValueComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void AddPriorityValueComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void TargetIdValueComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void SenderIdValueComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void AddParcelButton_Click(object sender, RoutedEventArgs e)
        {
            int SenderId = ((CustomerForList)(SenderIdValueComboBox.SelectedItem)).Id;
            int TargetId = ((CustomerForList)(TargetIdValueComboBox.SelectedItem)).Id;
            WeightCategories weight = (WeightCategories)(AddWeightValueComboBox.SelectedItem);
            Priorities p = (Priorities)(AddPriorityValueComboBox.SelectedItem);
            blObject.AddParcel(AddParcelid, SenderId, TargetId, weight, p);
            this.Close();
        }

        private void AddParcelId_TextChanged(object sender, TextChangedEventArgs e)
        {
            if ((int.TryParse(AddParcelId.Text, out AddParcelid) && AddParcelid >= 0))
            {
                AddParcelId.Background = Brushes.White;
            }
            else
            {
                AddParcelId.Background = Brushes.Red;
            }
        }
    }
}
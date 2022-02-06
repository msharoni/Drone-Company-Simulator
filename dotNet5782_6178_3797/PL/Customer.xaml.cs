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

namespace PL
{
    /// <summary>
    /// Interaction logic for Custumer.xaml
    /// </summary>
    public partial class Customer : Window
    {
        BlApi.IBL blObject = BlApi.BlFactory.GetBl();
        //customers option
        int Id;
        string Name;
        int Phone;
        int Longitude, Latitude;
        BO.Customer OurCustomer;
        public Customer(int? customerId = null)
        {
            InitializeComponent();
            if (customerId == null) // add mode
            {
                AddCutomer.Visibility = Visibility.Visible;
                MainGrid.Visibility = Visibility.Collapsed;
            }
            else //normal customer mode
            {
                MainGrid.Visibility = Visibility.Visible;
                AddCutomer.Visibility = Visibility.Collapsed;
                OurCustomer = blObject.DisplayCustomer((int)customerId);
                MainGrid.DataContext = blObject.DisplayCustomer((int)customerId);
            }
        }
        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)//move window option
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }
        private void Close_Click(object sender, RoutedEventArgs e)//close x
        {
            this.Close();
        }

        private void UpdateName_Click(object sender, RoutedEventArgs e)//update the name
        {
            try
            {
                blObject.UpdateCustomer(OurCustomer.Id, NameTextBox.Text, OurCustomer.phone);
                new Customer(OurCustomer.Id).Show();
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void NameTextBox_TextChanged(object sender, TextChangedEventArgs e)//name of customer
        {
            if (NameTextBox.Text != OurCustomer.name)
            {
                Name = NameTextBox.Text;
                UpdateName.Visibility = Visibility.Visible;
            }
        }

        private void ForCustomer_MouseDoubleClick(object sender, MouseButtonEventArgs e)//foe customer parcels
        {
            BO.ParcelInCustomer parcel = (BO.ParcelInCustomer)((ListView)sender).SelectedItem;
            Parcel parcelWindow = new Parcel(parcel.Id);
            parcelWindow.Show();
        }

        private void FromCustomer_MouseDoubleClick(object sender, MouseButtonEventArgs e)//From customer parcels
        {
            BO.ParcelInCustomer parcel = (BO.ParcelInCustomer)((ListView)sender).SelectedItem;
            Parcel parcelWindow = new Parcel(parcel.Id);
            parcelWindow.Show();
        }

        private void PhoneTextBox_TextChanged(object sender, TextChangedEventArgs e)//Phone changed
        {
            if ((int.TryParse(PhoneTextBox.Text, out Phone) && Phone > 0))
            {
                PhoneTextBox.Background = Brushes.White;
            }
            else
            {
                PhoneTextBox.Background = Brushes.Red;
            }
        }

        private void LongitudeTextBox_TextChanged(object sender, TextChangedEventArgs e)//location longatude
        {
            if ((int.TryParse(LongitudeTextBox.Text, out Longitude) && Longitude >= 0 && Longitude<=180))
            {
                LongitudeTextBox.Background = Brushes.White;
            }
            else
            {
                LongitudeTextBox.Background = Brushes.Red;
            }
        }

        private void LatitudeTextBox_TextChanged(object sender, TextChangedEventArgs e)//location latitude
        {
            if ((int.TryParse(LatitudeTextBox.Text, out Latitude) && Latitude >= 0 && Latitude <= 180))
            {
                LatitudeTextBox.Background = Brushes.White;
            }
            else
            {
                LatitudeTextBox.Background = Brushes.Red;
            }
        }

        private void AddCustomerButton_Click(object sender, RoutedEventArgs e) //Adding Customer
        {
            try
            {
                BO.Location location = new BO.Location { Lattitude = Latitude, Longitude = Longitude };
                blObject.AddCustomer(Id, Name, Phone.ToString(), location );
                Close();
            }
            catch (Exception ex)//message box
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void AddNameTextBox_TextChanged(object sender, TextChangedEventArgs e)//Defineing a parameter
        {
            Name = AddNameTextBox.Text;
        }

        private void IdTextBox_1_TextChanged(object sender, TextChangedEventArgs e)//Id text box
        {
            if ((int.TryParse(IdTextBox_1.Text, out Id) && Id > 0))
            {
                IdTextBox_1.Background = Brushes.White;
            }
            else
            {
                IdTextBox_1.Background = Brushes.Red;
            }
        }
    }
}
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
        int Id;
        string Name;
        BO.CustomerForList OurCustomer;
        public Customer(BO.CustomerForList customer = null)
        {
            InitializeComponent();
            OurCustomer = customer;
            MainGrid.DataContext = blObject.DisplayCustomer(customer.Id);
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

        private void UpdateName_Click(object sender, RoutedEventArgs e)
        {

        }

        private void NameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}

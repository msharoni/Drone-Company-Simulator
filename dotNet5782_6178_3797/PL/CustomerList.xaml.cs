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
    /// Interaction logic for CustomerList.xaml
    /// </summary>
    public partial class CustomerList : Page
    {
        BlApi.IBL BLObject = BlApi.BlFactory.GetBl();
        public CustomerList()
        {
            InitializeComponent();
            CustomerListView.ItemsSource = BLObject.DisplayCustomers();
        }

        private void CustomerListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            BO.CustomerForList customer = (BO.CustomerForList)((ListView)sender).SelectedItem;
            Customer CustomerWindow = new Customer(customer.Id);
            CustomerWindow.Show();
        }

        private void AddCustomerButton_Click(object sender, RoutedEventArgs e)
        {
            new Customer().Show();
        }
    }
}
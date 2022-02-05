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
    /// Interaction logic for ParcelList.xaml
    /// </summary>
    public partial class ParcelList : Page
    {
        BlApi.IBL BLObject = BlApi.BlFactory.GetBl();
        public ParcelList()
        {
            InitializeComponent();
            ParcelListView.ItemsSource = BLObject.DisplayParcels();
        }
        private void ParcelListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            BO.ParcelForList parcel = (BO.ParcelForList)((ListView)sender).SelectedItem;
            Parcel parcelWindow = new Parcel(parcel.Id);
            parcelWindow.Show();
        }
        private void AddParcelButton_Click(object sender, RoutedEventArgs e)
        {
            Parcel parcelWindow = new Parcel();
            parcelWindow.Show();
        }
    }
}
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
    /// Interaction logic for StationList.xaml
    /// </summary>
    public partial class StationList : Page
    {
        BlApi.IBL BLObject = BlApi.BlFactory.GetBl();
        public StationList()
        {
            InitializeComponent();
            StationListView.ItemsSource = BLObject.DisplayStations();
        }

        private void SortList_Click(object sender, RoutedEventArgs e)
        {
            var stationsList = BLObject.DisplayStations();
            var result = from station in stationsList
                         orderby station.NumOfVacantChargers descending
                         select station;
            StationListView.ItemsSource = result;
        }

        private void StationListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            BO.StationForList station = (BO.StationForList)((ListView)sender).SelectedItem;
            Station StationWindow = new Station(station);
            StationWindow.Show();
        }
    }
}

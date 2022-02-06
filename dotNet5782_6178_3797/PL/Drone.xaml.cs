using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        BO.Drone OurDrone;
        DateTime x;
        BackgroundWorker backgroundWorker;
        bool closingPending = false;
        public Action parentRefresh;
        public Drone(int? droneId = null)
        {
            InitializeComponent();

            
            
            FirstCombo.DataContext = Enum.GetValues(typeof(WeightCategories));

            if (droneId == null) // add mode
            {
                MainGrid.Visibility = Visibility.Visible;
                DroneGrid.Visibility = Visibility.Collapsed;
            }
            else // options mode
            {
                Id = (int)droneId;
                OurDrone = blObject.DisplayDrone((int)droneId);
                DroneGrid.DataContext = blObject.DisplayDrone(OurDrone.Id);
                DroneGrid.Visibility = Visibility.Visible;
                MainGrid.Visibility = Visibility.Collapsed;//emm mor should take care of this
                if (OurDrone.Status == DroneStatuses.Available)
                {
                    ChargeDroneButton.Visibility = Visibility.Visible;
                    LinktoParcel.Visibility = Visibility.Visible;
                }
                else if (OurDrone.Status == DroneStatuses.maintenance)
                {
                    UnchargeDroneButton.Visibility = Visibility.Visible;
                }
                if (OurDrone.Parcel!=null && OurDrone.Parcel.Id != -1)
                {
                    BO.Parcel DroneParcel = blObject.DisplayParcel(OurDrone.Parcel.Id);
                    if (DroneParcel.Linked != null && DroneParcel.PickedUp == null)
                    {
                        PickupParcelButton.Visibility = Visibility.Visible;
                    }
                    else if (DroneParcel.PickedUp != null && DroneParcel.Delivered == null)
                    {
                        DeliverParcelButton.Visibility = Visibility.Visible;
                    } 
                }
                backgroundWorker = new BackgroundWorker();
                backgroundWorker.WorkerReportsProgress = true;
                backgroundWorker.WorkerSupportsCancellation = true;
                backgroundWorker.DoWork += startSimulator;
                backgroundWorker.ProgressChanged += refresh;
                backgroundWorker.RunWorkerCompleted += complete;
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
            try
            {
                blObject.AddDrone(Id, Model, Weight, StationId);
                if (parentRefresh is not null) parentRefresh();
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            if (backgroundWorker is not null && backgroundWorker.IsBusy)
            {
                closingPending = true;
                backgroundWorker.CancelAsync();
            }
            else
            {
                Close();
            }
        }

        private void ChargeDroneButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                blObject.ChargeDrone(OurDrone.Id);
                new Drone(OurDrone.Id).Show();
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            x = DateTime.Now;
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
            try
            {
                blObject.UpdateDrone(OurDrone.Id, OptionsModelTextBox.Text);
                new Drone(OurDrone.Id).Show();
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void UnchargeDroneButton_Click(object sender, RoutedEventArgs e)
        {
            DateTime now = DateTime.Now;
            try
            {
                blObject.UnChargeDrone(OurDrone.Id, (now - x).TotalSeconds);
                new Drone(OurDrone.Id).Show();
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void LinktoParcel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                blObject.LinkDroneToParcel(OurDrone.Id);
                new Drone(OurDrone.Id).Show();
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void PickupParcel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                blObject.PickUpParcel(OurDrone.Id);
                new Drone(OurDrone.Id).Show();
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void DeliverParcelButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                blObject.DeliverParcel(OurDrone.Id);
                new Drone(OurDrone.Id).Show();
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            blObject.ActivateSimulator(OurDrone.Id, () =>   backgroundWorker.ReportProgress(1), () => backgroundWorker.CancellationPending);
        }
        private void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            OurDrone.CurrentLocation = blObject.DisplayDrone(OurDrone.Id).CurrentLocation;
            OurDrone.Battery = blObject.DisplayDrone(OurDrone.Id).Battery;
            OurDrone.Parcel.Id = blObject.DisplayDrone(OurDrone.Id).Parcel.Id;
            OurDrone.Status = blObject.DisplayDrone(OurDrone.Id).Status;
            this.DataContext = OurDrone;
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (!backgroundWorker.IsBusy)
                backgroundWorker.RunWorkerAsync();
        }
        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            backgroundWorker.CancelAsync();
        }

        void startSimulator(object? sender, DoWorkEventArgs e)
        {
            blObject.ActivateSimulator(Id, () => backgroundWorker.ReportProgress(0), () => backgroundWorker.CancellationPending);
        }
        void refresh(object? sender, ProgressChangedEventArgs e)
        {
            OurDrone = blObject.DisplayDrone(Id);
            DroneGrid.DataContext = blObject.DisplayDrone(OurDrone.Id);
        }
        void complete(object? sender, RunWorkerCompletedEventArgs e)
        {
            if (closingPending)
                Close();
        }
    }
}

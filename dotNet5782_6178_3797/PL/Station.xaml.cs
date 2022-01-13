﻿using System;
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
    /// Interaction logic for Station.xaml
    /// </summary>
    public partial class Station : Window
    {
        BlApi.IBL blObject = BlApi.BlFactory.GetBl();
        int Id;
        string Name;
        BO.StationForList OurStation;

        public Station(BO.StationForList station = null)
        {
            InitializeComponent();
            OurStation = station;
            MainGrid.DataContext = blObject.DisplayStation(station.Id);
        }
        private void Name_TextChanged(object sender, TextChangedEventArgs e)
        {
            Name = NameTextBox.Text;
        }
        private void NameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (NameTextBox.Text != OurStation.Name)
            {
                Name = NameTextBox.Text;
                UpdateName.Visibility = Visibility.Visible;
            }
        }
        private void UpdateName_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                blObject.UpdateStation(OurStation.Id, NameTextBox.Text, (OurStation.NumOfVacantChargers + OurStation.NumOfOccupiedChargers));
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
            this.Close();
        }
    }
}

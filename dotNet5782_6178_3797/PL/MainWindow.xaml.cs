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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DronesList DronesList;
        BlApi.IBL BLObject = BlApi.BlFactory.GetBl();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void dronesListButton_Click(object sender, RoutedEventArgs e)
        {
            currentPage.Content = new DronesList(BLObject);
            
        }

        private void currentPage_Navigated(object sender, NavigationEventArgs e)
        {

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

        private void StationListButton_Click(object sender, RoutedEventArgs e)
        {
            currentPage.Content = new StationList();
        }

        private void CustomerListButton_Click(object sender, RoutedEventArgs e)
        {
            currentPage.Content = new CustomerList();
        }

        private void ParcelListButton_Click(object sender, RoutedEventArgs e)
        {
            currentPage.Content = new ParcelList();
        }
    }
}

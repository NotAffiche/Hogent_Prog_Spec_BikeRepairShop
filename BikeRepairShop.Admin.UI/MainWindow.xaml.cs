﻿using BikeRepairShop.Admin.UI.Model;
using BikeRepairShopBL.Domain;
using BikeRepairShopBL.DTO;
using BikeRepairShopBL.Managers;
using BikeRepairShopDL.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
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

namespace BikeRepairShop.Admin.UI;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private CustomerBikeManager customerBikeManager;
    private ObservableCollection<BikeUI> bikes;
    private CustomerBikeRepository customerBikeRepo;

    private ObservableCollection<string> customers; //TODO: replace with DTO (like BikeInfo)

    public MainWindow()
    {
        InitializeComponent();
        BikeDataGrid.IsReadOnly= true;
        string conn = ConfigurationManager.ConnectionStrings["ADOconnSQL"].ConnectionString;
        customerBikeRepo = new CustomerBikeRepository(conn);
        customerBikeManager = new CustomerBikeManager(customerBikeRepo);
        bikes= new ObservableCollection<BikeUI>(customerBikeManager.GetBikesInfo().Select(x=>new BikeUI(x.Id, x.Description, x.BikeType, x.PurchaseCost, x.Customer.id, x.Customer.custDesc)));
        customers = new ObservableCollection<string>(new List<string>() { "jos", "janine", "ivo"});
        BikeDataGrid.ItemsSource = bikes;
    }

    private void MenuItemAddBike_Click(object sender, RoutedEventArgs e)
    {
        WindowBike windowBike = new WindowBike(customerBikeManager);

        windowBike.CBCustomer.ItemsSource = customers;

        if (windowBike.ShowDialog() == true)
        {
            bikes.Add(windowBike.Bike);
        }
        
    }

    private void MenuItemDeleteBike_Click(object sender, RoutedEventArgs e)
    {
        BikeUI bike = (BikeUI)BikeDataGrid.SelectedItem;
        if (bike == null) MessageBox.Show("No selection", "Bike");
        else
        {
            if (MessageBox.Show("Are you sure?", "Delete bike", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                BikeUI bikeUI = (BikeUI)BikeDataGrid.SelectedItem;
                bikes.Remove(bikeUI);
                //TODO: DL
            }
        }
    }

    private void MenuItemUpdateBike_Click(object sender, RoutedEventArgs e)
    {
        WindowBike windowBike = new WindowBike(customerBikeManager, true);

        BikeUI bike = (BikeUI)BikeDataGrid.SelectedItem;
        if (bike == null) MessageBox.Show("No selection", "Bike");
        else
        {
            windowBike.Bike= bike;

            windowBike.ShowDialog();
        }
    }

    private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
    {
        Application.Current.Shutdown();
    }
}

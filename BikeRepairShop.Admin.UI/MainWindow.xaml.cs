﻿using BikeRepairShop.Admin.UI.Mappers;
using BikeRepairShop.Admin.UI.Model;
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
    private CustomerBikeRepository customerBikeRepo;
    private RepairmanManager repairmanManager;
    private RepairmanRepository repairmanRepository;

    public ObservableCollection<BikeUI> bikes;
    public ObservableCollection<CustomerUI> customers;
    public ObservableCollection<RepairmanUI> repairmen;

    public MainWindow()
    {
        InitializeComponent();
        CustomerDataGrid.IsReadOnly = true;
        BikeDataGrid.IsReadOnly = true;
        string conn = ConfigurationManager.ConnectionStrings["ADOconnSQL"].ConnectionString;
        customerBikeRepo = new CustomerBikeRepository(conn);
        customerBikeManager = new CustomerBikeManager(customerBikeRepo);
        repairmanRepository = new RepairmanRepository(conn);
        repairmanManager = new RepairmanManager(repairmanRepository);
        LoadGrids();
    }

    public void LoadGrids()
    {
        bikes = new ObservableCollection<BikeUI>(customerBikeManager.GetBikesInfo().Select(x => new BikeUI(x.Id, x.Description, x.BikeType, x.PurchaseCost, x.Customer.id, x.Customer.custDesc)));
        customers = new ObservableCollection<CustomerUI>(customerBikeManager.GetCustomersInfo().Select(x => new CustomerUI(x.Id, x.Name, x.Email, x.Address, x.NrOfBikes, x.TotalBikeValues)));
        repairmen = new ObservableCollection<RepairmanUI>(repairmanManager.GetRepairmanInfos().Select(x=>new RepairmanUI(x.Id, x.Name, x.Email, x.CostPerHour)));
        BikeDataGrid.ItemsSource = bikes;
        CustomerDataGrid.ItemsSource = customers;
        RepairmenDataGrid.ItemsSource = repairmen;
    }

    #region customers
    private void MenuItemAddCustomer_Click(object sender, RoutedEventArgs e)
    {
        WindowCustomer windowCustomer = new WindowCustomer(customerBikeManager, this);
        windowCustomer.Customer = new CustomerUI();
        windowCustomer.ShowDialog();
    }
    private void MenuItemDeleteCustomer_Click(object sender, RoutedEventArgs e)
    {
        CustomerUI cust = (CustomerUI)CustomerDataGrid.SelectedItem;
        if (cust == null) MessageBox.Show("No selection", "Customer");
        else
        {
            if (MessageBox.Show("Are you sure?", "Delete customer", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                CustomerUI customerUI = (CustomerUI)CustomerDataGrid.SelectedItem;
                customerBikeManager.DeleteCustomer(CustomerMapper.ToDTO(customerUI));
            }
        }
        LoadGrids();
    }
    private void MenuItemUpdateCustomer_Click(object sender, RoutedEventArgs e)
    {
        WindowCustomer windowCustomer = new WindowCustomer(customerBikeManager, this, true);

        CustomerUI customer = (CustomerUI)CustomerDataGrid.SelectedItem;
        if (customer == null) MessageBox.Show("No selection", "Customer");
        else
        {
            windowCustomer.Customer = customer;
            windowCustomer.ShowDialog();
        }
    }
    #endregion

    #region bikes
    private void MenuItemAddBike_Click(object sender, RoutedEventArgs e)
    {
        WindowBike windowBike = new WindowBike(customerBikeManager, this);
        windowBike.CBCustomer.ItemsSource = customers;
        windowBike.Bike = new BikeUI();
        windowBike.ShowDialog();
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
                customerBikeManager.DeleteBike(BikeMapper.ToDTO(bikeUI));
            }
        }
    }
    private void MenuItemUpdateBike_Click(object sender, RoutedEventArgs e)
    {
        WindowBike windowBike = new WindowBike(customerBikeManager, this, true);

        BikeUI bike = (BikeUI)BikeDataGrid.SelectedItem;
        if (bike == null) MessageBox.Show("No selection", "Bike");
        else
        {
            windowBike.Bike = bike;
            windowBike.ShowDialog();
        }
    }
    #endregion

    #region repairmen
    private void MenuItemAddRepairman_Click(object sender, RoutedEventArgs e)
    {
        WindowRepairman windowRepairman = new WindowRepairman(repairmanManager, this);
        windowRepairman.Repairman = new RepairmanUI();
        windowRepairman.ShowDialog();
    }
    private void MenuItemDeleteRepairman_Click(object sender, RoutedEventArgs e)
    {
        RepairmanUI repairman = (RepairmanUI)BikeDataGrid.SelectedItem;
        if (repairman == null) MessageBox.Show("No selection", "Repairman");
        else
        {
            if (MessageBox.Show("Are you sure?", "Delete repairman", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                RepairmanUI repairmanUI = (RepairmanUI)RepairmenDataGrid.SelectedItem;
                repairmanManager.DeleteRepairman(RepairmanMapper.ToDTO(repairmanUI));
            }
        }
        LoadGrids();
    }
    private void MenuItemUpdateRepairman_Click(object sender, RoutedEventArgs e)
    {
        WindowRepairman repairmanWindow = new WindowRepairman(repairmanManager, this, true);

        RepairmanUI repairman = (RepairmanUI)RepairmenDataGrid.SelectedItem;
        if (repairman == null) MessageBox.Show("No selection", "Bike");
        else
        {
            repairmanWindow.Repairman = repairman;
            repairmanWindow.ShowDialog();
        }
    }
    #endregion

    private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
    {
        Application.Current.Shutdown();
    }
}

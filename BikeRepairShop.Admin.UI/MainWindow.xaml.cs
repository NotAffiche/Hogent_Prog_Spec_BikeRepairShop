using BikeRepairShop.Admin.UI.Mappers;
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
    public CustomerBikeManager customerBikeManager;
    public RepairmanManager repairmanManager;
    public RepairOrderManager repairOrderManager;

    public ObservableCollection<BikeUI> bikes;
    public ObservableCollection<CustomerUI> customers;
    public ObservableCollection<RepairmanUI> repairmen;
    public ObservableCollection<RepairTaskUI> repairTasks;
    public ObservableCollection<RepairOrderUI> repairOrders;
    public ObservableCollection<RepairOrderItemUI> repairOrderItems;

    public MainWindow()
    {
        InitializeComponent();
        CustomerDataGrid.IsReadOnly = true;
        BikeDataGrid.IsReadOnly = true;
        string conn = ConfigurationManager.ConnectionStrings["ADOconnSQL"].ConnectionString;
        customerBikeManager = new CustomerBikeManager(new CustomerBikeRepository(conn));
        repairmanManager = new RepairmanManager(new RepairmanRepository(conn));
        repairOrderManager = new RepairOrderManager(new RepairOrderRepository(conn), new CustomerBikeRepository(conn), new RepairmanRepository(conn));
        LoadGrids();
    }

    public void LoadGrids()
    {
        bikes = new ObservableCollection<BikeUI>(customerBikeManager.GetBikesInfo().Select(x => new BikeUI(x.Id, x.Description, x.BikeType, x.PurchaseCost, x.Customer.id, x.Customer.custDesc)));
        customers = new ObservableCollection<CustomerUI>(customerBikeManager.GetCustomersInfo().Select(x => new CustomerUI(x.Id, x.Name, x.Email, x.Address, x.NrOfBikes, x.TotalBikeValues)));
        repairmen = new ObservableCollection<RepairmanUI>(repairmanManager.GetRepairmanInfos().Select(x=>new RepairmanUI(x.Id, x.Name, x.Email, x.CostPerHour)));
        repairTasks = new ObservableCollection<RepairTaskUI>(repairOrderManager.GetRepairTaskInfos().Select(x=>new RepairTaskUI(x.Id, x.Description, x.RepairTime, x.CostMaterials)));
        BikeDataGrid.ItemsSource = bikes;
        CustomerDataGrid.ItemsSource = customers;
        RepairmenDataGrid.ItemsSource = repairmen;

        RepairmenDataGrid.IsReadOnly = true;
        RepairOrdersDataGrid.IsReadOnly = true;
        RepairOrderItemsDataGrid.IsReadOnly = true;
        RepairOrdersDataGrid.ItemsSource = null;
        RepairOrderItemsDataGrid.ItemsSource = null;
        //repairorder cb customer
        CBCustomer.DisplayMemberPath = "Name";
        CBCustomer.SelectedValuePath = "ID";
        CBCustomer.ItemsSource = customers;
    }

    public void Unfocus()
    {
        CBCustomer.SelectedItem = null;
    }

    private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
    {
        Application.Current.Shutdown();
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
        if (repairman == null) MessageBox.Show("No selection", "Repairman");
        else
        {
            repairmanWindow.Repairman = repairman;
            repairmanWindow.ShowDialog();
        }
    }
    #endregion

    #region repairorders
    public CustomerUI? cUI;
    private void CBCustomer_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        cUI = (CustomerUI)CBCustomer.SelectedItem;
        if (cUI!=null)
        {
            CustomerInfo ci = CustomerMapper.ToDTO(cUI);
            repairOrders = new ObservableCollection<RepairOrderUI>(repairOrderManager.GetRepairOrderInfos(ci).Select(x => new RepairOrderUI(x.ID, x.Urgency, x.OrderDate, x.Paid)));
            RepairOrdersDataGrid.ItemsSource = repairOrders;
            RepairOrderItemsDataGrid.ItemsSource = null;
        }
    }
    private void RepairOrdersDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        //load repair order items for this repair order
        RepairOrderUI roUI = (RepairOrderUI)RepairOrdersDataGrid.SelectedItem;
        if (roUI!= null)
        {
            cUI = (CustomerUI)CBCustomer.SelectedItem;
            RepairOrderInfo roi = new RepairOrderInfo(roUI.ID, roUI.Urgency, roUI.Paid, roUI.OrderDate, cUI.ID, $"{cUI.Name} ({cUI.Email})");
            repairOrderItems = new ObservableCollection<RepairOrderItemUI>(repairOrderManager.GetRepairOrderItemInfos(roi).Select(x => new RepairOrderItemUI(x.ID, x.RepairOrder.RepairOrderId, x.RepairOrder.OrderDate, 
                x.RepairOrder.Urgency, x.Bike.BikeId, x.Bike.BikeType, x.Bike.Description, x.RepairTask.RepairTaskId, x.RepairTask.RepairTime, x.RepairTask.Description, x.RepairTask.CostMaterials, x.Repairman.RepairmanId, x.Repairman.Name, 
                x.Repairman.Email, x.Repairman.CostPerHour)));
            RepairOrderItemsDataGrid.ItemsSource = repairOrderItems;
        }
    }

    private void MenuItemAddRepairOrder_Click(object sender, RoutedEventArgs e)
    {
        if ((CustomerUI)CBCustomer.SelectedItem!=null)
        {
            WindowRepairOrder windowRepairOrder = new WindowRepairOrder(repairOrderManager, this, (CustomerUI)CBCustomer.SelectedItem);
            windowRepairOrder.RepairOrder = new RepairOrderUI();
            windowRepairOrder.Show();
        }
        else
        {
            MessageBox.Show("Select a customer", "Repair Order");
        }
    }
    private void MenuItemDeleteRepairOrder_Click(object sender, RoutedEventArgs e)
    {
        RepairOrderUI repairOrder = (RepairOrderUI)RepairOrdersDataGrid.SelectedItem;
        if (repairOrder == null) MessageBox.Show("No selection", "Repair order");
        else
        {
            if (MessageBox.Show("Are you sure?", "Delete repair order", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                repairOrderManager.RemoveRepairOrder(RepairOrderMapper.OrderToDTO(repairOrder, cUI));
            }
        }
        LoadGrids();
    }
    private void MenuItemUpdateRepairOrder_Click(object sender, RoutedEventArgs e)
    {
        WindowRepairOrder windowRepairOrder = new WindowRepairOrder(repairOrderManager, this, (CustomerUI)CBCustomer.SelectedItem, true);

        RepairOrderUI repairOrder = (RepairOrderUI)RepairOrdersDataGrid.SelectedItem;
        if (repairOrder == null) MessageBox.Show("No selection", "Repair Order item");
        else
        {
            windowRepairOrder.cUI = (CustomerUI)CBCustomer.SelectedItem;
            windowRepairOrder.RepairOrder = repairOrder;
            windowRepairOrder.ShowDialog();
        }
    }

    //repairorder items
    private void MenuItemAddRepairOrderItem_Click(object sender, RoutedEventArgs e)
    {
        if ((CustomerUI)CBCustomer.SelectedItem != null)
        {
            WindowRepairOrderItem windowRepairOrderItem = new WindowRepairOrderItem(repairOrderManager, this, (CustomerUI)CBCustomer.SelectedItem, (RepairOrderUI)RepairOrdersDataGrid.SelectedItem);
            windowRepairOrderItem.RepairOrderItem = new RepairOrderItemUI();
            windowRepairOrderItem.Show();
        }
        else
        {
            MessageBox.Show("Select a customer and repairorder", "Repair Order");
        }
    }
    private void MenuItemDeleteRepairOrderItem_Click(object sender, RoutedEventArgs e)
    {
        RepairOrderItemUI repairOrderItem = (RepairOrderItemUI)RepairOrderItemsDataGrid.SelectedItem;
        if (repairOrderItem == null) MessageBox.Show("No selection", "Repair order item");
        else
        {
            if (MessageBox.Show("Are you sure?", "Delete repair order item", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                RepairOrderItemInfo roiInfo = RepairOrderMapper.OrderItemToDTO(repairOrderItem, cUI);
                repairOrderManager.RemoveRepairOrderItem(roiInfo);
            }
        }
        LoadGrids();
    }
    private void MenuItemUpdateRepairOrderItem_Click(object sender, RoutedEventArgs e)
    {
        WindowRepairOrderItem windowRepairOrderItem = new WindowRepairOrderItem(repairOrderManager, this, (CustomerUI)CBCustomer.SelectedItem, (RepairOrderUI)RepairOrdersDataGrid.SelectedItem, true);

        RepairOrderItemUI repairOrderItem = (RepairOrderItemUI)RepairOrderItemsDataGrid.SelectedItem;
        if (repairOrderItem == null) MessageBox.Show("No selection", "Repair Order Item");
        else
        {
            windowRepairOrderItem.cUI = (CustomerUI)CBCustomer.SelectedItem;
            windowRepairOrderItem.RepairOrderItem = repairOrderItem;
            windowRepairOrderItem.ShowDialog();
        }
    }
    #endregion
}

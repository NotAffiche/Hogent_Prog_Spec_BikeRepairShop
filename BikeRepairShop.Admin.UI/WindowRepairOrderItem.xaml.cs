using BikeRepairShop.Admin.UI.Mappers;
using BikeRepairShop.Admin.UI.Model;
using BikeRepairShopBL.Domain;
using BikeRepairShopBL.Managers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace BikeRepairShop.Admin.UI;

/// <summary>
/// Interaction logic for WindowRepairOrderItem.xaml
/// </summary>
public partial class WindowRepairOrderItem : Window
{
    public RepairOrderItemUI RepairOrderItem { get; set; }
    private bool update;
    private RepairOrderManager repairOrderManager;
    private MainWindow parentWindow;
    public CustomerUI cUI;
    public RepairOrderUI roUI;

    public WindowRepairOrderItem(RepairOrderManager repairOrderManager, MainWindow parent, CustomerUI cUI, RepairOrderUI roUI, bool update = false)
    {
        InitializeComponent();
        parentWindow = parent;
        this.repairOrderManager = repairOrderManager;
        this.update = update;
        this.cUI = cUI;
        this.roUI = roUI;
    }

    private void CancelRepairOrderItemButton_Click(object sender, RoutedEventArgs e)
    {
        Close();
    }

    private void SaveRepairOrderItemButton_Click(object sender, RoutedEventArgs e)
    {
        BikeUI bike = (BikeUI)CBBike.SelectedItem;
        RepairTaskUI repairTask = (RepairTaskUI)CBRepairTask.SelectedItem;
        RepairmanUI repairman = (RepairmanUI)CBRepairman.SelectedItem;
        if (bike == null || repairTask == null || repairman == null) MessageBox.Show("Make sure to select bike repairtask repairman");
        else
        {
            RepairOrderItem.RepairOrderId = (int)roUI.ID!;
            RepairOrderItem.RepairOrder = (roUI.OrderDate, roUI.Urgency);
            RepairOrderItem.BikeId = (int)bike.ID!;
            RepairOrderItem.Bike = (bike.BikeType.ToString(), bike.Description);
            RepairOrderItem.RepairTaskId = (int)repairTask.ID!;
            RepairOrderItem.RepairTask = (repairTask.RepairTime, repairTask.Description, repairTask.CostMaterials);
            RepairOrderItem.RepairmanId = (int)repairman.ID!;
            RepairOrderItem.Repairman = (repairman.Name, repairman.Email, repairman.CostPerHour);
            if (update)
            {//update
                RepairOrderItem.ID = int.Parse(TBId.Text);
                repairOrderManager.UpdateRepairOrderItem((RepairOrderMapper.OrderItemToDTO(RepairOrderItem, cUI)));
            }
            else
            {//add
            repairOrderManager.AddRepairOrderItem(RepairOrderMapper.OrderItemToDTO(RepairOrderItem, cUI));
            }
            parentWindow.LoadGrids();
            Close();
        }
    }

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
        try
        {
            if (cUI == null) throw new NullReferenceException("no selected customer");
            ObservableCollection<BikeUI> bikesPerCustomer = new ObservableCollection<BikeUI>(parentWindow.customerBikeManager
                .GetBikesInfo((int)cUI.ID!).Select(x=>new BikeUI(x.Id, x.Description, x.BikeType, x.PurchaseCost, x.Customer.id, x.Customer.custDesc)));
            CBBike.DisplayMemberPath = "DisplayName";
            CBBike.SelectedValuePath = "ID";
            CBBike.ItemsSource = bikesPerCustomer;
            CBRepairTask.DisplayMemberPath = "DisplayName";
            CBRepairTask.SelectedValuePath = "ID";
            CBRepairTask.ItemsSource = parentWindow.repairTasks;
            CBRepairman.DisplayMemberPath = "DisplayName";
            CBRepairman.SelectedValuePath = "ID";
            CBRepairman.ItemsSource = parentWindow.repairmen;
            if (update)
            {//update
                TBId.Text = RepairOrderItem.ID.ToString();
                BikeUI selectedBike = bikesPerCustomer.FirstOrDefault(b => b.ID == RepairOrderItem.BikeId)!;
                CBBike.SelectedItem = selectedBike;
                RepairTaskUI selectedTask = parentWindow.repairTasks.FirstOrDefault(x => x.ID == RepairOrderItem.RepairTaskId)!;
                CBRepairTask.SelectedItem = selectedTask;
                RepairmanUI selectedRepairman = parentWindow.repairmen.FirstOrDefault(x => x.ID == RepairOrderItem.RepairmanId)!;
                CBRepairman.SelectedItem = selectedRepairman;
            }
            else
            {//create
                TBId.Text = "<generated>";
            }
        }
        catch
        {
            MessageBox.Show("An error occured. Make sure to (re)select an repair order.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}

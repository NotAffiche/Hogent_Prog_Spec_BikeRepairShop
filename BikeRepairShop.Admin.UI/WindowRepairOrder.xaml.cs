using BikeRepairShop.Admin.UI.Mappers;
using BikeRepairShop.Admin.UI.Model;
using BikeRepairShopBL.Domain;
using BikeRepairShopBL.Managers;
using Microsoft.VisualBasic;
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
using System.Windows.Shapes;
using System.Xml.Linq;

namespace BikeRepairShop.Admin.UI;

/// <summary>
/// Interaction logic for WindowRepairOrder.xaml
/// </summary>
public partial class WindowRepairOrder : Window
{

    public RepairOrderUI RepairOrder { get; set; }
    private bool update;
    private RepairOrderManager repairOrderManager;
    private MainWindow parentWindow;
    private List<string> urgency = new List<string>() { "Normal", "NoRush", "Fast" };
    public CustomerUI cUI;

    public WindowRepairOrder(RepairOrderManager repairOrderManager, MainWindow parent, CustomerUI cUI, bool update = false)
    {
        InitializeComponent();
        parentWindow = parent;
        this.repairOrderManager = repairOrderManager;
        this.update = update;
        this.cUI = cUI;
    }

    private void CancelRepairOrderButton_Click(object sender, RoutedEventArgs e)
    {
        Close();
    }

    private void SaveRepairOrderButton_Click(object sender, RoutedEventArgs e)
    {
        RepairOrder.Urgency = CBUrgency.SelectedItem.ToString()!;
        RepairOrder.Paid = (bool)CheckPaid.IsChecked!;
        if (update)
        {//update
            //repairOrderManager.UpdateRepairOrder((RepairOrderMapper.OrderToDTO(RepairOrder, cUI));
        }
        else
        {//add
            RepairOrder.OrderDate = DateOnly.FromDateTime(DateTime.Now);
            repairOrderManager.AddRepairOrder(RepairOrderMapper.OrderToDTO(RepairOrder, cUI));
        }
        parentWindow.LoadGrids();
        Close();
    }

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
        try
        {
            if (cUI == null) throw new NullReferenceException("no selected customer");
            CBUrgency.ItemsSource = urgency;
            TBCustomer.Text = cUI.CustomerDescription;
            if (update)
            {//update
                TBId.Text = RepairOrder.ID.ToString();
                TBOrderDate.Text = RepairOrder.OrderDate.ToString();
                CBUrgency.SelectedItem = RepairOrder.Urgency;
                CheckPaid.IsChecked = RepairOrder.Paid;
            }
            else
            {//create
                TBId.Text = "<generated>";
                CBUrgency.SelectedIndex = 0;
                TBOrderDate.Text = DateOnly.FromDateTime(DateTime.Now).ToString();
            }
        }
        catch
        {
            MessageBox.Show("An error occured. Make sure to (re)select an repair order / customer.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}

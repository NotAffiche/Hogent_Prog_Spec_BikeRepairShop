using BikeRepairShop.Admin.UI.Mappers;
using BikeRepairShop.Admin.UI.Model;
using BikeRepairShopBL.Domain;
using BikeRepairShopBL.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
/// Interaction logic for WindowRepairman.xaml
/// </summary>
public partial class WindowRepairman : Window
{
    public RepairmanUI Repairman { get; set; }
    private bool update;
    private RepairmanManager repairmanManager;
    private MainWindow parentWindow;

    public WindowRepairman(RepairmanManager repairmanManager, MainWindow parent, bool update = false)
    {
        InitializeComponent();
        parentWindow = parent;
        this.repairmanManager = repairmanManager;
        this.update = update;
    }

    private void CancelRepairmanButton_Click(object sender, RoutedEventArgs e)
    {
        Close();
    }

    private void SaveRepairmanButton_Click(object sender, RoutedEventArgs e)
    {
        Repairman.Name = TBName.Text;
        Repairman.Email = TBEmail.Text;
        Repairman.CostPerHour = double.Parse(TBCostPerHour.Text);
        if (update)
        {//update
            repairmanManager.UpdateRepairman(RepairmanMapper.ToDTO(Repairman));
        }
        else
        {//add
            repairmanManager.AddRepairman(RepairmanMapper.ToDTO(Repairman));
        }
        DialogResult = true;
        parentWindow.LoadGrids();
        Close();
    }

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
        if (update)
        {//update
            TBId.Text = Repairman.ID.ToString();
            TBName.Text = Repairman.Name;
            TBEmail.Text = Repairman.Email;
            TBCostPerHour.Text = Repairman.CostPerHour.ToString();
        }
        else
        {//create
            TBId.Text = "<generated>";
        }
    }
}

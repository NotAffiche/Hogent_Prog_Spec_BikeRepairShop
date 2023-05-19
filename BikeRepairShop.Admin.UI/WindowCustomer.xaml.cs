using BikeRepairShop.Admin.UI.Mappers;
using BikeRepairShop.Admin.UI.Model;
using BikeRepairShopBL.Domain;
using BikeRepairShopBL.Managers;
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

namespace BikeRepairShop.Admin.UI;

/// <summary>
/// Interaction logic for WindowCustomer.xaml
/// </summary>
public partial class WindowCustomer : Window
{
    public CustomerUI Customer { get; set; }
    private bool update;
    private CustomerBikeManager customerBikeManager;
    private MainWindow parentWindow;

    public WindowCustomer(CustomerBikeManager customerBikeManager, MainWindow parent, bool update = false)
    {
        InitializeComponent();
        parentWindow = parent;
        this.customerBikeManager = customerBikeManager;
        this.update = update;
    }

    private void CancelCustomerButton_Click(object sender, RoutedEventArgs e)
    {
        Close();
    }

    private void SaveCustomerButton_Click(object sender, RoutedEventArgs e)
    {
        Customer.Name = TBName.Text;
        Customer.Address = TBAddress.Text;
        Customer.Email = TBEmail.Text;
        if (update)
        {//update
            customerBikeManager.UpdateCustomer(CustomerMapper.ToDTO(Customer));
        }
        else
        {//add
            customerBikeManager.AddCustomer(CustomerMapper.ToDTO(Customer));
        }
        DialogResult = true;
        parentWindow.LoadGrids();
        Close();
    }

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
        TBBikes.Text = $"count: {Customer.NrOfBikes}; total val: {Customer.TotalBikesValue}";
        if (update)
        {
            TBId.Text = Customer.ID.ToString();
            TBName.Text = Customer.Name;
            TBAddress.Text = Customer.Address;
            TBEmail.Text = Customer.Email;
        }
        else
        {
            TBId.Text = "<generated>";
        }
    }
}

using BikeRepairShop.Admin.UI.Mappers;
using BikeRepairShop.Admin.UI.Model;
using BikeRepairShopBL.Domain;
using BikeRepairShopBL.DTO;
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

namespace BikeRepairShop.Admin.UI
{
    /// <summary>
    /// Interaction logic for WindowBike.xaml
    /// </summary>
    public partial class WindowBike : Window
    {
        public BikeUI Bike { get; set; }
        private bool update;
        private CustomerBikeManager customerBikeManager;
        private MainWindow parentWindow;

        public WindowBike(CustomerBikeManager customerBikeManager, MainWindow parent, bool update=false)
        {
            InitializeComponent();
            parentWindow = parent;
            this.customerBikeManager= customerBikeManager;
            CBBikeType.ItemsSource = Enum.GetValues(typeof(BikeType)).Cast<BikeType>();
            CBCustomer.SelectedIndex = 0;
            CBBikeType.SelectedIndex = 0;
            TBId.IsReadOnly = true;
            this.update = update;
        }

        private void CancelBikeButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void SaveBikeButton_Click(object sender, RoutedEventArgs e)
        {
            Bike.Description = TBDescription.Text;
            Bike.BikeType = (BikeType)CBBikeType.SelectedItem;
            Bike.PurchaseCost = double.Parse(TBPurchaseCost.Text);
            if (update)
            {//update
                customerBikeManager.UpdateBike(BikeMapper.ToDTO(Bike));
            }
            else
            {//add
                if (CBCustomer.SelectedItem is CustomerUI selectedCustomer)
                {
                    Bike.CustomerId = (int)selectedCustomer.ID!;
                    Bike.CustomerDesc = $"{selectedCustomer.CustomerDescription}";
                    customerBikeManager.AddBike(BikeMapper.ToDTO(Bike));
                }
                else
                {
                    MessageBox.Show("No customer selected", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            DialogResult = true;
            parentWindow.LoadGrids();
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (update)
            {
                CBBikeType.SelectedItem = Bike.BikeType;
                TBDescription.Text= Bike.Description;
                TBId.Text = Bike.Id.ToString();
                TBPurchaseCost.Text = Bike.PurchaseCost.ToString();
                TBCustomer.Visibility = Visibility.Visible;
                CBCustomer.Visibility = Visibility.Collapsed;
                TBCustomer.Text = customerBikeManager.GetCustomer(Bike.CustomerId).Name;
            }
            else
            {
                CBCustomer.DisplayMemberPath = "Name";
                CBCustomer.SelectedValuePath = "Id";
                CBCustomer.ItemsSource = parentWindow.customers;
                TBCustomer.Visibility = Visibility.Collapsed;
                CBCustomer.Visibility = Visibility.Visible;
                TBId.Text = "<generated>";
            }
        }
    }
}

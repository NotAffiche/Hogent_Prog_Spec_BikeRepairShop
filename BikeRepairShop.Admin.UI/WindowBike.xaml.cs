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
        public WindowBike(CustomerBikeManager customerBikeManager, bool update=false)
        {
            InitializeComponent();
            this.customerBikeManager= customerBikeManager;
            CBBikeType.ItemsSource = Enum.GetValues(typeof(BikeType)).Cast<BikeType>();
            CBCustomer.SelectedIndex = 0;
            CBBikeType.SelectedIndex = 0;
            TBId.IsReadOnly = true;
            this.update = update;
            if (!update)
            {
                TBCustomer.Visibility = Visibility.Collapsed;
                CBCustomer.Visibility = Visibility.Visible;
            }
            else
            {
                TBCustomer.Visibility = Visibility.Visible;
                CBCustomer.Visibility = Visibility.Collapsed;
                TBId.Text = "<generated>";
            }
        }

        private void CancelBikeButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void SaveBikeButton_Click(object sender, RoutedEventArgs e)
        {
            if(update)
            {
                //customerManager.UpdateBike();
                Bike.Description = TBDescription.Text;
                Bike.BikeType = (BikeType)CBBikeType.SelectedItem;
                Bike.PurchaseCost = double.Parse(TBPurchaseCost.Text);
            }
            else//add
            {
                //TODO: save to DL
                //Bike = new BikeUI(69, "meat rub", BikeType.regularBike, 420.69, 4, "Adrian (a@b.c)");
                customerBikeManager.AddBike(BikeMapper.ToDTO(Bike));
            }
            DialogResult = true;
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (update)
            {
                TBCustomer.Text = "jos";
                CBBikeType.SelectedItem = Bike.BikeType;
                TBDescription.Text= Bike.Description;
                TBId.Text = Bike.Id.ToString();
                TBPurchaseCost.Text = Bike.PurchaseCost.ToString();
            }
        }
    }
}

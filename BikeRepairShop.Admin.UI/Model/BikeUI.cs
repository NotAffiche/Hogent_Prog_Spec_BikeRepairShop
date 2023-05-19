using BikeRepairShopBL.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeRepairShop.Admin.UI.Model;

public class BikeUI : INotifyPropertyChanged
{
    public BikeUI()
    {

    }
    public BikeUI(int? id, string desc, BikeType bikeType, double purchaseCost, int custId, string custDesc) {
        Id = id;
        Description = desc;
        BikeType = bikeType;
        PurchaseCost = purchaseCost;
        CustomerId= custId;
        CustomerDesc = custDesc;
    }

    public int? Id { get; set; }
    //
    private string _description;
    public string Description { get { return _description; } set { _description = value; OnPropertyChanged(); } }
    private BikeType _bikeType;
    public BikeType BikeType { get { return _bikeType; } set { _bikeType = value; OnPropertyChanged(); } }
    private double _purchaseCost;
    public double PurchaseCost { get { return _purchaseCost; } set { _purchaseCost = value; OnPropertyChanged(); } }
    public int CustomerId { get; set; }
    public string CustomerDesc { get; set; }

    public event PropertyChangedEventHandler? PropertyChanged;
    private void OnPropertyChanged(string name=null)
    {
        PropertyChanged?.Invoke(name, new PropertyChangedEventArgs(name));
    }
}

using BikeRepairShopBL.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeRepairShop.Admin.UI.Model;

public class CustomerUI : INotifyPropertyChanged
{
    public CustomerUI()
    {

    }
    public CustomerUI(int? id, string name, string email, string address, int nrOfBikes, double totalBikesValue)
    {
        ID = id;
        Name = name;
        Email = email;
        Address = address;
        NrOfBikes = nrOfBikes;
        TotalBikesValue = totalBikesValue;
    }

    public int? ID { get; set; }
    private string _name;
    public string Name { get { return _name; } set { _name = value; OnPropertyChanged(); } }
    private string _email;
    public string Email { get { return _email; } set { _email = value; OnPropertyChanged(); } }
    private string _address;
    public string Address { get { return _address; } set { _address = value; OnPropertyChanged(); } }
    private int _nrOfBikes;
    public int NrOfBikes { get { return _nrOfBikes; } set { _nrOfBikes= value; OnPropertyChanged(); } }
    private double _totalBikesValue;
    public double TotalBikesValue { get { return _totalBikesValue; } set { _totalBikesValue= value; OnPropertyChanged(); } }
    //private List<Bike> _bikes = new List<Bike>();
    public string CustomerDescription { get { return $"{Name} ({Email})"; } }

    //event
    public event PropertyChangedEventHandler? PropertyChanged;
    private void OnPropertyChanged(string name = null)
    {
        PropertyChanged?.Invoke(name, new PropertyChangedEventArgs(name));
    }
}

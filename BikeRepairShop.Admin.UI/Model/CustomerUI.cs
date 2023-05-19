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
    public CustomerUI(int? id, string name, string email, string address)
    {
        ID = id;
        Name = name;
        Email = email;
        Address = address;
    }

    public int? ID { get; set; }
    private string _name;
    public string Name { get { return _name; } set { _name = value; OnPropertyChanged(); } }
    private string _email;
    public string Email { get { return _email; } set { _email = value; OnPropertyChanged(); } }
    private string _address;
    public string Address { get { return _address; } set { _address = value; OnPropertyChanged(); } }

    //event
    public event PropertyChangedEventHandler? PropertyChanged;
    private void OnPropertyChanged(string name = null)
    {
        PropertyChanged?.Invoke(name, new PropertyChangedEventArgs(name));
    }
}

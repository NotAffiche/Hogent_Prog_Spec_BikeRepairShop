using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeRepairShop.Admin.UI.Model;

public class RepairmanUI : INotifyPropertyChanged
{
    public RepairmanUI()
    {

    }
    public RepairmanUI(int? id, string name, string email, double costPerHour)
    {
        ID = id;
        Name = name;
        Email = email;
        CostPerHour = costPerHour;
    }

    public int? ID { get; set; }
    private string _name;
    public string Name { get { return _name; } set { _name = value; OnPropertyChanged(); } }
    private string _email;
    public string Email { get { return _email; } set { _email = value; OnPropertyChanged(); } }
    private double _costPerHour;
    public double CostPerHour { get { return _costPerHour; } set { _costPerHour = value; OnPropertyChanged(); } }

    //event
    public event PropertyChangedEventHandler? PropertyChanged;
    private void OnPropertyChanged(string name = null)
    {
        PropertyChanged?.Invoke(name, new PropertyChangedEventArgs(name));
    }
}

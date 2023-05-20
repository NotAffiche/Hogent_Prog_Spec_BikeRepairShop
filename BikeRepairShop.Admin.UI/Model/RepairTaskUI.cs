using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeRepairShop.Admin.UI.Model;

public class RepairTaskUI : INotifyPropertyChanged
{
    public RepairTaskUI()
    {

    }
    public RepairTaskUI(int? id, string desc, int reptime, double costmat)
    {
        ID = id;
        Description = desc;
        RepairTime = reptime;
        CostMaterials = costmat;
    }

    public int? ID { get; set; }
    private string _description;
    public string Description { get { return _description; } set { _description = value; OnPropertyChanged(); } }
    private int _repairTime;
    public int RepairTime { get { return _repairTime; } set { _repairTime = value; OnPropertyChanged(); } }
    private double _costMaterials;
    public double CostMaterials { get { return _costMaterials; } set { _costMaterials = value; OnPropertyChanged(); } }

    //event
    public event PropertyChangedEventHandler? PropertyChanged;
    private void OnPropertyChanged(string name = null)
    {
        PropertyChanged?.Invoke(name, new PropertyChangedEventArgs(name));
    }
}

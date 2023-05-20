using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeRepairShop.Admin.UI.Model;

public class RepairOrderItemUI : INotifyPropertyChanged
{
    public RepairOrderItemUI()
    {

    }
    public RepairOrderItemUI(int? id, int repOrderId, DateOnly orderDate, string urgency, int bikeId, string bikeType, string? bikeDesc, int repTaskId, int repTime, string taskDesc, double matCost, 
        int repManId, string repManName, string repmanEmail, double repmancph)
    {
        ID = id;
        RepairOrderId = repOrderId;
        RepairOrder = (orderDate, urgency);
        BikeId = bikeId;
        Bike = (bikeType, bikeDesc);
        RepairTaskId = repTaskId;
        RepairTask = (repTime, taskDesc, matCost);
        RepairmanId = repManId;
        Repairman = (repManName, repmanEmail, repmancph);
    }

    public int? ID { get; set; }
    //
    internal int _repairOrderId;
    internal int RepairOrderId { get { return _repairOrderId; } set { _repairOrderId = value; OnPropertyChanged(); } }
    internal (DateOnly OrderDate, string Urgency) RepairOrder { get; set; }
    //
    internal int _bikeId;
    internal int BikeId { get { return _bikeId; } set { _bikeId = value; OnPropertyChanged(); } }
    public (string BikeType, string? Description) Bike { get; set; }
    //
    internal int _repairTaskId;
    internal int RepairTaskId { get { return _repairTaskId; } set { _repairTaskId = value; OnPropertyChanged(); } }
    public (int RepairTime, string Description, double CostMaterials) RepairTask { get; set; }
    //
    internal int _repairmanId;
    internal int RepairmanId { get { return _repairmanId; } set { _repairmanId = value; OnPropertyChanged(); } }
    public (string Name, string Email, double CostPerHour) Repairman { get; set; }

    #region event
    //event
    public event PropertyChangedEventHandler? PropertyChanged;
    private void OnPropertyChanged(string name = null)
    {
        PropertyChanged?.Invoke(name, new PropertyChangedEventArgs(name));
    }
    #endregion
}

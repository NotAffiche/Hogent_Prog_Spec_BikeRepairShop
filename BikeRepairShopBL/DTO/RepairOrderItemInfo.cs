using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeRepairShopBL.DTO;

public class RepairOrderItemInfo
{
    public RepairOrderItemInfo(int? id, int repOrderId, DateOnly orderDate, string urgency, int bikeId, string bikeType, string? bikeDesc, int repTaskId, int repTime, string taskDesc, double matCost, int repManId, string repManName, string repmanEmail, double repmancph)
    {
        ID = id;
        RepairOrder = (repOrderId, orderDate, urgency);
        Bike = (bikeId, bikeType, bikeDesc);
        RepairTask = (repTaskId, repTime, taskDesc, matCost);
        Repairman = (repManId, repManName, repmanEmail, repmancph);
    }

    public int? ID { get; set; }
    public (int RepairOrderId, DateOnly OrderDate, string Urgency) RepairOrder { get; set; }
    public (int BikeId, string BikeType, string? Description) Bike { get; set; }
    public (int RepairTaskId, int RepairTime, string Description, double CostMaterials) RepairTask { get; set; }
    public (int RepairmanId, string Name, string Email, double CostPerHour) Repairman { get; set; }
    public override string ToString()
    {
        return $"{ID}; ";
    }
}

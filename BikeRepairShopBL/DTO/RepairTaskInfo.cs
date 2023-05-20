using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeRepairShopBL.DTO;

public class RepairTaskInfo
{
    public RepairTaskInfo(int? id, string desc, int reptime, double cmat)
    {
        Id = id;
        Description = desc;
        RepairTime = reptime;
        CostMaterials = cmat;
    }

    public int? Id { get; set; }
    public string Description { get; set; }
    public int RepairTime { get; set; }
    public double CostMaterials { get; set; }

    public override bool Equals(object? obj)
    {
        return obj is RepairTaskInfo info &&
               Id == info.Id &&
               Description == info.Description &&
               RepairTime == info.RepairTime &&
               CostMaterials == info.CostMaterials;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Description, RepairTime, CostMaterials);
    }
}

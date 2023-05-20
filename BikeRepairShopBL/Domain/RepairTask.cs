using BikeRepairShopBL.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeRepairShopBL.Domain;

public class RepairTask
{
    public int? ID { get; private set; }
    public string Description { get; private set; }
    public int RepairTime { get; private set; }
    public double CostMaterials { get; private set; }

    internal RepairTask(string desc, int reptime, double matcost)
    {
        SetName(desc);
        SetRepairTime(reptime);
        SetMatCost(matcost);
    }

    internal RepairTask(int? id, string desc, int reptime, double matcost)
    {
        if (id.HasValue) SetId((int)id);
        SetName(desc);
        SetRepairTime(reptime);
        SetMatCost(matcost);
    }

    public void SetId(int id)
    {
        if (id <= 0) throw new DomainException("RepairTask id");
        ID = id;
    }
    public void SetName(string desc)
    {
        if (string.IsNullOrWhiteSpace(desc)) throw new DomainException("RepairTask desc");
        Description = desc;
    }
    public void SetRepairTime(int reptime)
    {
        if (reptime <= 0) throw new DomainException("RepairTask reptime");
        RepairTime = reptime;
    }
    public void SetMatCost(double cmat)
    {
        if (cmat < 0) throw new DomainException("RepairTask cost mat");
        CostMaterials = cmat;
    }
}

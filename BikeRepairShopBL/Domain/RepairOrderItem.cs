using BikeRepairShopBL.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeRepairShopBL.Domain;

public class RepairOrderItem
{
    internal RepairOrderItem(int? id, RepairOrder ro, Bike bike, RepairTask rt, Repairman rm)
    {
        SetId(id);
        SetRepairOrder(ro);
        SetBike(bike);
        SetRepairTask(rt);
        SetRepairman(rm);
    }

    public int? ID { get; private set; }
    public RepairOrder RepairOrder { get; private set; }
    public Bike Bike { get; private set; }
    public RepairTask RepairTask { get; private set; }
    public Repairman Repairman { get; private set; }

    public void SetId(int? id)
    {
        //if (!id.HasValue) throw new DomainException("RepairOrderItem null id");
        if (id <= 0) throw new DomainException("RepairOrderItem id");
        ID = id;
    }
    public void SetRepairOrder(RepairOrder ro)
    {
        if (ro == null) throw new DomainException("RepairOrderItem ro null");
        if (!ro.GetRepairOrderItems().Contains(this)) ro.AddRepairOrderItem(this);
        RepairOrder = ro;
    }
    public void SetBike(Bike bike)
    {
        if (bike == null) throw new DomainException("RepairOrderItem bike null");
        Bike = bike;
    }
    public void SetRepairTask(RepairTask rt)
    {
        if (rt == null) throw new DomainException("RepairOrderItem rt null");
        RepairTask = rt;
    }
    public void SetRepairman(Repairman rm)
    {
        if (rm == null) throw new DomainException("RepairOrderItem rm null");
        Repairman = rm;
    }
}

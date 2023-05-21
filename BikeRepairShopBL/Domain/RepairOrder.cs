using BikeRepairShopBL.DTO;
using BikeRepairShopBL.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeRepairShopBL.Domain;

public class RepairOrder
{
    internal RepairOrder(int? id, Urgency urgency, DateOnly orderDate, Customer customer, bool paid)
    {
        SetId(id);
        SetUrgency(urgency);
        SetOrderDate(orderDate);
        SetCustomer(customer);
        SetPaid(paid);
    }

    public int? ID { get; private set; }
    public Urgency Urgency { get; private set; }
    public DateOnly OrderDate { get; private set; }
    public Customer Customer { get; private set; }
    public bool Paid { get; private set; }
    private List<RepairOrderItemInfo> _repairOrderItems = new List<RepairOrderItemInfo>();
    public IReadOnlyList<RepairOrderItemInfo> GetRepairOrderItems() { return _repairOrderItems.AsReadOnly(); }

    public void SetId(int? id)
    {
        //if (!id.HasValue) throw new DomainException("RepairOrder null id");
        if (id <= 0) throw new DomainException("RepairOrder id");
        ID = id;
    }
    public void SetUrgency(Urgency urgency)
    {
        if (!Enum.IsDefined(typeof(Urgency), urgency)) throw new DomainException("RepairOrder urgency");
        Urgency = urgency;
    }
    public void SetOrderDate(DateOnly date)
    {
        if (date < DateOnly.MinValue || DateOnly.MaxValue < date) throw new DomainException("RepairOrder order date");
        OrderDate = date;
    }
    public void SetCustomer(Customer customer)
    {
        if (customer == null) throw new DomainException("RepairOrder Customer null");
        if (!customer.GetRepairOrders().Contains(this)) customer.AddRepairOrder(this);
        Customer = customer;
    }
    public void SetPaid(bool paid)
    {
        Paid = paid;
    }
    //
    public void AddRepairOrderItem(RepairOrderItemInfo roi)
    {
        if (roi == null) throw new DomainException("RepairOrder addroi roi null");
        if (_repairOrderItems.Contains(roi)) throw new DomainException("RepairOrder addroi list already contains roi");
        _repairOrderItems.Add(roi);
        //if (roi.RepairOrder != this) roi.SetRepairOrder(this);
    }
    public void RemoveRepairOrderItem(RepairOrderItemInfo roi)
    {
        if (roi == null) throw new DomainException("RepairOrder delroi roi null");
        if (!_repairOrderItems.Contains(roi)) throw new DomainException("RepairOrder delroi list doesnt contain roi");
        _repairOrderItems.Remove(roi);
    }
    
    //
    public double GetCost()
    {
        double cost = 0;

        //base cost
        foreach (RepairOrderItemInfo roi in GetRepairOrderItems())
        {
            cost += (((roi.Repairman.CostPerHour * roi.RepairTask.RepairTime) / 60) + roi.RepairTask.CostMaterials); // (((cost per hour repman x repairtime in min) / min in hour) + constant material cost)
        }
        //discount & additional cost
        const double DISCOUNT_MORE_THAN_FIVE_ORDERS = 0.02;
        const double DISCOUNT_NO_RUSH = 0.05;
        const double EXTRA_COST_FAST = 0.20;

        double additivesNsubtracts = 0;

        switch(Urgency)
        {
            case Urgency.Fast:
                additivesNsubtracts += EXTRA_COST_FAST * cost;
                break;
            case Urgency.NoRush:
                additivesNsubtracts -= DISCOUNT_NO_RUSH * cost;
                break;
            default: break;
        }
        if (Customer.GetRepairOrders().Count > 5)
        {
            additivesNsubtracts -= DISCOUNT_MORE_THAN_FIVE_ORDERS * cost;
        }
        double totalCost = cost + additivesNsubtracts;
        return Math.Round(totalCost, 2);
    }
}

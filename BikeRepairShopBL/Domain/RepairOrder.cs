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
    private List<RepairOrderItem> _repairOrderItems = new List<RepairOrderItem>();
    public IReadOnlyList<RepairOrderItem> GetRepairOrderItems() { return _repairOrderItems.AsReadOnly(); }

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
    public void AddRepairOrderItem(RepairOrderItem roi)
    {
        if (roi == null) throw new DomainException("RepairOrder addroi roi null");
        if (_repairOrderItems.Contains(roi)) throw new DomainException("RepairOrder addroi list already contains roi");
        _repairOrderItems.Add(roi);
        if (roi.RepairOrder != this) roi.SetRepairOrder(this);
    }
    public void RemoveRepairOrderItem(RepairOrderItem roi)
    {
        if (roi == null) throw new DomainException("RepairOrder delroi roi null");
        if (!_repairOrderItems.Contains(roi)) throw new DomainException("RepairOrder delroi list doesnt contain roi");
        _repairOrderItems.Remove(roi);
    }
}

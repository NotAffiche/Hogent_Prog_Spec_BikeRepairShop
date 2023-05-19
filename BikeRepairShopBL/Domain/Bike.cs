using BikeRepairShopBL.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BikeRepairShopBL.Domain;

public class Bike
{
    private int? _id;
    private string? _description;
    private double _purchaseCost;

    internal Bike(int? id, BikeType bikeType, double purchaseCost, string? description)
    {
        if (id.HasValue) SetId((int)id);
        BikeType = bikeType;
        SetPurchaseCost(purchaseCost);
        Description = description;
    }

    public int? ID { get; private set; }
    public string? Description { get; set; }
    public double PurchaseCost { get; private set; }
    public BikeType BikeType { get; set; }
    public Customer Customer { get; private set; }

    public void SetId(int id)
    {
        if (id <0) throw new DomainException("Bike invalid id");
        ID = id;
    }

    public void SetPurchaseCost(double purchaseCost)
    {
        if (purchaseCost < 0) throw new DomainException("Bike invalid purchase cost");
        PurchaseCost = purchaseCost;
    }

    public void SetCustomer(Customer customer)
    {
        if (customer == null) throw new DomainException("Bike Customer null");
        if (!customer.GetBikes().Contains(this)) customer.AddBike(this);
        Customer = customer;
    }

    public override bool Equals(object? obj)
    {
        return obj is Bike bike &&
               Description == bike.Description &&
               PurchaseCost == bike.PurchaseCost &&
               BikeType == bike.BikeType;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Description, PurchaseCost, BikeType);
    }
}

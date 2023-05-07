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

    public int? ID
    {
        get { return _id; }
        private set
        {
            if (value <= 0) throw new DomainException("bike-setid");
            _id = value;
        }
    }
    public string? Description { get; set; }
    public double PurchaseCost
    {
        get { return _purchaseCost; }
        private set
        {
            if (value <= 0) throw new DomainException("bike-setpurchasecost");
            _purchaseCost = value;
        }
    }
    public BikeType BikeType { get; set; }

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

    public Bike(string? description, BikeType biketype, double purchaseCost)
    {
        this.Description = description;
        this.BikeType= biketype;
        this.PurchaseCost = purchaseCost;
    }
}

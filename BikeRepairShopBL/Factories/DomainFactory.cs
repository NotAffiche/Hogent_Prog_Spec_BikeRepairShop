using BikeRepairShopBL.Domain;
using BikeRepairShopBL.DTO;
using BikeRepairShopBL.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeRepairShopBL.Factories;

public class DomainFactory
{
    public static Customer NewCustomer(CustomerInfo customerInfo)
    {
        try
        {
            return new Customer(customerInfo.Name, customerInfo.Email, customerInfo.Address);
        }
        catch (Exception ex) { throw new DomainException("NewCustomer", ex); }
    }
    public static Customer ExistingCustomer(int id, string name, string email, string address, List<Bike> bikes)
    {
        try
        {
            return new Customer(id, name, email, address, bikes);
        }
        catch (Exception ex) { throw new DomainException("ExistingCustomer", ex); }
    }
    public static Bike NewBike(BikeInfo bikeInfo)
    {
        try
        {
            return new Bike(bikeInfo.Id, bikeInfo.BikeType, bikeInfo.PurchaseCost, bikeInfo.Description);
        }
        catch (Exception ex) { throw new DomainException("NewBike", ex); }
    }
    public static Bike ExistingBike(int? id, BikeType bikeType, double purchaseCost, string? description)
    {
        try
        {
            return new Bike(id, bikeType, purchaseCost, description);
        }
        catch (Exception ex) { throw new DomainException("NewBike", ex); }
    }

    public static Repairman NewRepairman(RepairmanInfo ri)
    {
        try
        {
            return new Repairman(ri.Id, ri.Name, ri.Email, ri.CostPerHour);
        }
        catch (Exception ex) { throw new DomainException("NewRepairman", ex); }
    }
    public static Repairman ExistingRepairman(int? id, string name, string email, double cph)
    {
        try
        {
            return new Repairman(id, name, email, cph);
        }
        catch (Exception ex) { throw new DomainException("ExistingRepairman", ex); }
    }
}

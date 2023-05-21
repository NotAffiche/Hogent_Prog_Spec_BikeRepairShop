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
            return new Customer(customerInfo.Id, customerInfo.Name, customerInfo.Email, customerInfo.Address);
        }
        catch (Exception ex) { throw new DomainException("NewCustomer", ex); }
    }
    public static Customer ExistingCustomer(int id, string name, string email, string address, List<Bike>? bikes = null, List<RepairOrder>? repairOrders = null)
    {
        try
        {
            if (bikes == null && repairOrders == null)
            {
                return new Customer(id, name, email, address);
            }
            else
            {
                return new Customer(id, name, email, address, bikes!, repairOrders!);
            }
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
    public static Bike ExistingBike(int? id, BikeType bikeType, double purchaseCost, string? description, Customer? c = null)
    {
        try
        {
            return new Bike(id, bikeType, purchaseCost, description, c);
        }
        catch (Exception ex) { throw new DomainException("ExistingBike", ex); }
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
    public static RepairOrder NewRepairOrder(RepairOrderInfo roi, Customer customer)
    {
        try
        {
            return new RepairOrder(roi.ID, (Urgency)Enum.Parse(typeof(Urgency), roi.Urgency), roi.OrderDate, customer, roi.Paid);
        }
        catch (Exception ex) { throw new DomainException("NewRepairOirder", ex); }
    }
    public static RepairOrder ExistingRepairOrder(int? id, Urgency urgency, DateOnly orderDate, Customer customer, bool paid)
    {
        try
        {
            return new RepairOrder(id, urgency, orderDate, customer, paid);
        }
        catch (Exception ex) { throw new DomainException("ExistingRepairman", ex); }
    }
    public static RepairOrderItem NewRepairOrderItem(RepairOrderItemInfo roii, RepairOrder ro, Bike bike, RepairTask rt, Repairman rm)
    {
        try
        {
            return new RepairOrderItem(roii.ID, ro, bike, rt, rm);
        }
        catch (Exception ex) { throw new DomainException("NewRepairOrderItem", ex); }
    }
    public static RepairOrderItem ExistingRepairOrderItem(int? id, RepairOrder ro, Bike bike, RepairTask rt, Repairman rm)
    {
        try
        {
            return new RepairOrderItem(id, ro, bike, rt, rm);
        }
        catch (Exception ex) { throw new DomainException("ExistingRepairOrderItem", ex); }
    }
    public static RepairTask ExistingRepairTask(int? id, string desc, int reptime, double matcost)
    {
        try
        {
            return new RepairTask(id, desc, reptime, matcost);
        }
        catch (Exception ex) { throw new DomainException("ExistingRepairTask", ex); }
    }
}

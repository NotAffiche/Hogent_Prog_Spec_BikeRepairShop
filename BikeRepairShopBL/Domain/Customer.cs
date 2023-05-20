using BikeRepairShopBL.DTO;
using BikeRepairShopBL.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeRepairShopBL.Domain;

public class Customer
{
    internal Customer(int? id, string name, string email, string address)
    {
        SetId(id);
        SetName(name);
        SetEmail(email);
        SetAddress(address);
    }
    internal Customer(int? id, string name, string email, string address, List<Bike> bikes, List<RepairOrder> repairOrders) : this(id, name, email, address)
    {
        this._bikes = bikes;
        foreach (Bike bike in bikes) { bike.SetCustomer(this); }
        foreach (RepairOrder ro in repairOrders) { ro.SetCustomer(this); }
    }

    public int? ID { get; private set; }
    public string Name { get; private set; }
    public string Email { get; private set; }
    public string Address { get; private set; }
    private List<Bike> _bikes = new List<Bike>();
    public IReadOnlyList<Bike> GetBikes() { return _bikes.AsReadOnly(); }
    private List<RepairOrder> _repairOrders = new List<RepairOrder>();
    public IReadOnlyList<RepairOrder> GetRepairOrders() { return _repairOrders.AsReadOnly(); }

    public void SetId(int? id)
    {
        if (id <= 0) throw new DomainException("Customer id");
        ID = id;
    }
    public void SetName(string name)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new DomainException("Customer name");
        Name = name;
    }
    public void SetEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email)) throw new DomainException("Customer email");
        Email = email;
    }
    public void SetAddress(string address)
    {
        if (string.IsNullOrWhiteSpace(address)) throw new DomainException("Customer address");
        Address = address;
    }

    public void AddBike(Bike bike)
    {
        //if (_bikes.Contains(bike)) throw new DomainException("customer-addbike");
        //_bikes.Add(bike);

        if (bike == null) throw new DomainException("Customer addbike bike null");
        //if (bike.Customer!=null) if (bike.Customer!=this) throw new DomainException("Customer"); //je kan een fiets niet toekennen aan een nieuwe klant            
        if (_bikes.Contains(bike)) throw new DomainException("Customer addbike list already contains bike");
        _bikes.Add(bike);
        if (bike.Customer != this) bike.SetCustomer(this);
    }
    public void RemoveBike(Bike bike)
    {
        //if (!_bikes.Contains(bike)) throw new DomainException("customer-removebike");
        //_bikes.Remove(bike);

        if (bike == null) throw new DomainException("Customer delbike bike null");
        //if (bike.Customer != this) throw new DomainException("Customer");
        if (!_bikes.Contains(bike)) throw new DomainException("Customer delbike list doesnt contain bike");
        _bikes.Remove(bike);
    }
    public void AddRepairOrder(RepairOrder ro)
    {
        if (ro == null) throw new DomainException("Customer addro ro null");
        if (_repairOrders.Contains(ro)) throw new DomainException("Customer addro list already contains ro");
        _repairOrders.Add(ro);
        if (ro.Customer != this) ro.SetCustomer(this);
    }
    public void RemoveRepairOrder(RepairOrder ro)
    {
        if (ro == null) throw new DomainException("Customer delro ro null");
        if (!_repairOrders.Contains(ro)) throw new DomainException("Customer delro list doesnt contain rp");
        _repairOrders.Remove(ro);
    }
}

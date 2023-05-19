using BikeRepairShopBL.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeRepairShopBL.Domain;

public class Customer
{
    private int? _id;
    private string _name;
    private string _address;
    private string _email;

    internal Customer(string name, string email, string address)
    {
        SetName(name);
        SetEmail(email);
        SetAddress(address);
    }
    internal Customer(int id, string name, string email, string address, List<Bike> bikes) : this(name, email, address)
    {
        SetId(id);
        this._bikes = bikes;
        foreach (Bike bike in bikes) { bike.SetCustomer(this); }
    }

    public int? ID { get; private set; }
    public string Name { get; private set; }
    public string Email { get; private set; }
    public string Address { get; private set; }
    private List<Bike> _bikes = new List<Bike>();
    public IReadOnlyList<Bike> GetBikes() { return _bikes.AsReadOnly(); }
    private List<CustomerRepairOrderInfo> _customerRepairOrderInfos = new List<CustomerRepairOrderInfo>();
    public IReadOnlyList<CustomerRepairOrderInfo> GetCustomerRepairOrderInfos() { return _customerRepairOrderInfos.AsReadOnly(); }

    public void SetId(int id)
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

}

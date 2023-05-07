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

    public int? ID
    {
        get { return _id; }
        private set
        {
            if (value <= 0) throw new DomainException("customer-setid");
            _id = value;
        }
    }
    public string Name
    {
        get { return _name; }
        private set
        {
            if (string.IsNullOrWhiteSpace(value)) throw new DomainException("customer-setname");
            _name = value;
        }
    }
    public string Address
    {
        get { return _address; }
        private set
        {
            if (string.IsNullOrWhiteSpace(value)) throw new DomainException("customer-setaddress");
            _address = value;
        }
    }
    public string Email
    {
        get { return _email; }
        private set
        {
            if (string.IsNullOrWhiteSpace(value)) throw new DomainException("customer-setemail");
            _email = value;
        }
    }

    private List<Bike> _bikes = new List<Bike>();
    public IReadOnlyList<Bike> GetBikes() { return _bikes.AsReadOnly(); }
    private List<CustomerRepairOrderInfo> _customerRepairOrderInfos = new List<CustomerRepairOrderInfo>();
    public IReadOnlyList<CustomerRepairOrderInfo> GetCustomerRepairOrderInfos() { return _customerRepairOrderInfos.AsReadOnly(); }

    public void AddBike(Bike bike)
    {
        if (_bikes.Contains(bike)) throw new DomainException("customer-addbike");
        _bikes.Add(bike);
    }
    public void RemoveBike(Bike bike)
    {
        if (!_bikes.Contains(bike)) throw new DomainException("customer-removebike");
        _bikes.Remove(bike);
    }

    public Customer(int id, string name, string address, string email)
    {
        this.ID= id;
        this.Name= name;
        this.Address= address;
        this.Email= email;
    }
}

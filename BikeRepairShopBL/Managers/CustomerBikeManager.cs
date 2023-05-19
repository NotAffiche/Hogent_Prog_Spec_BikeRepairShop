using BikeRepairShopBL.Domain;
using BikeRepairShopBL.DTO;
using BikeRepairShopBL.Exceptions;
using BikeRepairShopBL.Factories;
using BikeRepairShopBL.Interfaces;

namespace BikeRepairShopBL.Managers;

public class CustomerBikeManager
{
    private ICustomerBikeRepository repo;

    public CustomerBikeManager(ICustomerBikeRepository repo)
    {
        this.repo = repo;
    }

    //
    public List<BikeInfo> GetBikesInfo()
    {
        try
        {
            return repo.GetBikesInfo();
        }
        catch(Exception ex)
        {
            throw new ManagerException("GetBikesInfo", ex);
        }
    }
    public void AddBike(BikeInfo bikeInfo)
    {
        try
        {
            if (bikeInfo == null) throw new ManagerException("CustomerBikeManager AddBike bikeInfo NULL");
            Customer customer = repo.GetCustomer(bikeInfo.Customer.id);
            Bike bike = DomainFactory.NewBike(bikeInfo);
            customer.AddBike(bike);
            repo.AddBike(bike);
            bikeInfo.Id = bike.ID;
        }
        catch(Exception ex)
        {
            throw new ManagerException("AddBike", ex);
        }
    }
    //
    public void AddCustomer(CustomerInfo customerInfo)
    {
        try
        {
            if (customerInfo == null) throw new ManagerException("CustomerManager AddCust ci null");
            Customer customer = DomainFactory.NewCustomer(customerInfo);
            repo.AddCustomer(customer);
            customerInfo.Id = customer.ID;
        }
        catch (Exception ex) { throw new ManagerException("CustomerManager AddCust", ex); }
    }
    public void UpdateCustomer(CustomerInfo customerInfo)
    {
        try
        {
            if (customerInfo == null) throw new ManagerException("CustomerManager update cust ci null");
            if (!customerInfo.Id.HasValue) throw new ManagerException("CustomerManager update cust ci id no val");
            Customer customer = repo.GetCustomer((int)customerInfo.Id);
            customer.SetAddress(customerInfo.Address);
            customer.SetName(customerInfo.Name);
            customer.SetEmail(customerInfo.Email);
            repo.UpdateCustomer(customer);
        }
        catch (Exception ex) { throw new ManagerException("CustomerManager UpdateCust", ex); }
    }
    public void DeleteCustomer(CustomerInfo customerInfo)
    {
        try
        {
            if (customerInfo == null) throw new ManagerException("CustomerManager del cust ci null");
            if (!customerInfo.Id.HasValue) throw new ManagerException("CustomerManager del cust ci no val");
            Customer customer = repo.GetCustomer((int)customerInfo.Id);
            repo.DeleteCustomer(customer);
        }
        catch (Exception ex) { throw new ManagerException("CustomerManager del cust", ex); }
    }
    public Customer GetCustomer(int id)
    {
        try
        {
            return repo.GetCustomer(id);
        }
        catch (Exception ex) { throw new ManagerException("CustomerManager get cust id", ex); }
    }
    public List<CustomerInfo> GetCustomersInfo()
    {
        try
        {
            return repo.GetCustomersInfo();
        }
        catch (Exception ex) { throw new ManagerException("CustomerManager get ci", ex); }
    }
    public List<CustomerInfo> GetCustomersInfo(string name)
    {
        try
        {
            return repo.GetCustomersInfo(name);
        }
        catch (Exception ex) { throw new ManagerException("CustomerManager get ci list name", ex); }
    }
    public Customer GetCustomer(string email)
    {
        try
        {
            return repo.GetCustomer(email);
        }
        catch (Exception ex) { throw new ManagerException("CustomerManager get cust email", ex); }
    }
    public void UpdateBike(BikeInfo bikeInfo)
    {
        try
        {
            if (bikeInfo == null) throw new ManagerException("CustomerManager update bike bi null");
            Customer customer = repo.GetCustomer(bikeInfo.Customer.id);
            Bike bike = customer.GetBikes().Where(x => x.ID == bikeInfo.Id).First();
            bike.Description = bikeInfo.Description;
            bike.BikeType = bikeInfo.BikeType;
            bike.SetPurchaseCost(bikeInfo.PurchaseCost);
            //TODO check if bike changed
            repo.UpdateBike(bike);
        }
        catch (Exception ex) { throw new ManagerException("CustomerManager update bike", ex); }
    }
    public void DeleteBike(BikeInfo bikeInfo)
    {
        try
        {
            if (bikeInfo == null) throw new ManagerException("CustomerManager del bike bi null");
            Customer? customer = repo.GetCustomer(bikeInfo.Customer.id);
            if (customer == null) throw new ManagerException("CustomerManager non existing (deleted?) customer");
            Bike bike = customer.GetBikes().Where(x => x.ID == bikeInfo.Id).First();
            //Bike bike = DomainFactory.ExistingBike(bikeInfo,customer);
            customer.RemoveBike(bike);
            repo.DeleteBike(bike);
        }
        catch (Exception ex) { throw new ManagerException("CustomerManager del bike", ex); }
    }
}

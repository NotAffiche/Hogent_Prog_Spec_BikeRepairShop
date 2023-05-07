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
            Customer customer = repo.GetCustomer(bikeInfo.Customer.id);
            Bike bike = DomainFactory.NewBike(bikeInfo);
            customer.AddBike(bike);
            repo.AddBike(bike);
            bikeInfo.Id= bike.ID;
        }
        catch(Exception ex)
        {
            throw new ManagerException("AddBike", ex);
        }
    }
}

using BikeRepairShopBL.Domain;
using BikeRepairShopBL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeRepairShopBL.Interfaces;

public interface ICustomerBikeRepository
{
    List<BikeInfo> GetBikesInfo();
    void AddBike(Bike bike);
    Customer GetCustomer(int id);
}

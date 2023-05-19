using BikeRepairShop.Admin.UI.Model;
using BikeRepairShopBL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeRepairShop.Admin.UI.Mappers;

public static class CustomerMapper
{
    public static CustomerInfo ToDTO(CustomerUI custUI)
    {
        return new CustomerInfo(custUI.ID, custUI.Name, custUI.Email, custUI.Address, custUI.NrOfBikes, custUI.TotalBikesValue);
    }
}

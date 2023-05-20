using BikeRepairShop.Admin.UI.Model;
using BikeRepairShopBL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeRepairShop.Admin.UI.Mappers;

public static class BikeMapper
{
    public static BikeInfo ToDTO(BikeUI bikeUI)
    {
        return new BikeInfo(bikeUI.ID, bikeUI.Description, bikeUI.BikeType, bikeUI.PurchaseCost, bikeUI.CustomerId, bikeUI.CustomerDesc);
    }
}

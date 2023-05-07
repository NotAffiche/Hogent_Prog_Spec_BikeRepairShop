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
    public static Bike NewBike(BikeInfo bi)
    {
        try
        {
            return new Bike(bi.Description, bi.BikeType, bi.PurchaseCost);
        }
        catch (Exception ex)
        {
            throw new FactoryException("NewBike", ex);
        }
    } 
}

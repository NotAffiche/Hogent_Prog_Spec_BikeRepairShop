using BikeRepairShopBL.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeRepairShopBL.DTO;

public class BikeInfo
{
    public BikeInfo(int? id, string description, BikeType bikeType, double purchaseCost, int custId, string custDesc)
    {
        Id = id;
        Description = description;
        BikeType = bikeType;
        PurchaseCost = purchaseCost;
        Customer = (custId, custDesc);
    }

    //DTO = Data Transfer Object
    //data from bike
    public int? Id { get; set; } // idem bikeID
    public string Description { get; set; }
    public BikeType BikeType { get; set; }
    public double PurchaseCost { get; set; }
    //data from customer
    public (int id,string custDesc) Customer { get; set; } //tuple -> id, customer info (name, email)
}

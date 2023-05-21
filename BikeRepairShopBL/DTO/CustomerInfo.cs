using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeRepairShopBL.DTO;

public class CustomerInfo
{
    public CustomerInfo(int? id, string name, string email, string address, int nrOfBikes, double totalBikeValues, int nrOfRepairOrders)
    {
        Id = id;
        Name = name;
        Email = email;
        Address = address;
        NrOfBikes = nrOfBikes;
        TotalBikeValues = totalBikeValues;
        NrOfRepairOrders = nrOfRepairOrders;
    }
    public int? Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Address { get; set; }
    public int NrOfBikes { get; set; }
    public double TotalBikeValues { get; set; }
    public int NrOfRepairOrders { get; set; }
    public override string ToString()
    {
        return $"{Id},{Name},{Address},{Email}";
    }
}
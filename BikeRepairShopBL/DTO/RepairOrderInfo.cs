using BikeRepairShopBL.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BikeRepairShopBL.DTO;

public class RepairOrderInfo
{
    public RepairOrderInfo(int? id, string urgency, bool paid, DateOnly orderDate, int? custId, string custDesc)
    {
        ID = id;
        Urgency = urgency;
        Paid = paid;
        OrderDate = orderDate;
        Customer = (custId, custDesc);
    }
    public RepairOrderInfo(int? id, string urgency, double? cost, bool paid, DateOnly orderDate, int? custId, string custDesc)
    {
        ID = id;
        Urgency = urgency;
        Cost = cost;
        Paid = paid;
        OrderDate = orderDate;
        Customer = (custId, custDesc);
    }

    public int? ID { get; set; }
    public string Urgency { get; set; }
    public double? Cost { get; set; }
    public bool Paid { get; set; }
    public DateOnly OrderDate { get; }
    public (int? id, string custDesc) Customer { get; set; }
    public override string ToString()
    {
        return $"{ID},{Cost},{Paid},{OrderDate}, {Customer}";
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeRepairShopBL.Domain;

public class CustomerRepairOrderInfo
{
    public int RepairOrderID { get; set; }
    public double? Cost { get; }
    public bool Payed { get; }
    public DateOnly OrderDate { get; }
}

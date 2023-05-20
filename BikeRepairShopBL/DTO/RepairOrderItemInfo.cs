using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeRepairShopBL.DTO;

public class RepairOrderItemInfo
{
    public RepairOrderItemInfo(int? id, int repOrderId, int bikeId, int repTaskId, int repManId)
    {
        ID = id;
    }

    public int? ID { get; set; }
    public override string ToString()
    {
        return $"";
    }
}

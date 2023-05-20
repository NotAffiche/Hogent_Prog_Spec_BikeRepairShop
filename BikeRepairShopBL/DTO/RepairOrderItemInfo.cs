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
        RepairOrderId = repOrderId;
        BikeId = bikeId;
        RepairTaskId = repTaskId;
        RepairmanId = repManId;
    }

    public int? ID { get; set; }
    public int RepairOrderId { get; set; }
    public int BikeId { get; set; }
    public int RepairTaskId { get; set; }
    public int RepairmanId { get; set; }
    public override string ToString()
    {
        return $"{ID}";
    }
}

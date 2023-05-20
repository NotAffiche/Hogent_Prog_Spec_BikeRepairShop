using BikeRepairShopBL.Domain;
using BikeRepairShopBL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeRepairShopBL.Interfaces;

public interface IRepairOrderRepository
{
    List<RepairTaskInfo> GetRepairTaskInfos();
    List<RepairOrderInfo> GetRepairOrderInfos(CustomerInfo c);
    List<RepairOrderItemInfo> GetRepairOrderItemInfos(RepairOrderInfo ro);
}

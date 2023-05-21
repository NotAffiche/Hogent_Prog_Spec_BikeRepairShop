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
    RepairOrder GetRepairOrder(int id);
    RepairOrderItem GetRepairOrderItem(int id, int roInfoId, Bike b, int rtInfoId, Repairman rm);
    List<RepairTaskInfo> GetRepairTaskInfos();
    List<RepairOrderInfo> GetRepairOrderInfos(int custId);
    List<RepairOrderItemInfo> GetRepairOrderItemInfos(RepairOrderInfo roi);
    RepairTask GetRepairTask(int id);

    void AddRepairOrder(RepairOrder ro);
    void AddRepairOrderItem(RepairOrderItem roi);

    void RemoveRepairOrder(RepairOrder ro);
    void RemoveRepairOrderItem(RepairOrderItem roi);

    void UpdateRepairOrder(RepairOrder ro);
    void UpdateRepairOrderItem(RepairOrderItem roi);
}

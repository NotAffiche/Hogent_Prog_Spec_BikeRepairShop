using BikeRepairShop.Admin.UI.Model;
using BikeRepairShopBL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeRepairShop.Admin.UI.Mappers;

public static class RepairOrderMapper
{
    public static RepairOrderInfo OrderToDTO(RepairOrderUI repUI, CustomerUI cUI)
    {
        return new RepairOrderInfo(repUI.ID, repUI.Urgency, repUI.Paid, repUI.OrderDate, cUI.ID, cUI.CustomerDescription);
    }

    public static RepairOrderItemInfo OrderItemToDTO(RepairOrderItemUI repUI, CustomerUI cUI)
    {
        return new RepairOrderItemInfo(repUI.ID, repUI.RepairOrderId, repUI.RepairOrder.OrderDate, repUI.RepairOrder.Urgency, repUI.BikeId, repUI.Bike.BikeType, repUI.Bike.Description,
            repUI.RepairTaskId, repUI.RepairTask.RepairTime, repUI.RepairTask.Description, repUI.RepairTask.CostMaterials, repUI.RepairmanId, repUI.Repairman.Name, repUI.Repairman.Email, repUI.Repairman.CostPerHour);
    }
}

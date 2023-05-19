using BikeRepairShop.Admin.UI.Model;
using BikeRepairShopBL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeRepairShop.Admin.UI.Mappers;

public static class RepairmanMapper
{
    public static RepairmanInfo ToDTO(RepairmanUI repUI)
    {
        return new RepairmanInfo(repUI.ID, repUI.Name, repUI.Email, repUI.CostPerHour);
    }
}

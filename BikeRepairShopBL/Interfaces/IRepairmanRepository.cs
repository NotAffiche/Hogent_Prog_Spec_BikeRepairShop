using BikeRepairShopBL.Domain;
using BikeRepairShopBL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeRepairShopBL.Interfaces;

public interface IRepairmanRepository
{
    List<RepairmanInfo> GetRepairmanInfos(string? name = null);
    void AddRepairman(Repairman r);
    Repairman GetRepairman(int id);
    Repairman GetRepairman(string email);
    void DeleteRepairman(Repairman r);
    void UpdateRepairman(Repairman r);
}

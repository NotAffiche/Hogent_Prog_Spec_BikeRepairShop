using BikeRepairShopBL.Domain;
using BikeRepairShopBL.DTO;
using BikeRepairShopBL.Exceptions;
using BikeRepairShopBL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeRepairShopBL.Managers;

public class RepairOrderManager
{
    private IRepairOrderRepository repo;

    public RepairOrderManager(IRepairOrderRepository repo)
    {
        this.repo = repo;
    }

    //
    public List<RepairOrderInfo> GetRepairOrderInfos(CustomerInfo c)
    {
        try
        {
            return repo.GetRepairOrderInfos(new Customer(c.Id, c.Name, c.Email, c.Address));
        }
        catch (Exception ex) { throw new ManagerException("RepairOrder get rep order infos", ex); }
    }
    public List<RepairTaskInfo> GetRepairTaskInfos()
    {
        try
        {
            return repo.GetRepairTaskInfos();
        }
        catch (Exception ex) { throw new ManagerException("RepairOrder get rtaskinfos", ex); }
    }
}

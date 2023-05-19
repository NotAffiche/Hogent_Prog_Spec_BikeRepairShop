using BikeRepairShopBL.Domain;
using BikeRepairShopBL.DTO;
using BikeRepairShopBL.Exceptions;
using BikeRepairShopBL.Factories;
using BikeRepairShopBL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeRepairShopBL.Managers;

public class RepairmanManager
{
    private IRepairmanRepository repo;

    public RepairmanManager(IRepairmanRepository repo)
    {
        this.repo = repo;
    }
    
    //
    public List<RepairmanInfo> GetRepairmanInfos(string? name = null)
    {
        try
        {
            return repo.GetRepairmanInfos(name);
        }
        catch (Exception ex) { throw new ManagerException("RepairmanManager get ri list name?", ex); }
    }
    public void AddRepairman(RepairmanInfo ri)
    {
        try
        {
            if (ri == null) throw new ManagerException("RepairmanManager AddRep ci null");
            Repairman repairman = DomainFactory.NewRepairman(ri);
            repo.AddRepairman(repairman);
            ri.Id = repairman.ID;
        }
        catch (Exception ex) { throw new ManagerException("RepairmanManager AddRep", ex); }
    }
    public Repairman GetRepairman(int id)
    {
        try
        {
            return repo.GetRepairman(id);
        }
        catch (Exception ex) { throw new ManagerException("RepairmanManager get rep id", ex); }
    }
    public Repairman GetRepairman(string email)
    {
        try
        {
            return repo.GetRepairman(email);
        }
        catch (Exception ex) { throw new ManagerException("RepairmanManager get rep email", ex); }
    }
    public void UpdateCustomer(RepairmanInfo ri)
    {
        try
        {
            if (ri == null) throw new ManagerException("RepairmanManager update rep ri null");
            if (!ri.Id.HasValue) throw new ManagerException("RepairmanManager update rep ri id no val");
            Repairman repairman = repo.GetRepairman((int)ri.Id);
            repairman.SetName(ri.Name);
            repairman.SetEmail(ri.Email);
            repairman.SetCostPerHour(ri.CostPerHour);
            repo.UpdateRepairman(repairman);
        }
        catch (Exception ex) { throw new ManagerException("RepairmanManager Update rep", ex); }
    }
    public void DeleteCustomer(RepairmanInfo ri)
    {
        try
        {
            if (ri == null) throw new ManagerException("RepairmanManager del rep ri null");
            if (!ri.Id.HasValue) throw new ManagerException("RepairmanManager del rep ri no val");
            Repairman repairman = repo.GetRepairman((int)ri.Id);
            repo.DeleteRepairman(repairman);
        }
        catch (Exception ex) { throw new ManagerException("RepairmanManager del rep", ex); }
    }


}

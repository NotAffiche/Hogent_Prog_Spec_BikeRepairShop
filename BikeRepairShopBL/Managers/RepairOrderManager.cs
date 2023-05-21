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

public class RepairOrderManager
{
    private IRepairOrderRepository repo;
    private ICustomerBikeRepository customerBikeRepo;
    private IRepairmanRepository repairmanRepo;

    public RepairOrderManager(IRepairOrderRepository repo, ICustomerBikeRepository customerBikeRepo, IRepairmanRepository repairmanRepo)
    {
        this.repo = repo;
        this.customerBikeRepo = customerBikeRepo;
        this.repairmanRepo= repairmanRepo;
    }

    //
    public List<RepairOrderInfo> GetRepairOrderInfos(CustomerInfo c)
    {
        try
        {
            return repo.GetRepairOrderInfos((int)c.Id!);
        }
        catch (Exception ex) { throw new ManagerException("RepairOrder get rep order infos", ex); }
    }
    public List<RepairOrderItemInfo> GetRepairOrderItemInfos(RepairOrderInfo roi)
    {
        try
        {
            return repo.GetRepairOrderItemInfos(roi);
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

    public void AddRepairOrder(RepairOrderInfo roInfo)
    {
        try
        {
            if (roInfo == null) throw new ManagerException("RepairOrderManager AddRO roInfo null");
            RepairOrder ro = DomainFactory.NewRepairOrder(roInfo, customerBikeRepo.GetCustomer((int)roInfo.Customer.id!));
            repo.AddRepairOrder(ro);
            roInfo.ID = ro.ID;
        }
        catch (Exception ex) { throw new ManagerException("RepairOrder add ro", ex); }
    }
    public void AddRepairOrderItem(RepairOrderItemInfo roiInfo)
    {
        try
        {
            if (roiInfo == null) throw new ManagerException("RepairOrderManager AddROI roiInfo null");
            RepairOrder ro = repo.GetRepairOrder(roiInfo.RepairOrder.RepairOrderId);
            Bike bike = customerBikeRepo.GetBike(roiInfo.Bike.BikeId);
            RepairTask rt = repo.GetRepairTask(roiInfo.RepairTask.RepairTaskId);
            Repairman rm = repairmanRepo.GetRepairman(roiInfo.Repairman.RepairmanId);
            RepairOrderItem roi = DomainFactory.NewRepairOrderItem(roiInfo, ro, bike, rt, rm);
            repo.AddRepairOrderItem(roi);
            roiInfo.ID = roi.ID;
        }
        catch (Exception ex) { throw new ManagerException("RepairOrder add roi", ex); }
    }

    public void RemoveRepairOrder(RepairOrderInfo roInfo)
    {
        try
        {
            if (roInfo == null) throw new ManagerException("RepairOrderManager RemRO roInfo null");
            RepairOrder ro = repo.GetRepairOrder((int)roInfo.ID!);
            repo.RemoveRepairOrder(ro);
        }
        catch (Exception ex) { throw new ManagerException("RepairOrder remove ro", ex); }
    }
    public void RemoveRepairOrderItem(RepairOrderItemInfo roiInfo)
    {
        try
        {
            if (roiInfo == null) throw new ManagerException("RepairOrderManager RemROI roiInfo null");
            RepairOrderItem roi = repo.GetRepairOrderItem((int)roiInfo.ID!, roiInfo.RepairOrder.RepairOrderId, customerBikeRepo.GetBike(roiInfo.Bike.BikeId), roiInfo.RepairTask.RepairTaskId, repairmanRepo.GetRepairman(roiInfo.Repairman.RepairmanId));
            repo.RemoveRepairOrderItem(roi);
        }
        catch (Exception ex) { throw new ManagerException("RepairOrder remove roi", ex); }
    }

    public void UpdateRepairOrder(RepairOrderInfo roInfo)
    {
        try
        {
            if (roInfo == null) throw new ManagerException("RepairOrderManager UpdateRO roInfo null");
            RepairOrder ro = DomainFactory.ExistingRepairOrder(roInfo.ID, (Urgency)Enum.Parse(typeof(Urgency), roInfo.Urgency), roInfo.OrderDate, customerBikeRepo.GetCustomer((int)roInfo.Customer.id!), roInfo.Paid);
            repo.UpdateRepairOrder(ro);
        }
        catch (Exception ex) { throw new ManagerException("RepairOrder update ro", ex); }
    }
    public void UpdateRepairOrderItem(RepairOrderItemInfo roiInfo)
    {
        try
        {
            if (roiInfo == null) throw new ManagerException("RepairOrderManager RemROI roiInfo null");
            //RepairOrderItem roi = repo.GetRepairOrderItem((int)roiInfo.ID!, roiInfo.RepairOrder.RepairOrderId, customerBikeRepo.GetBike(roiInfo.Bike.BikeId), roiInfo.RepairTask.RepairTaskId, repairmanRepo.GetRepairman(roiInfo.Repairman.RepairmanId));
            RepairOrder ro = repo.GetRepairOrder((int)roiInfo.RepairOrder.RepairOrderId!);
            Bike bike = customerBikeRepo.GetBike((int)roiInfo.Bike.BikeId!);
            RepairTask rt = repo.GetRepairTask((int)roiInfo.RepairTask.RepairTaskId);
            Repairman rm = repairmanRepo.GetRepairman((int)roiInfo.Repairman.RepairmanId);
            RepairOrderItem roi = DomainFactory.ExistingRepairOrderItem(roiInfo.ID!, ro, bike, rt, rm);
            repo.UpdateRepairOrderItem(roi);
        }
        catch (Exception ex) { throw new ManagerException("RepairOrder update roi", ex); }
    }
}

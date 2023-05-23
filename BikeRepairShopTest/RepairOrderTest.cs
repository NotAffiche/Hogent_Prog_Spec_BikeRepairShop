using BikeRepairShopBL.Domain;
using BikeRepairShopBL.DTO;
using BikeRepairShopBL.Exceptions;
using BikeRepairShopBL.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BikeRepairShopTest;

public class RepairOrderTest
{
    Customer c;
    DateOnly od;
    RepairOrder sro;

    public RepairOrderTest()
    {
        c = DomainFactory.ExistingCustomer(1, "Adrian", "adrian@stu.hogent", "SN");
        od = DateOnly.FromDateTime(DateTime.Now);
        sro = DomainFactory.ExistingRepairOrder(1, Urgency.Normal, od, c, false);
    }

    [Fact]
    public void SetId_NegativeId_ThrowsDomainException()
    {
        //var ro = DomainFactory.ExistingRepairOrder(1, Urgency.Normal, od, c, false);
        Assert.Throws<DomainException>(() => sro.SetId(-1));
    }

    [Fact]
    public void SetUrgency_InvalidUrgency_ThrowsDomainException()
    {
        Assert.Throws<DomainException>(() => sro.SetUrgency((Urgency)999));
    }

    [Fact]
    public void SetCustomer_NullCustomer_ThrowsDomainException()
    {
        Assert.Throws<DomainException>(() => sro.SetCustomer(null));
    }

    [Fact]
    public void SetCustomer_AddsRepairOrderToCustomer()
    {
        sro.SetCustomer(c);

        Assert.Contains(sro, c.GetRepairOrders());
    }

    [Fact]
    public void SetPaid_ValidPaid()
    {
        sro.SetPaid(true);

        Assert.True(sro.Paid);
    }

    [Fact]
    public void AddRepairOrderItem_NullRepairOrderItem_ThrowsDomainException()
    {
        Assert.Throws<DomainException>(() => sro.AddRepairOrderItem(null));
    }

    [Fact]
    public void AddRepairOrderItem_DuplicateRepairOrderItem_ThrowsDomainException()
    {
        var repairOrderItem = new RepairOrderItemInfo(1, 1, od, "Normal", 1, "mountainBike", "iets", 1, 15, "fix", 50, 1, "adrian", "adr@stu.hog", 25);

        sro.AddRepairOrderItem(repairOrderItem);

        Assert.Throws<DomainException>(() => sro.AddRepairOrderItem(repairOrderItem));
    }

    [Fact]
    public void RemoveRepairOrderItem_NullRepairOrderItem_ThrowsDomainException()
    {
        Assert.Throws<DomainException>(() => sro.RemoveRepairOrderItem(null));
    }

    [Fact]
    public void RemoveRepairOrderItem_NonExistingRepairOrderItem_ThrowsDomainException()
    {
        var repairOrderItem = new RepairOrderItemInfo(1, 1, od, "Normal", 1, "mountainBike", "iets", 1, 15, "fix", 50, 1, "adrian", "adr@stu.hog", 25);
        foreach (var item in sro.GetRepairOrderItems())
        {
            sro.RemoveRepairOrderItem(item);
        }
        Assert.Throws<DomainException>(() => sro.RemoveRepairOrderItem(repairOrderItem));
    }

    [Fact]
    public void GetCost_CalculatesCostCorrectly()
    {
        // Arrange
        var bike = DomainFactory.ExistingBike(1, BikeType.mountainBike, 50, "schoon", c);
        var repairman = DomainFactory.ExistingRepairman(1, "ad", "ab@stu.hog", 20);
        var repairTask = DomainFactory.ExistingRepairTask(1, "Fix Bike", 30, 50);
        var repairOrderItem1 = new RepairOrderItemInfo(1, (int)sro.ID!, od, sro.Urgency.ToString(), (int)bike.ID!, bike.BikeType.ToString(), bike.Description, 
            (int)repairTask.ID!, repairTask.RepairTime, repairTask.Description, repairTask.CostMaterials,
            (int)repairman.ID!, repairman.Name, repairman.Email, repairman.CostPerHour);

        sro.AddRepairOrderItem(repairOrderItem1);

        // Act
        var cost = sro.GetCost();

        // Assert
        Assert.Equal(60, cost);
    }
}

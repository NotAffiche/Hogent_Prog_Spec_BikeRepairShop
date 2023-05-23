using BikeRepairShopBL.Domain;
using BikeRepairShopBL.Exceptions;
using BikeRepairShopBL.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BikeRepairShopTest;

public class RepairOrderItemTest
{
    static Customer customer = DomainFactory.ExistingCustomer(1, "Adrian", "adrian@stu.hogent", "SN");
    static RepairOrder repairOrder = DomainFactory.ExistingRepairOrder(1, Urgency.Normal, DateOnly.FromDateTime(DateTime.Now), customer, false);
    static Bike bikeA = DomainFactory.ExistingBike(1, BikeType.regularBike, 1000, "rgbike");
    static Bike bikeB = DomainFactory.ExistingBike(2, BikeType.mountainBike, 2000, "mtbike");
    static RepairTask repairTaskA = DomainFactory.ExistingRepairTask(1, "fix bike", 30, 50);
    static RepairTask repairTaskB = DomainFactory.ExistingRepairTask(2, "fix bike more", 60, 250);
    static Repairman repairmanA = DomainFactory.ExistingRepairman(1, "adrian b", "ab@stu.hogent", 15);
    static Repairman repairmanB = DomainFactory.ExistingRepairman(2, "a b", "ba@stu.hogent", 30);


    [Fact]
    public void RepairOrderItemTest_SetId_NegativeId_ThrowsDomainException()
    {
        Assert.Throws<DomainException>(() => DomainFactory.ExistingRepairOrderItem(-1, repairOrder, bikeA, repairTaskA, repairmanA));
    }

    [Fact]
    public void RepairOrderItemTest_SetRepairOrder_ValidRepairOrder()
    {
        var roi = DomainFactory.ExistingRepairOrderItem(1, DomainFactory.ExistingRepairOrder(2, Urgency.Fast, DateOnly.FromDateTime(DateTime.Now), 
            DomainFactory.ExistingCustomer(6, "iemand", "anders@gmail", "brussel"), false), bikeA, repairTaskA, repairmanA);

        roi.SetRepairOrder(repairOrder);
        Assert.Equal(repairOrder, roi.RepairOrder);
    }

    [Fact]
    public void RepairOrderItemTest_SetBike_ValidBike()
    {
        var roi = DomainFactory.ExistingRepairOrderItem(1, repairOrder, bikeA, repairTaskA, repairmanA);

        Assert.Equal(bikeA, roi.Bike);
    }
    [Fact]
    public void RepairOrderItemTest_ChangeBike_ValidBike()
    {
        var roi = DomainFactory.ExistingRepairOrderItem(1, repairOrder, bikeA, repairTaskA, repairmanA);
        Assert.Equal(bikeA, roi.Bike);
        roi.SetBike(bikeB);
        Assert.Equal(bikeB, roi.Bike);
    }

    [Fact]
    public void RepairOrderItemTest_SetBike_NullBike_ThrowsDomainException()
    {
        var roi = DomainFactory.ExistingRepairOrderItem(1, repairOrder, bikeA, repairTaskA, repairmanA);
        Assert.Throws<DomainException>(() => roi.SetBike(null));
    }

    [Fact]
    public void RepairOrderItemTest_SetRepairTask_ValidRepairTask()
    {
        var roi = DomainFactory.ExistingRepairOrderItem(1, repairOrder, bikeA, repairTaskA, repairmanA);
        Assert.Equal(repairTaskA, roi.RepairTask);
    }
    [Fact]
    public void RepairOrderItemTest_ChangeRepairTask_ValidRepairTask()
    {
        var roi = DomainFactory.ExistingRepairOrderItem(1, repairOrder, bikeA, repairTaskA, repairmanA);
        Assert.Equal(repairTaskA, roi.RepairTask);
        roi.SetRepairTask(repairTaskB);
        Assert.Equal(repairTaskB, roi.RepairTask);
    }

    [Fact]
    public void RepairOrderItemTest_SetRepairTask_NullRepairTask_ThrowsDomainException()
    {
        var roi = DomainFactory.ExistingRepairOrderItem(1, repairOrder, bikeA, repairTaskA, repairmanA);
        Assert.Throws<DomainException>(() => roi.SetBike(null));
    }

    [Fact]
    public void RepairOrderItemTest_SetRepairman_ValidRepairman()
    {
        var roi = DomainFactory.ExistingRepairOrderItem(1, repairOrder, bikeA, repairTaskA, repairmanA);
        Assert.Equal(repairmanA, roi.Repairman);
    }
    [Fact]
    public void RepairOrderItemTest_ChangeRepairman_ValidRepairman()
    {
        var roi = DomainFactory.ExistingRepairOrderItem(1, repairOrder, bikeA, repairTaskA, repairmanA);
        Assert.Equal(repairmanA, roi.Repairman);
        roi.SetRepairman(repairmanB);
        Assert.Equal(repairmanB, roi.Repairman);
    }

    [Fact]
    public void RepairOrderItemTest_SetRepairman_NullRepairman()
    {
        var roi = DomainFactory.ExistingRepairOrderItem(1, repairOrder, bikeA, repairTaskA, repairmanA);
        Assert.Throws<DomainException>(() => roi.SetRepairman(null));
    }
}

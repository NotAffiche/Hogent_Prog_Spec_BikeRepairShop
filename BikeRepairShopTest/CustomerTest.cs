using Xunit;
using System;
using BikeRepairShopBL.Domain;
using BikeRepairShopBL.Exceptions;
using BikeRepairShopBL.Factories;

namespace BikeRepairShopTest;

public class CustomerTest
{
    [Fact]
    public void SetId_NegativeId_ThrowsDomainException()
    {
        var customer = DomainFactory.ExistingCustomer(1, "Adrian", "ad@stu.hog", "SN");

        Assert.Throws<DomainException>(() => customer.SetId(-1));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void SetName_NullOrWhiteSpaceName_ThrowsDomainException(string newName)
    {
        var customer = DomainFactory.ExistingCustomer(1, "Adrian", "ad@stu.hog", "SN");

        Assert.Throws<DomainException>(() => customer.SetName(newName));
        Assert.Throws<DomainException>(() => customer.SetName(newName));
        Assert.Throws<DomainException>(() => customer.SetName(newName));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void SetEmail_NullOrWhiteSpaceEmail_ThrowsDomainException(string newEmail)
    {
        var customer = DomainFactory.ExistingCustomer(1, "Adrian", "ad@stu.hog", "SN");

        Assert.Throws<DomainException>(() => customer.SetEmail(newEmail));
        Assert.Throws<DomainException>(() => customer.SetEmail(newEmail));
        Assert.Throws<DomainException>(() => customer.SetEmail(newEmail));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void SetAddress_NullOrWhiteSpaceAddress_ThrowsDomainException(string newAdd)
    {
        var customer = DomainFactory.ExistingCustomer(1, "Adrian", "ad@stu.hog", "SN");

        Assert.Throws<DomainException>(() => customer.SetAddress(newAdd));
        Assert.Throws<DomainException>(() => customer.SetAddress(newAdd));
        Assert.Throws<DomainException>(() => customer.SetAddress(newAdd));
    }

    [Fact]
    public void AddBike_NullBike_ThrowsDomainException()
    {
        var customer = DomainFactory.ExistingCustomer(1, "Adrian", "ad@stu.hog", "SN");

        Assert.Throws<DomainException>(() => customer.AddBike(null));
    }

    [Fact]
    public void AddBike_AddsBikeToCustomer()
    {
        var customer = DomainFactory.ExistingCustomer(1, "Adrian", "ad@stu.hog", "SN");
        var bike = DomainFactory.ExistingBike(1, BikeType.mountainBike, 50, "schoon", customer);

        Assert.Contains(bike, customer.GetBikes());
        Assert.Equal(customer, bike.Customer);
    }

    [Fact]
    public void RemoveBike_NullBike_ThrowsDomainException()
    {
        var customer = DomainFactory.ExistingCustomer(1, "Adrian", "ad@stu.hog", "SN");

        Assert.Throws<DomainException>(() => customer.RemoveBike(null));
    }

    [Fact]
    public void RemoveBike_RemovesBikeFromCustomer()
    {
        var customer = DomainFactory.ExistingCustomer(1, "Adrian", "ad@stu.hog", "SN");
        var bike = DomainFactory.ExistingBike(1, BikeType.mountainBike, 50, "schoon", customer);

        customer.RemoveBike(bike);

        Assert.DoesNotContain(bike, customer.GetBikes());
    }

    [Fact]
    public void AddRepairOrder_NullRepairOrder_ThrowsDomainException()
    {
        var customer = DomainFactory.ExistingCustomer(1, "Adrian", "ad@stu.hog", "SN");

        Assert.Throws<DomainException>(() => customer.AddRepairOrder(null));
    }

    [Fact]
    public void AddRepairOrder_AddsRepairOrderToCustomer()
    {
        var customer = DomainFactory.ExistingCustomer(1, "Adrian", "ad@stu.hog", "SN");
        var repairOrder = DomainFactory.ExistingRepairOrder(1, Urgency.Normal, DateOnly.FromDateTime(DateTime.Now), customer, false);

        Assert.Contains(repairOrder, customer.GetRepairOrders());
        Assert.Equal(customer, repairOrder.Customer);
    }

    [Fact]
    public void RemoveRepairOrder_NullRepairOrder_ThrowsDomainException()
    {
        var customer = DomainFactory.ExistingCustomer(1, "Adrian", "ad@stu.hog", "SN");

        Assert.Throws<DomainException>(() => customer.RemoveRepairOrder(null));
    }

    [Fact]
    public void RemoveRepairOrder_RemovesRepairOrderFromCustomer()
    {
        var customer = DomainFactory.ExistingCustomer(1, "Adrian", "ad@stu.hog", "SN");
        var repairOrder = DomainFactory.ExistingRepairOrder(1, Urgency.Normal, DateOnly.FromDateTime(DateTime.Now), customer, false);

        customer.RemoveRepairOrder(repairOrder);

        Assert.DoesNotContain(repairOrder, customer.GetRepairOrders());
    }
}
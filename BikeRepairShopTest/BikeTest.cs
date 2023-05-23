using Xunit;
using System;
using BikeRepairShopBL.Domain;
using BikeRepairShopBL.Factories;
using BikeRepairShopBL.DTO;
using BikeRepairShopBL.Exceptions;

namespace BikeRepairShopTest;

public class BikeTest
{
    [Fact]
    public void BikeTest_NewBike_DomainFactory_NewBike_IsValid()
    {
        CustomerInfo validCustomerInfo = new CustomerInfo(1, "Adrian", "adrian@stu.hogent", "SN", 1, 50, 0);
        BikeInfo validBikeInfo = new BikeInfo(1, "nice bike", BikeType.mountainBike, 50, (int)validCustomerInfo.Id!, $"{validCustomerInfo.Name} ({validCustomerInfo.Email})");
        Bike b = DomainFactory.NewBike(validBikeInfo);
    }

    [Fact]
    public void BikeTest_SetId_InvalidId_ThrowsDomainException()
    {
        var bike = DomainFactory.ExistingBike(null, BikeType.regularBike, 1000, "Reg Bike");

        Assert.Throws<DomainException>(() => bike.SetId(-1));
    }

    [Fact]
    public void BikeTest_SetPurchaseCost_InvalidPurchaseCost_ThrowsDomainException()
    {
        var bike = DomainFactory.ExistingBike(null, BikeType.regularBike, 1500, "bike");

        Assert.Throws<DomainException>(() => bike.SetPurchaseCost(-100));
    }

    [Fact]
    public void BikeTest_SetCustomer_ValidCustomer()
    {
        var bike = DomainFactory.ExistingBike(null, BikeType.regularBike, 500, "sn bike");
        CustomerInfo validCustomerInfo = new CustomerInfo(1, "Adrian", "adrian@stu.hogent", "SN", 1, 50, 0);
        var customer = DomainFactory.NewCustomer(validCustomerInfo);

        bike.SetCustomer(customer);

        Assert.Equal(customer, bike.Customer);
        Assert.Contains(bike, customer.GetBikes());
    }

    [Fact]
    public void BikeTest_SetCustomer_NullCustomer_ThrowsDomainException()
    {
        var bike = DomainFactory.ExistingBike(null, BikeType.regularBike, 1200, "tech bike");

        Assert.Throws<DomainException>(() => bike.SetCustomer(null));
    }

    [Fact]
    public void BikeTest_Equals_TwoBikesWithSameProperties_ReturnsTrue()
    {
        var bike1 = DomainFactory.ExistingBike(null, BikeType.regularBike, 1000, "Mountain Bike");
        var bike2 = DomainFactory.ExistingBike(null, BikeType.regularBike, 1000, "Mountain Bike");

        var result = bike1.Equals(bike2);

        Assert.True(result);
    }
}
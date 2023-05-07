using Xunit;
using System;
using BikeRepairShopBL.Domain;

namespace BikeRepairShopTest;

public class BikeTest : IDisposable
{
    Bike bikeTestCase;
    public BikeTest()
    {
        //setup test
    }
    public void Dispose()
    {
        //close down test
        //close down db?
    }

    [Fact]
    public void TestConstructor()
    {
        //arrange
        bikeTestCase = new Bike(1, null, 457);
        //act

        //assert
        Assert.True(bikeTestCase.ID > 0);
        Assert.True(bikeTestCase.PurchaseCost>0);
    }
}
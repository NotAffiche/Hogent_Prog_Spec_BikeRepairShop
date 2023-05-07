using Xunit;
using System;
using BikeRepairShopBL.Domain;

namespace BikeRepairShopTest;

public class CustomerTest : IDisposable
{
    Customer customerTestCase;
    Bike bikeA;
    Bike bikeB;
    Bike bikeC;

    public CustomerTest() 
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
        customerTestCase = new Customer(1, "Tester", "TestAddress", "test@test.test");
        //act

        //assert
        Assert.True(customerTestCase.ID>0);
        Assert.True(!customerTestCase.Name.Equals(string.IsNullOrWhiteSpace));
        Assert.True(!customerTestCase.Address.Equals(string.IsNullOrWhiteSpace));
        Assert.True(!customerTestCase.Email.Equals(string.IsNullOrWhiteSpace));
    }

    [Fact]
    public void TestBikes()
    {
        customerTestCase = new Customer(1, "Tester", "TestAddress", "test@test.test");
        bikeA = new Bike(1, null, 420.69);
        bikeB = new Bike(1, "kapot voorwiel", 12.96);
        bikeC = new Bike(3, "", 3);


        //todo 
    }
}
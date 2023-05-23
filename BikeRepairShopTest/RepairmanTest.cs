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

public class RepairmanTest
{
    [Fact]
    public void RepairmanTest_SetId_NegativeId_ThrowsDomainException()
    {
        Assert.Throws<DomainException>(() => DomainFactory.ExistingRepairman(-1, "adrian b", "ab@stu.hogent", 15));
    }

    [Fact]
    public void RepairmanTest_SetName_NullOrWhiteSpaceName_ThrowsDomainException()
    {
        Assert.Throws<DomainException>(() => DomainFactory.ExistingRepairman(1, "  ", "ab@stu.hogent", 15));
    }

    [Fact]
    public void RepairmanTest_SetEmail_NullOrWhiteSpaceEmail_ThrowsDomainException()
    {
        Assert.Throws<DomainException>(() => DomainFactory.ExistingRepairman(1, "adrian b", "", 15));
    }

    [Fact]
    public void SetCostPerHour_NegativeCostPerHour_ThrowsDomainException()
    {
        Assert.Throws<DomainException>(() => DomainFactory.ExistingRepairman(1, "adrian b", "ab@stu.hogent", -15));
    }

    [Fact]
    public void RepairmanTest_SetId_ValidId()
    {
        var repairman = DomainFactory.ExistingRepairman(null, "adrian b", "ab@stu.hogent", 15);

        repairman.SetId(1);

        Assert.Equal(1, repairman.ID);
    }

    [Fact]
    public void RepairmanTest_SetName_ValidName()
    {
        var repairman = DomainFactory.ExistingRepairman(null, "test", "ab@stu.hogent", 15);

        repairman.SetName("adrian b");

        Assert.Equal("adrian b", repairman.Name);
    }

    [Fact]
    public void RepairmanTest_SetEmail_ValidEmail()
    {
        var repairman = DomainFactory.ExistingRepairman(null, "adrian b", "test", 15);

        repairman.SetEmail("ab@stu.hogent");

        Assert.Equal("ab@stu.hogent", repairman.Email);
    }

    [Fact]
    public void RepairmanTest_SetCostPerHour_ValidCostPerHour()
    {
        var repairman = DomainFactory.ExistingRepairman(null, "adrian b", "ab@stu.hogent", 0);

        repairman.SetCostPerHour(15);

        Assert.Equal(15, repairman.CostPerHour);
    }
}

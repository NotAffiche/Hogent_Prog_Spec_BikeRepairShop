using BikeRepairShopBL.Domain;
using BikeRepairShopBL.DTO;
using BikeRepairShopBL.Factories;
using BikeRepairShopBL.Interfaces;
using BikeRepairShopBL.Managers;
using BikeRepairShopDL.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeRepairShopTest;

public class DatabaseFixture : IDisposable
{
    private string connString = @"Data Source=.\SQLEXPRESS;Initial Catalog=BikeRepairShopTest;Integrated Security=True";
    public RepairmanRepositoryHelper dbHelper;
    private void CleanDB()
    {
        string clearDB = "DELETE FROM Repairman;DBCC CHECKIDENT ('Repairman', RESEED, 0);";
        using (SqlConnection conn = new SqlConnection(connString))
        using (SqlCommand command = conn.CreateCommand())
        {
            conn.Open();
            command.CommandText = clearDB;
            command.ExecuteNonQuery();
        }
    }
    public DatabaseFixture()
    {
        CleanDB();
        repairmanRepo = new RepairmanRepository(connString);
        dbHelper = new RepairmanRepositoryHelper(connString);
        // ... initialize data in the test database ...
    }
    public void Dispose()
    {
        // ... clean up test data from the database ...
        CleanDB();
    }
    public IRepairmanRepository repairmanRepo { get; private set; }
}

public class TestRepairmanManager : IClassFixture<DatabaseFixture>
{
    DatabaseFixture fixture;
    RepairmanManager repairmanManager;

    public TestRepairmanManager(DatabaseFixture fixture)
    {
        this.fixture = fixture;
        repairmanManager = new RepairmanManager(fixture.repairmanRepo);
    }
    [Fact, Priority(1)]
    public void Test_AddRepairman_Valid()
    {
        //Test();
        RepairmanInfo repairmanInfo = new RepairmanInfo(null, "Jose", "Jose@FietsCentral", 17.5);

        repairmanManager.AddRepairman(repairmanInfo);
        var bDB = repairmanManager.GetRepairmanInfos();

        Xunit.Assert.NotNull(bDB);
        Xunit.Assert.Contains(repairmanInfo, bDB);
    }
    [Fact, Priority(2)]
    public void Test_UpdateRepairman()
    {
        RepairmanInfo repairmanInfo = new RepairmanInfo(null, "Jose", "Jose@FietsCentral", 17.5);
        repairmanManager.AddRepairman(repairmanInfo);
        Repairman repairman = repairmanManager.GetRepairman(1);
        repairman.SetName("Nieuwe Jose");
        RepairmanInfo ri = new RepairmanInfo(repairman.ID, repairman.Name, repairman.Email, repairman.CostPerHour);

        repairmanManager.UpdateRepairman(ri);
        var rDB = repairmanManager.GetRepairmanInfos();

        Xunit.Assert.NotNull(rDB);
        Xunit.Assert.Contains(ri, rDB);
    }
    [Fact, Priority(3)]
    public void Test_DeleteRepairman()
    {
        RepairmanInfo repairmanInfo = new RepairmanInfo(null, "Jose", "Jose@FietsCentral", 17.5);
        repairmanManager.AddRepairman(repairmanInfo);
        Repairman repairman = repairmanManager.GetRepairman(1);
        RepairmanInfo ri = new RepairmanInfo(repairman.ID, repairman.Name, repairman.Email, repairman.CostPerHour);
        repairmanManager.DeleteRepairman(ri);
        Xunit.Assert.Equal(0, fixture.dbHelper.Status((int)ri.Id!));
    }
}

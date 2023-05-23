using BikeRepairShopBL.Domain;
using BikeRepairShopBL.Factories;
using BikeRepairShopDL.Exceptions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeRepairShopTest;

public class RepairmanRepositoryHelper
{
    private string connectionString;

    public RepairmanRepositoryHelper(string connectionString)
    {
        this.connectionString = connectionString;
    }
    public int Status(int id)
    {
        try
        {
            string sql = "SELECT status FROM repairman WHERE id=@id";
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                connection.Open();
                command.CommandText = sql;
                command.Parameters.AddWithValue("@id", id);
                int status = (int)command.ExecuteScalar();
                return status;
            }
        }
        catch (Exception ex) { throw new RepositoryException("Delete", ex); }
    }
}

using BikeRepairShopBL.Domain;
using BikeRepairShopBL.DTO;
using BikeRepairShopBL.Factories;
using BikeRepairShopBL.Interfaces;
using BikeRepairShopDL.Exceptions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BikeRepairShopDL.Repositories;

public class RepairmanRepository : IRepairmanRepository
{
    private string connectionString;

    public RepairmanRepository(string connectionString)
    {
        this.connectionString = connectionString;
    }

    public void AddRepairman(Repairman r)
    {
        try
        {
            string sql = "INSERT INTO repairman(name,email,costperhour,status) output INSERTED.ID VALUES(@name,@email,@costperhour,@status)";
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();
                try
                {
                    command.Transaction = transaction;
                    command.CommandText = sql;
                    command.Parameters.AddWithValue("@name", r.Name);
                    command.Parameters.AddWithValue("@email", r.Email);
                    command.Parameters.AddWithValue("@costperhour", r.CostPerHour);
                    command.Parameters.AddWithValue("@status", 1);
                    int id = (int)command.ExecuteScalar();
                    r.SetId(id);
                    transaction.Commit();
                }
                catch (Exception) { transaction.Rollback(); throw; }
            }
        }
        catch (Exception ex) { throw new RepositoryException("AddRepairman", ex); }
    }

    public void DeleteRepairman(Repairman r)
    {
        try
        {
            string sql = "UPDATE repairman SET status=0 WHERE id=@id and status=1";
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                connection.Open();
                command.CommandText = sql;
                command.Parameters.AddWithValue("@id", r.ID);
                command.ExecuteNonQuery();
            }
        }
        catch (Exception ex) { throw new RepositoryException("DeleteRepairman", ex); }
    }

    public Repairman GetRepairman(int id)
    {
        try
        {
            string sql = "SELECT * FROM repairman WHERE id=@id AND status=1";
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                connection.Open();
                command.CommandText = sql;
                command.Parameters.AddWithValue("@id", id);
                IDataReader reader = command.ExecuteReader();
                string name = null, email = null;
                double cph = 0;
                while (reader.Read())
                {
                    name = (string)reader["name"];
                    email = (string)reader["email"];
                    cph = (double)reader["costperhour"];
                }
                reader.Close();
                return DomainFactory.ExistingRepairman(id, name, email, cph);
            }
        }
        catch (Exception ex) { throw new RepositoryException("GetRepairman id", ex); }
    }

    public Repairman GetRepairman(string email)
    {
        try
        {
            string sql = "SELECT * FROM repairman WHERE email=@email AND status=1";
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                connection.Open();
                command.CommandText = sql;
                command.Parameters.AddWithValue("@email", email);
                IDataReader reader = command.ExecuteReader();
                int id = 0;
                string name = null;
                double cph = 0;
                while (reader.Read())
                {
                    id = (int)reader["id"];
                    name = (string)reader["name"];
                    cph = (double)reader["costperhour"];
                }
                reader.Close();
                return DomainFactory.ExistingRepairman(id, name, email, cph);
            }
        }
        catch (Exception ex) { throw new RepositoryException("GetRepairman email", ex); }
    }

    public List<RepairmanInfo> GetRepairmanInfos(string? name = null)
    {
        try
        {
            string sql;
            if (name == null)
                sql = "SELECT * FROM repairman WHERE status=1;";
            else
                sql = "SELECT * FROM repairman WHERE status=1 AND name LIKE @namematch;";
            List<RepairmanInfo> repairmen = new List<RepairmanInfo>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                connection.Open();
                command.CommandText = sql;
                if (name != null) command.Parameters.AddWithValue("@namematch", $"%{name}%");
                IDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    //new CustomerInfo((int)reader["id"], (string)reader["name"], (string)reader["email"], (string)reader["address"], (int)reader["nrofbikes"], (double)reader["totalcost"])
                    repairmen.Add(new RepairmanInfo((int)reader["id"], (string)reader["name"], (string)reader["email"], (double)reader["costperhour"]));
                }
                reader.Close();
                return repairmen;
            }
        }
        catch (Exception ex) { throw new RepositoryException("GetRepairmanInfos", ex); }
    }

    public void UpdateRepairman(Repairman r)
    {
        try
        {
            string sql = "UPDATE repairman SET name=@name,email=@email,costperhour=@costperhour WHERE id=@id and status=1";
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                connection.Open();
                command.CommandText = sql;
                command.Parameters.AddWithValue("@name", r.Name);
                command.Parameters.AddWithValue("@email", r.Email);
                command.Parameters.AddWithValue("@costperhour", r.CostPerHour);
                command.Parameters.AddWithValue("@id", r.ID);
                command.ExecuteNonQuery();
            }
        }
        catch (Exception ex) { throw new RepositoryException("UpdateRepairman", ex); }
    }
}

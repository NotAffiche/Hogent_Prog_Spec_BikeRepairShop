﻿using BikeRepairShopBL.Domain;
using BikeRepairShopBL.DTO;
using BikeRepairShopBL.Factories;
using BikeRepairShopBL.Interfaces;
using BikeRepairShopDL.Exceptions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeRepairShopDL.Repositories;

public class RepairOrderRepository : IRepairOrderRepository
{
    private string connectionString;

    public RepairOrderRepository(string connectionString)
    {
        this.connectionString = connectionString;
    }

    public List<RepairOrderInfo> GetRepairOrderInfos(Customer c)
    {
        List<RepairOrderInfo> repairOrderInfos = new List<RepairOrderInfo>();
        try
        {
            string sql = "SELECT ro.id, ro.urgency, ro.orderdate, ro.customerid, ro.paid, ro.status, c.Id as cid, c.Name, c.Email, c.Status FROM RepairOrder ro JOIN Customer c on ro.CustomerId=c.Id " +
                "WHERE ro.Status=1 AND c.Status=1 AND ro.CustomerId=@custId;";
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("@custId", c.ID);
                IDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    DateTime dt = reader.GetDateTime(2);
                    RepairOrderInfo roi = new RepairOrderInfo((int)reader["id"], (string)reader["urgency"], Convert.ToBoolean((int)reader["paid"]), new DateOnly(dt.Year, dt.Month, dt.Day), (int)reader["cid"], $"{(string)reader["name"]} ({(string)reader["email"]})");
                    repairOrderInfos.Add(roi);
                }
                reader.Close();
            }
            return repairOrderInfos;
        }
        catch (Exception ex)
        {
            throw new RepositoryException("RepairOrderRepo-GetRepairOrderInfos", ex);
        }
    }

    public List<RepairOrderItemInfo> GetRepairOrderItemInfos(RepairOrder ro)
    {
        List<RepairOrderItemInfo> repairOrderItemInfos = new List<RepairOrderItemInfo>();
        try
        {
            string sql = "SELECT * WHERE ro.Status=1 AND ro.Id=@roId;";
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("@roId", ro.ID);
                IDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    DateTime dt = reader.GetDateTime(2);
                    RepairOrderInfo roi = new RepairOrderInfo((int)reader["id"], (string)reader["urgency"], Convert.ToBoolean((int)reader["paid"]), new DateOnly(dt.Year, dt.Month, dt.Day), (int)reader["cid"], $"{(string)reader["name"]} ({(string)reader["email"]})");
                    repairOrderItemInfos.Add(roi);
                }
                reader.Close();
            }
            return repairOrderItemInfos;
        }
        catch (Exception ex)
        {
            throw new RepositoryException("RepairOrderRepo-GetRepairOrderInfos", ex);
        }
    }

    public List<RepairTaskInfo> GetRepairTaskInfos()
    {
        List<RepairTaskInfo> repairOrderInfos = new List<RepairTaskInfo>();
        try
        {
            string sql = "SELECT * FROM RepairTask WHERE Status=1;";
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandText = sql;
                IDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    repairOrderInfos.Add(new RepairTaskInfo((int)reader["id"], (string)reader["description"], (int)reader["repairtime"], (double)reader["costmaterials"]));
                }
                reader.Close();
            }
            return repairOrderInfos;
        }
        catch (Exception ex)
        {
            throw new RepositoryException("RepairOrderRepo-GetRepairTaskInfos", ex);
        }
    }
}
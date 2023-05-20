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

    //

    #region get
    public RepairOrder GetRepairOrder(int id)
    {
        RepairOrder? ro = null;
        try
        {
            string sql = "SELECT * FROM RepairOrder ro JOIN Customer c ON ro.CustomerId=c.Id WHERE ro.Status=1 AND c.Status=1 AND ro.Id=@roId";
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("@roId", id);
                IDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    DateTime dt = reader.GetDateTime(2);
                    Customer c = DomainFactory.ExistingCustomer((int)reader[6], (string)reader["name"], (string)reader["email"], (string)reader["address"], null, null);
                    ro = DomainFactory.ExistingRepairOrder((int)reader[0], (Urgency)Enum.Parse(typeof(Urgency), (string)reader["urgency"]), new DateOnly(dt.Year, dt.Month, dt.Day), c, Convert.ToBoolean((int)reader["paid"]));
                }
                reader.Close();
            }
            return ro;
        }
        catch (Exception ex)
        {
            throw new RepositoryException("RepairOrderRepo-GetRepairOrder", ex);
        }
    }
    public List<RepairOrderInfo> GetRepairOrderInfos(int custId)
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
                cmd.Parameters.AddWithValue("@custId", custId);
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
    public List<RepairOrderItemInfo> GetRepairOrderItemInfos(RepairOrderInfo ro)
    {
        List<RepairOrderItemInfo> repairOrderItemInfos = new List<RepairOrderItemInfo>();
        try
        {
            string sql = "SELECT * FROM RepairOrderItem roi JOIN Bike b ON roi.BikeId=b.Id JOIN RepairTask rt ON roi.RepairTaskId=rt.Id JOIN Repairman rm ON roi.RepairmanId=rm.Id WHERE roi.Status=1 AND roi.RepairOrderId=@roId;";
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("@roId", ro.ID);
                IDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    RepairOrderItemInfo roii = new RepairOrderItemInfo(
                        (int)reader[0],//id
                        (int)ro.ID,//reporderid
                        ro.OrderDate,//reporderdate
                        ro.Urgency,//reporder urgency
                        (int)reader[2],//bikeid
                        (string)reader[7],//biketype
                        (string)reader[8],//bikedesc
                        (int)reader[12],//taskid
                        (int)reader[14],//reptime
                        (string)reader[13],//taskdesc
                        (double)reader[15],//matcost
                        (int)reader[17],//repman id
                        (string)reader[18],//repman name
                        (string)reader[19],//repman email
                        (double)reader[20]//repman cph
                        );
                    repairOrderItemInfos.Add(roii);
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
    public RepairTask GetRepairTask(int id)
    {
        RepairTask? repairTask = null;
        try
        {
            string sql = "SELECT * FROM RepairTask WHERE Status=1 AND Id=@id;";
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("@id", id);
                IDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    repairTask = DomainFactory.ExistingRepairTask(id, (string)reader["description"], (int)reader["repairtime"], (double)reader["costmaterials"]);
                }
                reader.Close();
            }
            return repairTask;
        }
        catch (Exception ex)
        {
            throw new RepositoryException("RepairOrderRepo-GetRepairTaskInfos", ex);
        }
    }
    #endregion

    #region add
    public void AddRepairOrder(RepairOrder ro)
    {
        try
        {
            string sqlC = "INSERT INTO repairorder(urgency,orderdate,customerid,paid,status) output INSERTED.ID VALUES(@urgency,@orderdate,@customerid,@paid,@status)";
            string sqlB = "INSERT INTO repairorderitem(repairoderid,bikeid,repairtaskid,repairmanid,status) output INSERTED.ID VALUES(@repairoderid,@bikeid,@repairtaskid,@repairmanid,@status)";
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();
                try
                {
                    command.Transaction = transaction;
                    command.CommandText = sqlC;
                    command.Parameters.AddWithValue("@urgency", ro.Urgency.ToString());
                    command.Parameters.AddWithValue("@orderdate", new DateTime(ro.OrderDate.Year, ro.OrderDate.Month, ro.OrderDate.Day));
                    command.Parameters.AddWithValue("@customerid", ro.Customer.ID);
                    command.Parameters.AddWithValue("@paid", ro.Paid);
                    command.Parameters.AddWithValue("@status", 1);
                    int id = (int)command.ExecuteScalar();
                    ro.SetId(id);
                    command.CommandText = sqlB;
                    foreach (RepairOrderItem roi in ro.GetRepairOrderItems())
                    {
                        command.Parameters.Clear();
                        command.Parameters.AddWithValue("@repairoderid", roi.RepairOrder.ID);
                        command.Parameters.AddWithValue("@bikeid", roi.Bike.ID);
                        command.Parameters.AddWithValue("@repairtaskid", roi.RepairTask.ID);
                        command.Parameters.AddWithValue("@repairmanid", roi.Repairman.ID);
                        command.Parameters.AddWithValue("@status", 1);
                        int roiId = (int)command.ExecuteScalar();
                        roi.SetId(roiId);
                    }
                    transaction.Commit();
                }
                catch (Exception) { transaction.Rollback(); throw; }
            }
        }
        catch (Exception ex) { throw new RepositoryException("AddRepairOrder", ex); }
    }

    public void AddRepairOrderItem(RepairOrderItem roi)
    {
        try
        {
            string sqlB = "INSERT INTO repairorderitem(repairorderid,bikeid,repairtaskid,repairmanid,status) output INSERTED.ID VALUES(@repairoderid,@bikeid,@repairtaskid,@repairmanid,@status)";
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();
                try
                {
                    command.Transaction = transaction;
                    command.CommandText = sqlB;
                    command.Parameters.AddWithValue("@repairoderid", roi.RepairOrder.ID);
                    command.Parameters.AddWithValue("@bikeid", roi.Bike.ID);
                    command.Parameters.AddWithValue("@repairtaskid", roi.RepairTask.ID);
                    command.Parameters.AddWithValue("@repairmanid", roi.Repairman.ID);
                    command.Parameters.AddWithValue("@status", 1);
                    int id = (int)command.ExecuteScalar();
                    roi.SetId(id);
                    transaction.Commit();
                }
                catch (Exception) { transaction.Rollback(); throw; }
            }
        }
        catch (Exception ex) { throw new RepositoryException("AddRepairOrderItem", ex); }
    }
    #endregion

    #region remove
    public void RemoveRepairOrder(RepairOrder ro)
    {
        throw new NotImplementedException();
    }

    public void RemoveRepairOrderItem(RepairOrderItem roi)
    {
        throw new NotImplementedException();
    }
    #endregion

    #region update
    public void UpdateRepairOrder(RepairOrder ro)
    {
        throw new NotImplementedException();
    }

    public void UpdateRepairOrderItem(RepairOrderItem roi)
    {
        throw new NotImplementedException();
    }
    #endregion
}

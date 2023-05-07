using BikeRepairShopBL.Domain;
using BikeRepairShopBL.DTO;
using BikeRepairShopBL.Interfaces;
using BikeRepairShopDL.Exceptions;
using System.Data;
using System.Data.SqlClient;

namespace BikeRepairShopDL.Repositories;

public class CustomerBikeRepository : ICustomerBikeRepository
{
    private string connectionString;

    public CustomerBikeRepository(string connectionString)
    {
        this.connectionString = connectionString;
    }

    public List<BikeInfo> GetBikesInfo()
    {
        List<BikeInfo> bikeInfos= new List<BikeInfo>();
        try
        {
            string sql = "SELECT b.*, c.Name, c.Email FROM Bike b INNER JOIN Customer c ON b.CustomerId=c.Id WHERE b.Status=1 AND c.Status=1;";
            using (SqlConnection conn =  new SqlConnection(connectionString))
            using (SqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandText = sql;
                IDataReader reader = cmd.ExecuteReader();
                while(reader.Read())
                {
                    //string d = reader.IsDBNull(reader.GetOrdinal("description")) ? null : (string)reader["description"];
                    //Enum.Parse(typeof(BikeInfo), (string)reader["biketype"], true);
                    bikeInfos.Add(
                        new BikeInfo(
                            (int)reader["id"], 
                        reader.IsDBNull(reader.GetOrdinal("description")) ? null : (string)reader["description"], 
                        (BikeType)Enum.Parse(typeof(BikeType), (string)reader["biketype"], true), 
                        (double)reader["purchasecost"], (int)reader["customerid"], 
                        (string)reader["name"]+" (" + (string)reader["email"]+")"));
                }
                reader.Close();
            }
            return bikeInfos;
        }
        catch (Exception ex)
        {
            throw new RepositoryException("CustomerBikeRepo-GetBikesInfo", ex);
        }
    }
    public void AddBike(Bike bike)
    {

    }

    public Customer GetCustomer(int id)
    {
        //todo
        return null;
    }
}

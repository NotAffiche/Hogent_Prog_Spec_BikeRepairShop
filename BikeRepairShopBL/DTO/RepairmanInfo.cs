using BikeRepairShopBL.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeRepairShopBL.DTO;

public class RepairmanInfo
{
    public RepairmanInfo(int? id, string name, string email, double cph)
    {
        Id = id;
        Name = name;
        Email = email;
        CostPerHour = cph;
    }

    public int? Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public double CostPerHour { get; set; }

    public override bool Equals(object? obj)
    {
        return obj is RepairmanInfo info &&
               Id == info.Id &&
               Name == info.Name &&
               Email == info.Email &&
               CostPerHour == info.CostPerHour;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Name, Email, CostPerHour);
    }
}

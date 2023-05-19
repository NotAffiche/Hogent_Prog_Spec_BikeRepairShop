using BikeRepairShopBL.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BikeRepairShopBL.Domain;

public class Repairman
{
    public int? ID { get; private set; }
    public string Name { get; private set; }
    public string Email { get; private set; }
    public double CostPerHour { get; private set; }

    internal Repairman(string name, string email, double cph)
    {
        SetName(name);
        SetEmail(email);
        SetCostPerHour(cph);
    }

    internal Repairman(int? id, string name, string email, double cph)
    {
        if (id.HasValue) SetId((int)id);
        SetName(name);
        SetEmail(email);
        SetCostPerHour(cph);
    }

    public void SetId(int id)
    {
        if (id <= 0) throw new DomainException("Repairman id");
        ID = id;
    }
    public void SetName(string name)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new DomainException("Repairman name");
        Name = name;
    }
    public void SetEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email)) throw new DomainException("Repairman email");
        Email = email;
    }
    public void SetCostPerHour(double cph)
    {
        if (cph < 0) throw new DomainException("Repairman cost per hour");
        CostPerHour = cph;
    }
}

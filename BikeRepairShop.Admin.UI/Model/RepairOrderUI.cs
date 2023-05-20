using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeRepairShop.Admin.UI.Model;

public class RepairOrderUI : INotifyPropertyChanged
{
    public RepairOrderUI()
    {

    }
    public RepairOrderUI(int? id, string urgency, DateOnly orderdate, bool paid/*, int custId, string custDesc*/)
    {
        ID = id;
        Urgency = urgency;
        OrderDate = orderdate;
        Paid = paid;
        //CustomerId = custId;
        //CustomerDescription = custDesc;
    }

    public int? ID { get; set; }
    private string _urgency;
    public string Urgency { get { return _urgency; } set { _urgency = value; OnPropertyChanged(); } }
    private DateOnly _orderDate;
    public DateOnly OrderDate { get { return _orderDate; } set { _orderDate = value; OnPropertyChanged(); } }
    private bool _paid;
    public bool Paid { get { return _paid; } set { _paid = value; OnPropertyChanged(); } }
    //private int _custId;
    //public int CustomerId { get { return _custId; } set { _custId = value; OnPropertyChanged(); } }
    //private string _custDesc;
    //public string CustomerDescription { get { return _custDesc; } set { _custDesc = value; OnPropertyChanged(); } }

    //event
    public event PropertyChangedEventHandler? PropertyChanged;
    private void OnPropertyChanged(string name = null)
    {
        PropertyChanged?.Invoke(name, new PropertyChangedEventArgs(name));
    }
}

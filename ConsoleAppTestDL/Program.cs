using BikeRepairShopDL.Repositories;
using System.Configuration;
//string conn = "Data Source=.\\SQLEXPRESS2;Initial Catalog=BikeRepairShop;Integrated Security=True";
string conn = ConfigurationManager.ConnectionStrings["ADOconnSQL"].ConnectionString;
CustomerBikeRepository repo = new CustomerBikeRepository(conn);
Console.WriteLine("==========================");
//repo.GetBikesInfo().ForEach(b => Console.WriteLine(b.Id));
var bikes = repo.GetBikesInfo();
bikes.ForEach(b => Console.WriteLine($"{b.Id} | {b.Description} | {b.BikeType} | {b.PurchaseCost} | {b.Customer}"));

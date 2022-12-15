using System.Data;
using Microsoft.Data.SqlClient;
using SignalR_SQLTableDependency.Models;

namespace SignalR_SQLTableDependency.Repositories
{
	public class ProductRepository
	{
		private readonly string connectionString;

		public ProductRepository(string connectionString)
		{
			this.connectionString = connectionString;
		}
		public List<Product> GetProducts()
		{
			List<Product> products = new();
			Product product;

			var data = GetProductDetailsFromDb();

			foreach (DataRow row in data.Rows)
			{
				product = new Product
				{
					Id = Convert.ToInt32(row["Id"]),
						Name = row["Name"].ToString(),
						Category = row["Category"].ToString(),
						Price = Convert.ToDecimal(row["Price"])
				};
				products.Add(product);
			}

			return products;
		}
		public DataTable GetProductDetailsFromDb()
		{
			const string query = "SELECT Id, Name, Category, Price FROM Products";
			DataTable dt = new();
			using(var connection = new SqlConnection(connectionString))
			{
				try
				{
					connection.Open();
					using(var command = new SqlCommand(query, connection))
					{
						using(var reader = command.ExecuteReader())
						{
							dt.Load(reader);
						}
					}
					return dt;
				}
				catch (SqlException)
				{
					throw;
				}
				finally
				{
					connection.Close();
				}
			}
		}
	}
}
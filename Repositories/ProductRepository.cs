using System.Data;
using Microsoft.Data.SqlClient;

namespace SignalR_SQLTableDependency.Repositories
{
	public class ProductRepository
	{
		private readonly string? connectionString;

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
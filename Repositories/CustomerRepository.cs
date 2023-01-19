using System.Data;
using Microsoft.Data.SqlClient;
using SignalR_SQLTableDependency.Models;

namespace SignalR_SQLTableDependency.Repositories;

public class CustomerRepository
{
	readonly string connectionString;

	public CustomerRepository(string connectionString)
	{
		this.connectionString = connectionString;
	}

	public List<Customer> GetCustomer()
	{
		List<Customer> customers = new();
		Customer customer;

		DataTable dataTable = GetCustomerDetailsFromDb();

		foreach (DataRow row in dataTable.Rows)
		{
			customer = new()
			{
				Id = Convert.ToInt32(row["Id"]),
					Name = row["Name"].ToString(),
					Gender = row["Gender"].ToString(),
					Mobile = row["Mobile"].ToString()
			};
			customers.Add(customer);
		}
		return customers;
	}

	private DataTable GetCustomerDetailsFromDb()
	{
		const string query = "Select Id, Name, Gender, Mobile FROM Customer";
		DataTable dataTable = new();

		using var connection = new SqlConnection(connectionString);
		try
		{
			connection.Open();
			using(var command = new SqlCommand(query, connection))
			{
				using var reader = command.ExecuteReader();
				dataTable.Load(reader);
			}
			return dataTable;
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

	public List<CustomerForGraph> GetCustomersForGraph()
	{
		List<CustomerForGraph> customersForGraph = new();
		CustomerForGraph customerForGraph;

		DataTable dataTable = GetCustomersForGraphFromDb();

		foreach (DataRow row in dataTable.Rows)
		{
			customerForGraph = new()
			{
				Gender = row["Gender"].ToString(),
				Customers = Convert.ToInt32(row["Customers"])
			};

			customersForGraph.Add(customerForGraph);
		}

		return customersForGraph;
	}

	private DataTable GetCustomersForGraphFromDb()
	{
		const string query = "SELECT Gender, COUNT(Id) Customers FROM Customer GROUP BY Gender";
		DataTable dataTable = new();

		using SqlConnection connection = new(connectionString);
		try
		{
			connection.Open();
			using(SqlCommand command = new(query, connection))
			{
				using var reader = command.ExecuteReader();
				dataTable.Load(reader);
			}
			return dataTable;
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
using System.Data;
using Microsoft.Data.SqlClient;
using SignalR_SQLTableDependency.Models;

namespace SignalR_SQLTableDependency.Repositories;

public class SaleRepository
{
	readonly string connectionString = null!;

	public SaleRepository(string connectionString)
	{
		this.connectionString = connectionString;
	}

	public List<Sale> GetSales()
	{
		List<Sale> sales = new();
		Sale sale;

		DataTable dataTable = GetSaleDetailsFromDb();

		foreach (DataRow row in dataTable.Rows)
		{
			sale = new()
			{
				Id = Convert.ToInt32(row["Id"]),
					Customer = row["Customer"].ToString(),
					Amount = Convert.ToDecimal(row["Amount"]),
					PurchasedOn = Convert.ToDateTime(row["PurchasedOn"])
			};

			sales.Add(sale);
		}

		return sales;
	}

	private DataTable GetSaleDetailsFromDb()
	{
		const string query = "SELECT Id, Customer, Amount, PurchasedOn FROM Sale";
		DataTable dataTable = new();

		using SqlConnection connection = new(connectionString);
		try
		{
			connection.Open();
			using(SqlCommand command = new(query, connection))
			{
				using SqlDataReader reader = command.ExecuteReader();
				dataTable.Load(reader);
			}
			return dataTable;
		}
		catch (Exception)
		{
			throw;
		}
		finally
		{
			connection.Close();
		}
	}

	public List<SaleForGraph> GetSalesForGraph()
	{
		List<SaleForGraph> salesForGraph = new();
		SaleForGraph saleForGraph;

		DataTable dataTable = GetSalesForGraphFromDb();

		foreach (DataRow row in dataTable.Rows)
		{
			saleForGraph = new()
			{
				PurchasedOn = Convert.ToDateTime(row["PurchasedOn"]).ToString("dd/MM/yyyy"),
				Amount = Convert.ToDecimal(row["Amount"])
			};

			salesForGraph.Add(saleForGraph);
		}

		return salesForGraph;
	}

	private DataTable GetSalesForGraphFromDb()
	{
		const string query = "SELECT  PurchasedOn, SUM(Amount) AS Amount FROM Sale GROUP BY PurchasedOn";
		DataTable dataTable = new();

		using SqlConnection connection = new(connectionString);
		try
		{
			connection.Open();
			using(SqlCommand command = new(query, connection))
			{
				using SqlDataReader reader = command.ExecuteReader();
				dataTable.Load(reader);
			}
			return dataTable;
		}
		catch (Exception)
		{
			throw;
		}
		finally
		{
			connection.Close();
		}
	}
}
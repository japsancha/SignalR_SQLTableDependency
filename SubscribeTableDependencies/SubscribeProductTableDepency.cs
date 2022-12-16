using SignalR_SQLTableDependency.Hubs;
using SignalR_SQLTableDependency.Models;
using TableDependency.SqlClient;

namespace SignalR_SQLTableDependency.SubscribeTableDependencies;

public class SubscribeProductTableDependency
{
	SqlTableDependency<Product> tableDependency;
	readonly DashboardHub dashboardHub;

	public SubscribeProductTableDependency(DashboardHub dashboardHub)
	{
		this.dashboardHub = dashboardHub;
	}
	public void SubscribeTableDependency()
	{
		const string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=SQLTableDependency;Integrated Security=True;Connect Timeout=30";
		tableDependency = new SqlTableDependency<Product>(connectionString);
		tableDependency.OnChanged += TableDependency_OnChanged;
		tableDependency.OnError += TableDependency_OnError;
		tableDependency.Start();
	}
	private void TableDependency_OnChanged(object sender, TableDependency.SqlClient.Base.EventArgs.RecordChangedEventArgs<Product> e)
	{
		if (e.ChangeType != TableDependency.SqlClient.Base.Enums.ChangeType.None)
		{
			dashboardHub.SendProducts();
		}
	}

	private void TableDependency_OnError(object sender, TableDependency.SqlClient.Base.EventArgs.ErrorEventArgs e)
	{
		Console.WriteLine($"{nameof(Product)} SqlTableDependency error {e.Error.Message}");
	}
}
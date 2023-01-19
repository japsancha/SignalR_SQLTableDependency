using SignalR_SQLTableDependency.Hubs;
using SignalR_SQLTableDependency.Models;
using TableDependency.SqlClient;

namespace SignalR_SQLTableDependency.SubscribeTableDependencies;

public class SubscribeProductTableDependency: ISubscribeTableDependency
{
	SqlTableDependency<Product> tableDependency = null!;
	readonly DashboardHub dashboardHub;

	public SubscribeProductTableDependency(DashboardHub dashboardHub)
	{
		this.dashboardHub = dashboardHub;
	}

	// instead of having connection in multiple places, we can pass it from called method
	// i.e. from middleware

	// instead of calling this method in Program.cs, we call it in a middleware
	public void SubscribeTableDependency(string connectionString)
	{
		tableDependency = new SqlTableDependency<Product>(connectionString);
		tableDependency.OnChanged += TableDependency_OnChanged;
		tableDependency.OnError += TableDependency_OnError;
		tableDependency.Start();
	}
	private void TableDependency_OnChanged(object sender, TableDependency.SqlClient.Base.EventArgs.RecordChangedEventArgs<Product> e)
	{
		if (e.ChangeType != TableDependency.SqlClient.Base.Enums.ChangeType.None)
		{
			_ = dashboardHub.SendProducts();
		}
	}

	private void TableDependency_OnError(object sender, TableDependency.SqlClient.Base.EventArgs.ErrorEventArgs e)
	{
		Console.WriteLine($"{nameof(Product)} SqlTableDependency error {e.Error.Message}");
	}
}
using SignalR_SQLTableDependency.Hubs;
using SignalR_SQLTableDependency.Models;
using TableDependency.SqlClient;
using TableDependency.SqlClient.Base.EventArgs;

namespace SignalR_SQLTableDependency.SubscribeTableDependencies;

public class SubscribeSaleTableDependency : ISubscribeTableDependency
{
	SqlTableDependency<Sale> tableDependency;
	readonly DashboardHub dashboardHub;

	public SubscribeSaleTableDependency(DashboardHub dashboardHub)
	{
		this.dashboardHub = dashboardHub;
	}

	// instead of having connection in multiple places, we can pass it from called method
	// i.e. from middleware

	// instead of calling this method in Program.cs, we call it in a middleware
	public void SubscribeTableDependency(string connectionString)
	{
		tableDependency = new SqlTableDependency<Sale>(connectionString, "Sale");

		tableDependency.OnChanged += TableDependency_OnChanged;
		tableDependency.OnError += TableDependency_OnError;
		tableDependency.Start();
	}

	private void TableDependency_OnError(object sender, TableDependency.SqlClient.Base.EventArgs.ErrorEventArgs e)
	{
		Console.WriteLine($"{nameof(Sale)} SqlTableDependency error: {e.Error.Message}");
	}

	private void TableDependency_OnChanged(object sender, RecordChangedEventArgs<Sale> e)
	{
		if (e.ChangeType != TableDependency.SqlClient.Base.Enums.ChangeType.None)
		{
			dashboardHub.SendSales();
		}
	}
}
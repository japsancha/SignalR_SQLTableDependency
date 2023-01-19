using SignalR_SQLTableDependency.Hubs;
using SignalR_SQLTableDependency.Models;
using TableDependency.SqlClient;
using TableDependency.SqlClient.Base.EventArgs;

namespace SignalR_SQLTableDependency.SubscribeTableDependencies;

public class SubscribeCustomerTableDependency : ISubscribeTableDependency
{
	SqlTableDependency<Customer> tableDependency = null!;
	readonly DashboardHub dashboardHub;

	public SubscribeCustomerTableDependency(DashboardHub dashboardHub)
	{
		this.dashboardHub = dashboardHub;
	}

	public void SubscribeTableDependency(string connectionString)
	{
		tableDependency = new SqlTableDependency<Customer>(connectionString);
		tableDependency.OnChanged += TableDependency_OnChanged;
		tableDependency.OnError += TableDependency_OnError;
		tableDependency.Start();
	}

	private void TableDependency_OnChanged(object sender, RecordChangedEventArgs<Customer> e)
	{
		if (e.ChangeType != TableDependency.SqlClient.Base.Enums.ChangeType.None)
		{
			_ = dashboardHub.SendCustomers();
		}
	}

	private void TableDependency_OnError(object sender, TableDependency.SqlClient.Base.EventArgs.ErrorEventArgs e)
	{
		Console.WriteLine($"{nameof(Customer)} SqlTableDependency error {e.Error.Message}");
	}
}
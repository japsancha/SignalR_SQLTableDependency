using Microsoft.AspNetCore.SignalR;

namespace SignalR_SQLTableDependency.Hubs
{
		public class DashboardHub : Hub
		{
				public void Hello()
				{
						//Clients.All.hello();
				}
		}
}
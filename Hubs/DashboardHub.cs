using Microsoft.AspNetCore.SignalR;
using SignalR_SQLTableDependency.Repositories;

namespace SignalR_SQLTableDependency.Hubs;

public class DashboardHub : Hub
{
	ProductRepository productRepository;

	public DashboardHub(IConfiguration configuration)
	{
		var connectionString = configuration.GetConnectionString("DefaultConnection");
		productRepository = new ProductRepository(connectionString);
	}
	public async Task SendProducts()
	{
		var products = productRepository.GetProducts();
		await Clients.All.SendAsync("ReceivedProducts", products);
	}
}
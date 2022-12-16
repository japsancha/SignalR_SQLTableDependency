using SignalR_SQLTableDependency.SubscribeTableDependencies;

namespace SignalR_SQLTableDependency.MiddlewareExtensions;

public static class ApplicationBuilderExtension
{
	public static void UseProductTableDependency(this IApplicationBuilder applicationBuilder, string connectionString)
	{
		var serviceProvider = applicationBuilder.ApplicationServices;
		var service = serviceProvider.GetService<SubscribeProductTableDependency>();
		service.SubscribeTableDependency(connectionString);
	}

	public static void UseSaleTableDependency(this IApplicationBuilder applicationBuilder, string connectionString)
	{
		var serviceProvider = applicationBuilder.ApplicationServices;
		var service = serviceProvider.GetService<SubscribeSaleTableDependency>();
		service.SubscribeTableDependency(connectionString);
	}
}

// here as well, we can pass the connectionString from Program.cs file (caller method)
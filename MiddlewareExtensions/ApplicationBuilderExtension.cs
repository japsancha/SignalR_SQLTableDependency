using SignalR_SQLTableDependency.SubscribeTableDependencies;

namespace SignalR_SQLTableDependency.MiddlewareExtensions;

public static class ApplicationBuilderExtension
{
	public static void UseProductTableDependency(this IApplicationBuilder applicationBuilder)
	{
		var serviceProvider = applicationBuilder.ApplicationServices;
		var service = serviceProvider.GetService<SubscribeProductTableDependency>();
		service.SubscribeTableDependency();
	}
}
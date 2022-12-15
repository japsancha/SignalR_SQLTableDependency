using Microsoft.AspNetCore.Mvc;

namespace SignalR_SQLTableDependency.Controllers;

// [Route("[controller]")]
public class DashboardController : Controller
{
	// private readonly ILogger<DashboardController> _logger;

	// public DashboardController(ILogger<DashboardController> logger)
	// {
	//     _logger = logger;
	// }

	public IActionResult Index()
	{
		return View();
	}

	// [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
	// public IActionResult Error()
	// {
	//     return View("Error!");
	// }
}
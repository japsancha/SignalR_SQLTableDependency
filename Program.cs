using SignalR_SQLTableDependency.Hubs;
using SignalR_SQLTableDependency.MiddlewareExtensions;
using SignalR_SQLTableDependency.SubscribeTableDependencies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSignalR();

// Add Class through DI
builder.Services.AddSingleton<DashboardHub>();
builder.Services.AddSingleton<SubscribeProductTableDependency>();
builder.Services.AddSingleton<SubscribeSaleTableDependency>();

var app = builder.Build();
var connectionString = app.Configuration.GetConnectionString("DefaultConnection");

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.MapHub<DashboardHub>("/dashboardHub");

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Dashboard}/{action=Index}/{id?}");

/*
 * We must call SubscribeTableDependency() method after app.MapHub<DashboardHub>("/dashboardHub");
 * because we need to pass the HubContext to the SubscribeTableDependency() method.
 * We create one middleware and call SubscribeTableDependency() method in it.
 */

app.UseProductTableDependency(connectionString);
app.UseSaleTableDependency(connectionString);

app.Run();
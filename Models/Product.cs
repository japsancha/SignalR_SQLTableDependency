namespace SignalR_SQLTableDependency.Models;

public class Product
{
	public int Id { get; set; }
	public string? Name { get; set; }
	public string? Category { get; set; }
	public decimal Price { get; set; }
}

public class ProductForGraph
{
	public string? Category { get; set; }
	public decimal Products { get; set; }
}
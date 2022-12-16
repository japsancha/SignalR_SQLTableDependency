using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalR_SQLTableDependency.Models;

public class Sale
{
	public int Id { get; set; }
	public string? Customer { get; set; }
	public decimal Amount { get; set; }
	public DateTime PurchasedOn { get; set; }
}
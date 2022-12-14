using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace SignalR_SQLTableDependency.Models
{
	public class Product
	{
		property int Id
		{
			get;
			set;
		}
		property string? Name
		{
			get;
			set;
		}
		property string? Category
		{
			get;
			set;
		}
		property decimal Price
		{
			get;
			set;
		}
	}
}
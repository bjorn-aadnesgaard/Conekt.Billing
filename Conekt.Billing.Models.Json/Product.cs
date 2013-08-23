using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Conekt.Billing.Models.Json
{
	public class Product
	{
		public int ID { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public int Quantity { get; set; }
		public decimal UnitPrice { get; set; }
		public decimal TaxRate { get; set; }
		public string ItemCode { get; set; } //Quickbooks item reference

		public decimal TotalTax
		{
			get
			{
				decimal amount = TaxRate > 0 ? Quantity * (UnitPrice * (TaxRate / 100)) : 0;
				return Math.Round(amount, 2);
			}
		}

		public decimal TotalAmount
		{
			get
			{
				decimal amount = TaxRate > 0 ? Quantity * (UnitPrice * ((TaxRate / 100) + 1)) : Quantity * UnitPrice;
				return Math.Round(amount, 2);
			}
		}
	}
}

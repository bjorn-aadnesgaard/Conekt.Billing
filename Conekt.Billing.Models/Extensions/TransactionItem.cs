using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Conekt.Billing.Models
{
	public partial class TransactionItem
	{
		public decimal Total
		{
			get
			{
				return TaxRate > 0 ? Quantity * (UnitPrice * TaxRate) : Quantity * UnitPrice;
			}
		}
	}
}
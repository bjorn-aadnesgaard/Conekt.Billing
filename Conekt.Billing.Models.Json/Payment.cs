using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Conekt.Billing.Models.Json
{
	public class Payment
	{
		public decimal Amount { get; set; }
		public Card Card { get; set; }
		public string ChargeRequestID { get; set; }
		public string Currency { get; set; }
		public DateTime DateCreated { get; set; }
		public string Description { get; set; }
		public string Merchant { get; set; }
		public Merchant.Stripe Stripe { get; set; }
	}
}

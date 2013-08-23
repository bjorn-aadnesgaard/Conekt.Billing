using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Conekt.Billing.Models
{
	public class PaymentCharge
	{
		public string ChargeRequestID { get; set; }
		public DateTime DateCreated { get; set; }
		public decimal Amount { get; set; }
	}
}
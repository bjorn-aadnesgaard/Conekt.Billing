using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Conekt.Billing.Models.Json
{
	public partial class Merchant
	{
		public class Stripe
		{
			public string PublishableKey { get; set; }
			public string TokenID { get; set; }
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Conekt.Billing.Library
{
	public class Enums
	{
		public enum Merchant
		{
			Stripe,
			iTransact
		}

		public enum SessionKeys
		{
			InvoiceResponse,
			PaymentResponse,
			TransactionList
		}

		public enum Status
		{
			Success,
			Info,
			Warning,
			Error
		}
	}
}

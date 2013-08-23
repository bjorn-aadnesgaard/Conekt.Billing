using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Conekt.Billing.Models;

namespace Conekt.Billing.Quickbooks
{
	public partial class XmlProcess
	{
		/// <summary>
		/// Returns the xml requests used to sync with Quickbooks
		/// </summary>
		/// <returns></returns>
		public static ArrayList BuildXmlRequests(Account account)
		{
			ArrayList al = new ArrayList();

			//Get invoices to be created in Quickbooks
			var invoices = Invoice.Add(account);
			if (!string.IsNullOrEmpty(invoices))
				al.Add(invoices);

			//Get payments to be created in Quickbooks
			var payments = Payment.Add(account);
			if (!string.IsNullOrEmpty(payments))
				al.Add(payments);

			return al;
		}
	}
}
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Conekt.Billing.Library
{
	/// <summary>
	/// See http://foxsys.blogspot.ca/2007/01/session-facade-aspnet-c.html for reference
	/// </summary>
	public static class SessionFacade
	{
		/// <summary>
		/// Gets or sets the invoice response.
		/// </summary>
		/// <value>
		/// The invoice response.
		/// </value>
		public static string InvoiceResponse
		{
			get
			{
				return (string)HttpContext.Current.Session[Enums.SessionKeys.InvoiceResponse.ToString()];
			}
			set
			{
				HttpContext.Current.Session[Enums.SessionKeys.InvoiceResponse.ToString()] = value;
			}
		}

		/// <summary>
		/// Gets or sets the payment response.
		/// </summary>
		/// <value>
		/// The payment response.
		/// </value>
		public static string PaymentResponse
		{
			get
			{
				return (string)HttpContext.Current.Session[Enums.SessionKeys.PaymentResponse.ToString()];
			}
			set
			{
				HttpContext.Current.Session[Enums.SessionKeys.PaymentResponse.ToString()] = value;
			}
		}

		/// <summary>
		/// Gets or sets the transaction list.
		/// </summary>
		/// <value>
		/// The transaction list.
		/// </value>
		public static ArrayList TransactionList
		{
			get
			{
				return (ArrayList)HttpContext.Current.Session[Enums.SessionKeys.TransactionList.ToString()];
			}
			set
			{
				HttpContext.Current.Session[Enums.SessionKeys.TransactionList.ToString()] = value;
			}
		}

		/// <summary>
		/// Clears all session data.
		/// </summary>
		public static void ClearAll()
		{
			InvoiceResponse = null;
			PaymentResponse = null;
			TransactionList = null;
		}
	}
}

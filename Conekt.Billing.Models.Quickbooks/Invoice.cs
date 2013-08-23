using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Conekt.Billing.Models;

namespace Conekt.Billing.Quickbooks
{
	public partial class Invoice
	{
		/// <summary>
		/// Adds the invoice wrapper xml markup structure.
		/// </summary>
		/// <param name="trans">The trans.</param>
		/// <returns></returns>
		private static string AddWrapper(Transaction trans)
		{
			//Invoice item XML markup
			string xml =
				"<InvoiceAddRq>" +
					"<InvoiceAdd>" +
						"<CustomerRef>" +
							"<FullName>" + trans.QuickbooksCustomer + "</FullName>" +
						"</CustomerRef>" +
						"<TxnDate>" + trans.DateCreated.ToString("yyyy-MM-dd") + "</TxnDate>" +
						"<Memo>" + trans.ChargeRequestID + "</Memo>" + //Reference field to map to the online transaction
						"{0}" +
					"</InvoiceAdd>" +
				"</InvoiceAddRq>";

			return xml;
		}

		/// <summary>
		/// Adds the invoice line items xml markup structure.
		/// </summary>
		/// <param name="trans">The trans.</param>
		/// <returns></returns>
		private static string AddItems(Transaction trans)
		{
			//Line item XML markup
			string xml =
				"<InvoiceLineAdd>" +
					"<ItemRef>" +
						"<FullName>" + "{0}" + "</FullName>" +
					"</ItemRef>" +
					"<Desc>" + "{1}" + "</Desc>" +
					"<Quantity>" + "{2}" + "</Quantity>" +
					"<Rate>" + "{3}" + "</Rate>" +
					"<Amount>" + "{4}" + "</Amount>" +
					"<SalesTaxCodeRef>" +
						"<FullName>" + "{5}" + "</FullName>" +
					"</SalesTaxCodeRef>" +
				"</InvoiceLineAdd>";
				
			string items = "";
			foreach(var item in trans.TransactionItem)
			{
				items += string.Format(xml, item.Code, item.Name, item.Quantity, item.UnitPrice, item.Quantity * item.UnitPrice, "H");
			}

			return items;
		}

		/// <summary>
		/// Creates the invoice xml structure.
		/// </summary>
		/// <notes>
		///	The InvoiceLineAdd.ItemRef.FullName field item must be associated with an account in order to successfully process.
		/// </notes>
		/// <returns></returns>
		public static string Add(Account account)
		{
			//Get a list of transactions
			ConektBillingEntities db = new ConektBillingEntities();
			IQueryable<Transaction> tx = db.Transaction.Where(t => t.AccountID == account.ID && t.TransactionImport.Count < 1); //Select transactions which have not been imported.

			//Return nothing if there are no transactions
			if(tx.Count() < 1)
				return null;

			//Create the xml string for each transaction
			string xml = "";
			foreach(var trans in tx)
			{
				//Create the parent wrapper for the invoice request
				xml += string.Format(AddWrapper(trans), AddItems(trans));
			}

			//Return the xml string
			return XmlStrings.CreateXml(xml);
		}
	}
}

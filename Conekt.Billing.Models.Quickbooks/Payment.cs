using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml;
using System.Xml.Linq;
using Conekt.Billing.Library;
using Conekt.Billing.Models;

namespace Conekt.Billing.Quickbooks
{
	public partial class Payment
	{
		/// <summary>
		/// Adds the wrapper xml markup structure.
		/// </summary>
		/// <param name="trans">The trans.</param>
		/// <returns></returns>
		private static string AddWrapper(Transaction trans)
		{
			//Invoice item XML markup
			string xml =
				"<ReceivePaymentAddRq>" +
					"<ReceivePaymentAdd>" +
						"<CustomerRef>" +
							"<FullName>" + trans.QuickbooksCustomer + "</FullName>" +
						"</CustomerRef>" +
						"<TxnDate>" + trans.DateCreated.ToString("yyyy-MM-dd") + "</TxnDate>" +
						"<RefNumber>" + trans.RefNumber + "</RefNumber>" +
						"<TotalAmount>" + trans.Total + "</TotalAmount>" +
						"<PaymentMethodRef>" +
							"<FullName>" + trans.PaymentMethod + "</FullName>" +
						"</PaymentMethodRef>" +
						"<AppliedToTxnAdd>" +
							"<TxnID>" + "{0}" + "</TxnID>" + //Required
							"<PaymentAmount>" + trans.Total + "</PaymentAmount>" +
						"</AppliedToTxnAdd>" +
					"</ReceivePaymentAdd>" +
				"</ReceivePaymentAddRq>";

			return xml;
		}

		/// <summary>
		/// Adds the xml markup structure.
		/// </summary>
		/// <notes>
		/// </notes>
		/// <returns></returns>
		public static string Add(Account account)
		{
			//Record the import procedure
			if (SessionFacade.PaymentResponse != null)
				Import.Record(SessionFacade.TransactionList);

			//Return null if InvoiceResponse session is null. Do not add payment request without the TxnID.
			if (SessionFacade.InvoiceResponse == null)
				return null;

			string response = "";
			string txnid = "";
			IEnumerable<XElement> items = null;

			//Get a list of transactions
			ConektBillingEntities db = new ConektBillingEntities();
			IQueryable<Transaction> tx = db.Transaction.Where(t => t.AccountID == account.ID && t.TransactionImport.Count < 1); //Select transactions which have not been imported.

			//Set the Quickbooks invoice response session
			response = SessionFacade.InvoiceResponse;

			if(!string.IsNullOrEmpty(response))
			{
				//Get the list of TxnID's from the Quickbooks response
				XElement responseXml = XElement.Parse(response);
				items = responseXml.Descendants("InvoiceRet").Distinct();
			}

			//Create the xml string for each transaction
			string xml = "";
			ArrayList listTrans = new ArrayList(); //Store TransactionID in array to store import data.
			foreach(var trans in tx)
			{
				var item = items.Where(i => i.Element("Memo").Value == trans.ChargeRequestID);

				//If we have a response from Quickbooks, set the TxnID
				if(items != null)
					txnid = item.Select(i => i.Element("TxnID").Value).Single();

				//Add the RefNumber to the Transaction object
				trans.RefNumber = item.Select(i => i.Element("RefNumber").Value).Single();

				//Create the parent wrapper for the invoice request
				xml += string.Format(AddWrapper(trans), txnid);

				//Add the TransactionID to the array
				listTrans.Add(trans.ID);
			}

			//Create transaction list session
			SessionFacade.TransactionList = listTrans;

			//Return the xml string
			return XmlStrings.CreateXml(xml);
		}
	}
}
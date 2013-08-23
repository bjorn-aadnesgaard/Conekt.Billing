using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ActionMailer.Net.Mvc;
using Conekt.Billing.Models;
using Conekt.Billing.Models.Json;

namespace Conekt.Billing.Controllers
{
	public class MailController : MailerBase
	{
		private const string testEmail = "bjorn@conekted.com";
		private const string from = "{0} <{1}>";

		public EmailResult TransactionComplete(JsonTransaction jtrans, PaymentCharge pCharge)
		{
			//Update the jstrans object with the ChargeRequestID and Date.
			jtrans.Payment.ChargeRequestID = pCharge.ChargeRequestID;
			jtrans.Payment.DateCreated = pCharge.DateCreated;

			string subject = "Payment Confirmation {0}";
			bool isLocalRequest = System.Web.HttpContext.Current.Request.IsLocal;

			From = string.Format(from, jtrans.Settings.Account.Name, jtrans.Settings.Account.Email);
			To.Add(isLocalRequest ? testEmail : jtrans.User.Email);
			Subject = string.Format(subject, pCharge.ChargeRequestID, jtrans.Settings.Account.Name);

			return Email("Transaction-Complete", jtrans);
		}
	}
}

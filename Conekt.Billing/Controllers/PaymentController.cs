using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Conekt.Billing;
using Conekt.Billing.Library;
using Conekt.Billing.Models;
using Conekt.Billing.Models.Json;

namespace Conekt.Billing.Controllers
{
    public class PaymentController : Controller
    {
		[HttpPost]
		public ActionResult Setup(JsonTransaction jtrans)
        {
			return PartialView("Form", jtrans);
        }

		[HttpPost]
		public ActionResult Process(JsonTransaction jtrans)
		{
			PaymentCharge pCharge = new PaymentCharge();

			switch ((Enums.Merchant)Enum.Parse(typeof(Enums.Merchant), jtrans.Payment.Merchant))
			{
				case Enums.Merchant.Stripe:
					pCharge = MerchantPayment.Stripe.Process(jtrans);
					break;
			}

			//Store transaction
			Transaction.Record(jtrans, pCharge);

			//Send invoice email
			if (!jtrans.Settings.DisableEmailInvoice) new MailController().TransactionComplete(jtrans, pCharge).DeliverAsync();

			//Return result view
			return PartialView("Complete", jtrans);
		}
    }
}

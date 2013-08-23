using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Stripe;
using Conekt.Billing.Models.Json;

namespace Conekt.Billing.Models
{
	public partial class MerchantPayment
	{
		public class Stripe
		{
			public static PaymentCharge Process(JsonTransaction jtrans)
			{
				//Process the charge request
				StripeConfiguration.SetApiKey(Account.GetMerchantApiKey(jtrans.AccountApiKey));
				var charge = new StripeChargeCreateOptions
				{
					AmountInCents = (int)(jtrans.Payment.Amount * 100), //Convert dollar amount to cents
					Currency = jtrans.Payment.Currency,
					Description = jtrans.Payment.Description,
					TokenId = jtrans.Payment.Stripe.TokenID
				};

				//Set the charge request values
				StripeCharge sRequest = new StripeChargeService().Create(charge);

				//Create a PaymentCharge object
				PaymentCharge pCharge = new PaymentCharge()
				{
					ChargeRequestID = sRequest.Id,
					DateCreated = sRequest.Created
				};

				//Create a new transaction database object
				//trans.ChargeRequestID = chargeRequest.Id;
				//trans.DateCreated = chargeRequest.Created;
				//trans.Payment.ChargeRequestID = chargeRequest.Id;
				//trans.Payment.TransactionDate = chargeRequest.Created;

				//This is set client side for improved PCI compliance. Credit card information never reaches the server!
				//Set the token request values
				//var chargeToken = new StripeTokenService().Get(transaction.Payment.Stripe.TokenID);
				//transaction.Payment.Card = new Card(); //Initialise the card
				//transaction.Payment.Card.Type = chargeToken.StripeCard.Type;
				//transaction.Payment.Card.Last4 = chargeToken.StripeCard.Last4;

				return pCharge;
			}
		}
	}
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Conekt.Billing.Models.Json;

namespace Conekt.Billing.Models
{
	public partial class Transaction
	{
		public string RefNumber { get; set; }

		/// <summary>
		/// Records the transaction information in the database.
		/// </summary>
		/// <param name="jtrans">The jtrans.</param>
		/// <param name="pCharge">The pcharge.</param>
		public static void Record(JsonTransaction jtrans, PaymentCharge pCharge)
		{
			ConektBillingEntities db = new ConektBillingEntities();
			
			//Create new Transaction object for database record
			Transaction trans = new Transaction()
			{
				ID = Guid.NewGuid(),
				AccountID = Account.GetAccountID(jtrans.AccountApiKey),
				ChargeRequestID = pCharge.ChargeRequestID,
				Currency = jtrans.Payment.Currency,
				DateCreated = pCharge.DateCreated,
				Merchant = jtrans.Payment.Merchant,
				PaymentMethod = jtrans.Payment.Card.Type,
				Total = jtrans.Payment.Amount,
				UserName = jtrans.User.UserName,
				QuickbooksCustomer = jtrans.Account,
				PaymentLast4 = Convert.ToInt32(jtrans.Payment.Card.Last4)
			};

			//TransactionItem
			foreach (var product in jtrans.Products)
			{
				TransactionItem item = new TransactionItem()
				{
					ID = Guid.NewGuid(),
					TransactionID = trans.ID,
					Code = product.ItemCode,
					Name = product.Name,
					Quantity = product.Quantity,
					TaxRate = (product.TaxRate / 100), //Convert to decimal
					UnitPrice = product.UnitPrice
				};

				//Add object to transaction
				trans.TransactionItem.Add(item);
			}

			//TransactionUser
			TransactionUser user = new TransactionUser()
			{
				ID = Guid.NewGuid(),
				TransactionID = trans.ID,
				Email = jtrans.User.Email,
				FirstName = jtrans.User.FirstName,
				LastName = jtrans.User.LastName,
				Phone = jtrans.User.Phone,
				PhoneExt = jtrans.User.PhoneExt,
				UserName = jtrans.User.UserName
			};

			//Add object to transaction
			trans.TransactionUser.Add(user);
			
			//Add transaction object and save to database
			db.Transaction.AddObject(trans);
			db.SaveChanges();
		}

		/// <summary>
		/// Gets the payment amount.
		/// </summary>
		/// <param name="jtrans">The jtrans.</param>
		/// <returns></returns>
		public static decimal GetPaymentAmount(JsonTransaction jtrans)
		{
			//Get the total amount from the products
			decimal amount = 0;
			foreach(Product product in jtrans.Products)
			{
				amount += product.TotalAmount;
			}

			return amount;
		}
	}
}
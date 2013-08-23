using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Conekt.Billing.Models
{
	public partial class Account
	{
		//TODO: Move to extensions sub folder?

		/// <summary>
		/// Returns the account ID from Web.config or database. The Web.config value will be returned if set.
		/// </summary>
		/// <param name="ApiKey">The API key.</param>
		/// <returns></returns>
		public static Guid GetAccountID(string AccountApiKey)
		{
			Guid accountID;

			//Get the Web.config Account.ID value
			if (!string.IsNullOrEmpty(Conekt.Billing.Configuration.ConfigurationManager.GetConfiguration().Account.ID))
				accountID = Guid.Parse(Conekt.Billing.Configuration.ConfigurationManager.GetConfiguration().Account.ID);
			
			//If null, get the account information with the ApiKey from the json object.
			else
				accountID = Account.Get(AccountApiKey).ID;

			return accountID;
		}

		/// <summary>
		/// Returns the merchant API key from Web.config or database. The Web.config value will be returned if set.
		/// </summary>
		/// <param name="ApiKey">The account API key.</param>
		/// <returns></returns>
		public static string GetMerchantApiKey(string ApiKey)
		{
			string key;

			//Get the Web.config Merchant.ApiKey value
			if(!string.IsNullOrEmpty(Conekt.Billing.Configuration.ConfigurationManager.GetConfiguration().Merchant.ApiKey))
				key = Conekt.Billing.Configuration.ConfigurationManager.GetConfiguration().Merchant.ApiKey;
			
			//If null, use the user is specifying the key via the json object, return value from database
			else
			{
				ConektBillingEntities db = new ConektBillingEntities();
				Guid guidApiKey = Guid.Parse(ApiKey);
				key = (from Account in db.Account
					   from AccountKey in db.AccountKey
					   where Account.ID == AccountKey.AccountID && AccountKey.ApiKey == guidApiKey
					   select Account).Single().MerchantApiKey;
			}

			return key;
		}

		/// <summary>
		/// Returns an account object from the database
		/// </summary>
		/// <param name="ApiKey">The account API Key</param>
		/// <returns></returns>
		public static Account Get(string ApiKey)
		{
			//Get the Web.config Account.ApiKey value, if null, use the user is specifying the key via the json object, return value from database
			if (!string.IsNullOrEmpty(Conekt.Billing.Configuration.ConfigurationManager.GetConfiguration().Account.ApiKey))
				ApiKey = Conekt.Billing.Configuration.ConfigurationManager.GetConfiguration().Account.ApiKey;

			ConektBillingEntities db = new ConektBillingEntities();
			Guid guidApiKey = Guid.Parse(ApiKey);
			Account account = (from Account in db.Account
							   from AccountKey in db.AccountKey
							   where Account.ID == AccountKey.AccountID && AccountKey.ApiKey == guidApiKey && AccountKey.IsActive == true
							   select Account).Single();

			return account;
		}
	}
}
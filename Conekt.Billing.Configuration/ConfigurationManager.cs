using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Conekt.Billing.Configuration
{
	public class ConfigurationManager : ConfigurationSection
	{
		public static ConfigurationManager GetConfiguration()
		{
			ConfigurationManager configuration = (ConfigurationManager)System.Configuration.ConfigurationManager.GetSection("PaymentModule");

			if (configuration != null)
				return configuration;

			return new ConfigurationManager();
		}

		[ConfigurationProperty("Account", IsRequired = false)]
		public Account Account
		{
			get { return (Account)this["Account"]; }
		}

		[ConfigurationProperty("Import", IsRequired = false)]
		public Import Import
		{
			get { return (Import)this["Import"]; }
		}

		[ConfigurationProperty("Merchant", IsRequired = false)]
		public Merchant Merchant
		{
			get { return (Merchant)this["Merchant"]; }
		}

		[ConfigurationProperty("Settings", IsRequired = false)]
		public Settings Settings
		{
			get { return (Settings)this["Settings"]; }
		}
	}
}
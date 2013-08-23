using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Conekt.Billing.Configuration
{
	public class Account : ConfigurationElement
	{
		[ConfigurationProperty("ApiKey", IsRequired = false)]
		public string ApiKey
		{
			get { return (string)this["ApiKey"]; }
		}

		[ConfigurationProperty("ID", IsRequired = false)]
		public string ID
		{
			get { return (string)this["ID"]; }
		}
	}
}
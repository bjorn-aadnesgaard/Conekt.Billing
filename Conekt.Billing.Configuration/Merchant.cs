using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Conekt.Billing.Configuration
{
	public class Merchant : ConfigurationElement
	{
		[ConfigurationProperty("ApiKey", IsRequired = false)]
		public string ApiKey
		{
			get { return (string)this["ApiKey"]; }
		}
	}
}
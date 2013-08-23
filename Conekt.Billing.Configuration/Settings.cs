using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Conekt.Billing.Configuration
{
	public class Settings : ConfigurationElement
	{
		[ConfigurationProperty("Mode", IsRequired = false)]
		public string Mode
		{
			get { return (string)this["Mode"]; }
		}
	}
}
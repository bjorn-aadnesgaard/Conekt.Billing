using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Conekt.Billing.Configuration
{
	public class Import : ConfigurationElement
	{
		[ConfigurationProperty("Disabled", IsRequired = false)]
		public bool Disabled
		{
			get { return (bool)this["Disabled"]; }
		}
	}
}
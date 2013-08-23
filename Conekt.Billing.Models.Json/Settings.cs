using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Conekt.Billing.Models.Json
{
	public class Settings
	{
		public bool DisableEmailInvoice { get; set; }
		public bool DisableRemoteScripts { get; set; }
		public bool DisableRemoteStyles { get; set; }
		public bool DisableSummary { get; set; }
		public Account Account { get; set; }
	}
}

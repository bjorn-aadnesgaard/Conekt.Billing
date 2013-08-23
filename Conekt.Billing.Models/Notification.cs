using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Conekt.Billing.Library;

namespace Conekt.Billing.Models
{
	public class Notification
	{
		public string Message { get; set; }
		public Enums.Status Status { get; set; }
		public bool DisableClose { get; set; }
		public bool DisableAnimation { get; set; }
	}
}
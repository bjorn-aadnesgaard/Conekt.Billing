using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Conekt.Billing.Models.Json
{
	public class JsonTransaction
	{
		public string Account { get; set; } //This is a Quickbooks customer name reference.
		public string AccountApiKey { get; set; }
		public Payment Payment { get; set; }
		public IList<Product> Products { get; set; }
		public Settings Settings { get; set; }
		public User User { get; set; }

		/// <summary>
		/// Returns the script required to post the transaction.
		/// </summary>
		/// <param name="arTrans">The ar trans.</param>
		public static string CreateScript(JsonTransaction jtrans)
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("<script type=\"text/javascript\">");
				sb.Append("$(document).ready(function () { ");
					sb.Append("var transaction = ");
						sb.Append(System.Web.Helpers.Json.Encode(jtrans)); //Encode the object as json
					sb.Append(";");
					sb.Append("payments.setup(transaction);"); //Setup the payment
				sb.Append(" });");
			sb.Append("</script>");

			return sb.ToString();
		}
	}
}

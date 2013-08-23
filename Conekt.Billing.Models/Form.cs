using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace Conekt.Billing.Models.Form
{
	public class Item
	{
		public string Label { get; set; }
		public string Placeholder { get { return Label; } }
		public string ID { get { return SyntaxCleanup(Label.ToLower()); } }
		public string CssClass { get { return SyntaxCleanup(Label.ToLower()); } }
		public string HelpText { get; set; }
		public int InputLimit { get; set; }
		public string DefaultValue { get; set; }

		private string SyntaxCleanup(string str)
		{
			return Regex.Replace(str, @"[!@$%./]", "").Replace("--", "-").Replace("#", "number").Replace(" ", "-");
		}
	}
}
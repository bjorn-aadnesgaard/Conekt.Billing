using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Xml;

namespace Conekt.Billing.Library
{
	public class Utility
	{
		/// <summary>
		/// Returns a generic list of countries from ~/App_Data/Countries.xml
		/// </summary>
		/// <ref>
		///	http://madskristensen.net/post/XML-country-list.aspx
		/// </ref>
		/// <returns></returns>
		public static List<string> GetCountryList()
		{
			List<string> countries = new List<string>();

			//Create the first entries manually.
			//string[] firstEntries = { "Canada", "United States" };
			//foreach (string entry in firstEntries)
			//    countryList.Add(entry);

			XmlDocument doc = new XmlDocument();
			doc.Load(HttpContext.Current.Server.MapPath("~/App_Data/Countries.xml"));

			foreach (XmlNode node in doc.SelectNodes("//country"))
			{
				countries.Add(node.InnerText);
			}

			return countries;
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Conekt.Billing.Quickbooks
{
	public partial class XmlStrings
	{
		/// <summary>
		/// Returns the request wrapper xml markup structure.
		/// </summary>
		/// <returns></returns>
		private static string XmlWrapper(bool includeResponseData = true)
		{
			string wrapper =
				"<?xml version=\"1.0\"?>" +
				"<?qbxml version=\"12.0\"?>" +
				"<QBXML>" +
					"<QBXMLMsgsRq onError=\"stopOnError\" responseData=\"" + (includeResponseData ? "includeAll" : "includeNone") + "\">" +
						"{0}" +
					"</QBXMLMsgsRq>" +
				"</QBXML>";

			return wrapper;
		}

		/// <summary>
		/// Creates the request xml markup.
		/// </summary>
		/// <param name="xml">The XML.</param>
		/// <returns></returns>
		public static string CreateXml(string xml, bool includeResponseData = true)
		{
			return string.Format(XmlWrapper(includeResponseData), xml);
		}
	}
}
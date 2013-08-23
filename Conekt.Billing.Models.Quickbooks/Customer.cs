using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Conekt.Billing.Quickbooks
{
	public class Customer
	{
		public static string GetCustomer()
		{
			string xml =
				"<CustomerQueryRq requestID=\"1\">" +
					"<MaxReturned>1</MaxReturned>" +
				"</CustomerQueryRq>";

			return XmlStrings.CreateXml(xml);
		}

		public static string AddCustomer()
		{
			//TODO:
			//Create customer object
			//Add CompanyName
			string xml =
				"<CustomerAddRq>" +
					"<CustomerAdd>" +
						"<Name>" + "On-Line Profile Customers" + "</Name>" +
						"<FirstName>" + "Bjorn" + "</FirstName>" +
						"<LastName>" + "Aadnesgaard" + "</LastName>" +
						"<BillAddress>" +
							"<Addr1>" + "320-1300 Marlborough Court" + "</Addr1>" +
							"<Addr2>" + "" + "</Addr2>" +
							"<City>" + "Oakville" + "</City>" +
							"<State>" + "Ontario" + "</State>" +
							"<PostalCode>" + "L6H2S2" + "</PostalCode>" +
						"</BillAddress>" +
						"<Phone>" + "647.247.2440" + "</Phone>" +
						"<Email>" + "bjorn@conekted.com" + "</Email>" +
					"</CustomerAdd>" +
				"</CustomerAddRq>";

			return XmlStrings.CreateXml(xml);
		}
	}
}

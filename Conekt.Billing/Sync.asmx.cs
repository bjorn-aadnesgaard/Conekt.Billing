using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Services;
using System.Xml;
using System.Xml.Linq;
using Conekt.Billing.Models;
using Conekt.Billing.Quickbooks;
using Conekt.Billing.Library;

namespace Conekt.Billing.WebServices
{
	[WebService(Namespace = "http://developer.intuit.com/", Name = "Sync", Description = "Sync online payment data with Quickbooks file")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [System.Web.Script.Services.ScriptService]
    public class Sync : WebService
    {
		//Strings
		public static string MessageError = "Error {0} Contact support for assistance.";
		public static string MessageInvalidSession = "Invalid session.";
		public static string MessageNoTrans = "0 transactions imported.";

		public int count = 0;
		public ArrayList req = new ArrayList();

		//This will cause issues when hit by multiple requests. Move to session.
		static Result result;
		static string accountApiKey;
		static Account account;

		private class Result
		{
			public int Code { get; set; }
			public string Message { get; set; }
			public string Severity { get; set; }
			public Guid Ticket { get; set; }
		}

		[WebMethod]
		public string serverVersion()
		{
			string serverVersion = "2.0.0.1";
			return serverVersion;
		}

		[WebMethod]
		public string clientVersion(string strVersion)
		{
			string retVal=null;
			double recommendedVersion  = 1.5;
			double supportedMinVersion = 1.0;
			double suppliedVersion = Convert.ToDouble(ParseForVersion(strVersion));
			
			if(suppliedVersion < recommendedVersion) {
				retVal = "W:We recommend that you upgrade your QBWebConnector";
			}
			else if(suppliedVersion < supportedMinVersion)
			{
				retVal = "E:You need to upgrade your QBWebConnector";
			}

			return retVal;
		}

		/// <summary>
		/// Authenticates the request.
		/// </summary>
		/// <param name="strUserName">Name of the STR user.</param>
		/// <param name="strPassword">The STR password.</param>
		/// <returns></returns>
		[WebMethod]
		public string[] authenticate(string strUserName, string strPassword)
		{
			string[] authReturn = new string[2];
			
			//Create session ticket
			authReturn[0] = System.Guid.NewGuid().ToString();

			//Get the user account
			account = Account.Get(accountApiKey);

			if (strUserName.Trim().Equals(strUserName) && strPassword.Trim().Equals(account.PassPhrase))
			{
				//Set the AccountApiKey value
				accountApiKey = strUserName;

				authReturn[1] = "";
			}
			else
				authReturn[1] = "nvu";

			return authReturn;
		}

		/// <summary>
		/// Handles error between Quickbooks and the Web Connector.
		/// </summary>
		/// <param name="ticket">The ticket.</param>
		/// <param name="hresult">The hresult.</param>
		/// <param name="message">The message.</param>
		/// <returns></returns>
		[WebMethod(EnableSession = true)]
		public string connectionError(string ticket, string hresult, string message)
		{
			if (Session["ce_counter"] == null)
				Session["ce_counter"] = 0;

			string retVal = null;

			// 0x80040400 - QuickBooks found an error when parsing the provided XML text stream. 
			const string QB_ERROR_WHEN_PARSING = "0x80040400";

			// 0x80040401 - Could not access QuickBooks.  
			const string QB_COULDNT_ACCESS_QB = "0x80040401";

			// 0x80040402 - Unexpected error. Check the qbsdklog.txt file for possible, additional information. 
			const string QB_UNEXPECTED_ERROR = "0x80040402";
			// Add more as you need...

			if (hresult.Trim().Equals(QB_ERROR_WHEN_PARSING))
			{
				retVal = "DONE";
			}
			else if (hresult.Trim().Equals(QB_COULDNT_ACCESS_QB))
			{
				retVal = "DONE";
			}
			else if (hresult.Trim().Equals(QB_UNEXPECTED_ERROR))
			{
				retVal = "DONE";
			}
			else
			{
				// Depending on various hresults return different value 
				if ((int)Session["ce_counter"] == 0)
				{
					retVal = "";
				}
				else
				{
					retVal = "DONE";
				}
			}

			Session["ce_counter"] = ((int)Session["ce_counter"]) + 1;

			return retVal;
		}

		/// <summary>
		/// Sends the XML request to Quickbooks for verification.
		/// </summary>
		/// <param name="ticket">The ticket.</param>
		/// <param name="strHCPResponse">The STR HCP response.</param>
		/// <param name="strCompanyFileName">Name of the STR company file.</param>
		/// <param name="qbXMLCountry">The qb XML country.</param>
		/// <param name="qbXMLMajorVers">The qb XML major vers.</param>
		/// <param name="qbXMLMinorVers">The qb XML minor vers.</param>
		/// <returns></returns>
		[WebMethod(EnableSession = true)]
		public string sendRequestXML(string ticket, string strHCPResponse, string strCompanyFileName, string qbXMLCountry, int qbXMLMajorVers, int qbXMLMinorVers)
		{
			if (Session["counter"] == null)
				Session["counter"] = 0;

			//Build the xml requests
			ArrayList req = XmlProcess.BuildXmlRequests(account);
			string request = "";
			int total = req.Count;
			count = Convert.ToInt32(Session["counter"]);

			if (count < total)
			{
				request = req[count].ToString();
				Session["counter"] = ((int)Session["counter"]) + 1;
			}
			else
			{
				count = 0;
				Session["counter"] = 0;
				request = "";

				//Set the session result
				result = new Result()
				{
					Ticket = Guid.Parse(ticket),
					Code = 1, //OK
					Message = MessageNoTrans
				};
			}

			return request;
		}

		/// <summary>
		/// Receives the response from Quickbooks and determines if an error occurred with the XML request.
		/// </summary>
		/// <error>
		///	<InvoiceAddRs requestID="1" statusCode="3020" statusSeverity="Error" statusMessage="string value" />
		/// </error>
		/// <param name="ticket">The ticket.</param>
		/// <param name="response">The response.</param>
		/// <param name="hresult">The hresult.</param>
		/// <param name="message">The message.</param>
		/// <returns></returns>
		[WebMethod(EnableSession = true)]
		public int receiveResponseXML(string ticket, string response, string hresult, string message)
		{
			int retVal;

			if(!string.IsNullOrEmpty(response))
			{
				//Parse the xml to locate an error
				XElement responseXml = XElement.Parse(response);
				result = (from item in responseXml.Descendants("QBXMLMsgsRs").Descendants().Take(1)
						  select new Result
						  {
							  Code = Convert.ToInt32(item.Attribute("statusCode").Value),
							  Message = item.Attribute("statusMessage").Value,
							  Severity = item.Attribute("statusSeverity").Value
						  }).SingleOrDefault();

				//Create invoice response session
				SessionFacade.InvoiceResponse = responseXml.Descendants("InvoiceRet").Count() > 0 ? response : null; //Set the session to null if this is anything except an invoice response

				//Create payment response session
				SessionFacade.PaymentResponse = responseXml.Descendants("ReceivePaymentRet").Count() > 0 ? response : null; //Set the session to null if this is anything except an invoice response
			}
			else
			{
				result = new Result()
				{
					Code = 100, //Custom error code
					Severity = "Error",
					Message = message
				};
			}

			//If there is an error, return the error code
			if (result != null && result.Severity == "Error")
			{
				//Set the session ticket
				result.Ticket = Guid.Parse(ticket);

				//Return the error code
				retVal = -result.Code;
			}
			else
			{
				ArrayList req = XmlProcess.BuildXmlRequests(account);
				int total = req.Count;
				int count = Convert.ToInt32(Session["counter"]);

				int percentage = (count * 100) / total;
				if (percentage >= 100)
				{
					count = 0;
					Session["counter"] = 0;
				}
				retVal = percentage;
			}

			return retVal;
		}

		/// <summary>
		/// Returns the error response if an error is found in the XML request.
		/// </summary>
		/// <param name="ticket">The ticket.</param>
		/// <returns></returns>
		[WebMethod]
		public string getLastError(string ticket)
		{
			//Create the error response
			string message = "";

			if (result.Ticket != Guid.Parse(ticket))
				return string.Format(MessageError, MessageInvalidSession);

			switch(result.Code)
			{
				case 1:
					//Leave blank to display success message
					break;

				default:
					message = string.Format(MessageError, result.Code + " - " + result.Message);
					break;
			}

			//Return the error response
			return message;
		}

		/// <summary>
		/// Closes the connection.
		/// </summary>
		/// <param name="ticket">The ticket.</param>
		/// <returns></returns>
		[WebMethod(EnableSession = true)]
		public string closeConnection(string ticket)
		{
			string message = "Success. {0}";
			string retVal = "";

			//Set the success result message
			switch(result.Code)
			{
				case 1:
					retVal = string.Format(message, result.Message);
					break;

				default:
					retVal = string.Format(message, "");
					break;
			}

			//Remove session values
			SessionFacade.ClearAll();
			accountApiKey = string.Empty;
			result = null;
			
			return retVal;
		}

		/// <summary>
		/// Parses for version.
		/// </summary>
		/// <param name="input">The input.</param>
		/// <returns></returns>
		private string ParseForVersion(string input)
		{
			string retVal = "";
			string major = "";
			string minor = "";
			Regex version = new Regex(@"^(?<major>\d+)\.(?<minor>\d+)(\.\w+){0,2}$", RegexOptions.Compiled);
			Match versionMatch = version.Match(input);
			
			if (versionMatch.Success)
			{
				major = versionMatch.Result("${major}");
				minor = versionMatch.Result("${minor}");
				retVal = major + "." + minor;
			}
			else
				retVal = input;

			return retVal;
		}
    }
}

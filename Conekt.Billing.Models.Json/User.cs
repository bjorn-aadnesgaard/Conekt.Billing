using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Conekt.Billing.Models.Json
{
	public class User
	{
		public string UserName { get; set; }
		public string FullName { get { return FirstName + " " + LastName; } }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public string Phone { get; set; }
		public int PhoneExt { get; set; }
		public string Address { get; set; }
		public string City { get; set; }
		public string Province { get; set; }
		public string PostalCode { get; set; }
		public string Country { get; set; }
	}
}

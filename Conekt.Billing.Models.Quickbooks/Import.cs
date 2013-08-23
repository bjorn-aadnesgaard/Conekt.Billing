using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Conekt.Billing.Models;
using Conekt.Billing.Configuration;

namespace Conekt.Billing.Quickbooks
{
	public class Import
	{
		/// <summary>
		/// Record the import batch
		/// </summary>
		/// <param name="trans">List of TransactionID's</param>
		public static void Record(ArrayList listTrans)
		{
			//If importing is disabled, do not record the import batch
			if (ConfigurationManager.GetConfiguration().Import.Disabled)
				return;

			//Create the BatchID
			Guid batchID = Guid.NewGuid();

			//Record the import
			ConektBillingEntities db = new ConektBillingEntities();
			foreach(var item in listTrans)
			{
				//Create the import object
				TransactionImport ti = new TransactionImport()
				{
					ID = Guid.NewGuid(),
					BatchID = batchID,
					DateCreated = DateTime.UtcNow,
					TransactionID = Guid.Parse(item.ToString())
				};

				//Add object
				db.TransactionImport.AddObject(ti);
			}

			//Save changes
			db.SaveChanges();
		}
	}
}
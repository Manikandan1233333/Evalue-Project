using System;
using System.Web.Services.Protocols;
using SalesXReportClasses;

namespace SalesXReportClasses.Service
{
	/// <summary>
	/// Summary description for SalesXReportReference.
	/// </summary>
	
	public class SalesXReportClass : CSAAWeb.Web.SoapHttpClientProtocol
	{
		[SoapDocumentMethodAttribute]
		public System.Data.DataSet SalesXReport(SalesXReportCriteria SearchFor)
		{
			return (System.Data.DataSet) Invoke(new object[] {SearchFor})[0];
		}
	}
}
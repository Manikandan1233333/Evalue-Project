/* 
 * History:
 * Modified By Cognizant as a part of Q1 Retrofit
 * 04/27/2006 Q1-Retrofit.Ch1: Added code to Log the Response in Soap Log as part of CSR #4833
 * 4/13/2010-MODIFIED BY COGNIZANT AS PART OF HO-6
 * HO-6.ch1:Added a new parameter @AppId to the cmd object of SalesXReport() method.
 */

using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Services;
using SalesXReportClasses;
using CSAAWeb;
//Q1-Retrofit.Ch1 : START - Added code to Log the Response in Soap Log as part of CSR #4833
using CSAAWeb.AppLogger;
//Q1-Retrofit.Ch1 : END

namespace SalesXReportClasses.WebService
{
	/// <summary>
	/// Summary description for SalesXReportWebService.
	/// </summary>
	[WebService(Namespace="http://csaa.com/webservices/")]
	public class SalesXReportClass : CSAAWeb.Web.SqlWebService
	{
		[WebMethod(Description="SalesXReportWebService",BufferResponse=true)]
		public DataSet SalesXReport(SalesXReportCriteria SearchFor)
		{
			SqlCommand Cmd = GetCommand("Salesx_Reports");
			SearchFor.CopyTo(Cmd);
			//Added by cognizant as a part of HO-6 on 4-13-2010.
			Cmd.Parameters["@AppId"].Value=SearchFor.GetAppId();
			SqlDataAdapter sqlDa = new SqlDataAdapter(Cmd);
			DataSet ds = new DataSet();
			sqlDa.Fill(ds,"Test");
			//Q1-Retrofit.Ch1 : START - Added code to Log the Response in Soap Log as part of CSR #4833
			if (ds.Tables.Count!=0)
			{
				Logger.LogToFile("SoapLog",ds.GetXml());
			}
			//Q1-Retrofit.Ch1 : END
			return ds;
		}
	}
}
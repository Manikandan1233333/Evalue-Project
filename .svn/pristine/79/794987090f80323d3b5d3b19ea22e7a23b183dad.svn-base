/*
 * Contains most of the function calls required for processing an order.
 * 
 * Revision History
 * 
 * 02/10/03 Bindu Removed _SetOrderStageFlag and added SetOrderStageFlag in order to call this function from the main page.
 *		--	Added GetCityState functiion to return city and state based on zip code.
 * 
 * 8/4/03 JOM Replace reference to Applogger with CSAAWeb.AppLogger
 * 5/15/05 AZL Added GetPrices and IsCompleteMembershipRecord for Membership IVR
 * 
 * 
 * Modified by COGNIZANT AS PART OF UT-CPM Retrofit CHANGES
 * 01/10/2008 - UT-CPM Retrofit.Ch1: Added the output parameter in MSCR_CALCRATES_SP stored procedure
 * 01/10/2008 - UT-CPM Retrofit.Ch2: Modified the conditions to display rate state and flag information in Pricing Grid
 * Modified by COGNIZANT AS PART OF Premier Membership Changes
 * 05/22/2008 Premier Membership.Ch1: Added a condition to check the status of the Premier flag and                                                display the membership name  "Premier" along with the rate in the UI.
 * Modified by cognizant  as a part of MP-Story131
 * MP-Story131.Ch1:Commented a code in GetPricingBreakdown() method which using to calculate the age and assigns to  the parameters list on 06-17-2010.
 * MP-Story131.Ch2:Commented a code in _GetEligibleAssociateCount() method which using to calculate the age and assigns to ineligible associate count on 06-17-2010.
 * */

using System;
using System.Data;
using System.Data.SqlClient;
using CSAAActors;
using MSCRCore;
using CSAAWeb.AppLogger;

// TODO:
// Add the following info re: MSCRUser
//
// 1. password
// 2. grant exec on 
//		SP_CHECK_AND_LOG_ERROR
//		SP_CONTENT_RESTORE_MEMBE_DETAILS
//		SP_INSERT_CUST_INFO
//		SP_INSERT_CUST_MBR
//		SP_INSERT_CUST_ORDER
//		SP_INSERT_ORDER
//		SP_RESTORE_CONSTANTS
//
// 3. add comments to above sp's and code to warn of usage!!!
 

namespace MSCRCore
{
	/// <summary>
	/// MSC Renewal Core class
	/// </summary>
	/// <remarks>
	/// Implements core application logic and miscellaneous functions
	/// </remarks>


	// TODO: review safety of using statics here!!!!

	public class Core
	{
		public static string SP_DELIM = "|";
		public static string REC_DELIM = "~";

		private bool	_bLogErrors = false;


		/// <summary>
		/// Default constructor
		/// </summary>
		public Core() 
		{
			//
			// TODO: Add constructor logic here
			//
		}

		/// <summary>
		/// Constructor which accepts logging info from client
		/// </summary>
		/// <remarks>
		/// Overloaded constructor
		/// </remarks>
		/// <param name="logPath"></param>
		/// <param name="logErrors"></param>
		public Core(bool logErrors) 
		{
			_bLogErrors = logErrors;
		}

        //CHGXXXXXXX - CH1 - Server Upgrade 4.6.1 - Removing the methods AddOrder with respect to Member.cs - March 2016

		/// <summary>
		/// 
		/// </summary>
		/// <param name="cn"></param>
		/// <param name="orderNum"></param>
		/// <param name="authCode"></param>
		public void SetOrderStageFlag(string connectionInfo, int orderNum, string authCode) 
		{
			SqlConnection sqlCon = null;
			try 
			{
				sqlCon = new SqlConnection(connectionInfo);
				sqlCon.Open();

				SqlCommand sqlCmd = new SqlCommand("SP_UPDATE_CUST_ORDER", sqlCon);
				sqlCmd.CommandType = CommandType.StoredProcedure;

				sqlCmd.Parameters.Add(_BuildParam("@inIdCustOrder", orderNum.ToString(), SqlDbType.Int));
				sqlCmd.Parameters.Add(_BuildParam("@inCyberAuthCode", authCode, SqlDbType.VarChar, 40));

				int nRowsAffected = sqlCmd.ExecuteNonQuery();

				if (nRowsAffected == 0) 
					throw new Exception("ERROR IN _SetOrderStageFlag!  Unable to set status flag for order " 
						+ orderNum.ToString() + " via SP_UPDATE_CUST_ORDER");

				return;
			}
			catch (Exception ex) 
			{
				Logger.Log(ex);
				throw ex;
			}
			finally 
			{
				if (sqlCon != null) 
				{
					sqlCon.Close();
				}
			}
		}

        //CHGXXXXXXX - CH1 - Server Upgrade 4.6.1 - Removing the methods AddOrderDebug with respect to Member.cs - March 2016

        //CHGXXXXXXX - CH1 - Server Upgrade 4.6.1 - Removing the methods _BuildDelimOrderString with respect to Member.cs - March 2016

        //CHGXXXXXXX - CH1 - Server Upgrade 4.6.1 - Removing the methods _BuildDelimOrderString with respect to Associate.cs - March 2016

        //CHGXXXXXXX - CH1 - Server Upgrade 4.6.1 - Removing the methods _BuildDelimBillingAddressString with respect to Member.cs - March 2016

        //CHGXXXXXXX - CH1 - Server Upgrade 4.6.1 - Removing the methods _BuildDelimGiverAddressString with respect to Member.cs - March 2016

        //CHGXXXXXXX - CH1 - Server Upgrade 4.6.1 - Removing the methods GetPricingBreakdown with respect to Member.cs - March 2016

        //CHGXXXXXXX - CH1 - Server Upgrade 4.6.1 - Removing the methods GetPrices with respect to Member.cs - March 2016

        //CHGXXXXXXX - CH1 - Server Upgrade 4.6.1 - Removing the methods IsCompleteMembershipRecord with respect to Member.cs - March 2016
        	

		/// <summary>
		/// Determines if member qualifies for straight-through renewal
		/// </summary>
		/// <remarks>
		/// NOTE: this is a driver function to invoke stored proc 
		/// MSCR_LIST_INELIGIBILITIES_SP. Proc contains business logic!
		/// </remarks>
		/// <param name="dataSource"></param>
		/// <param name="MemberId"></param>
		/// <param name="ClubCode"></param>
		/// <param name="AssociateId"></param>
		/// <param name="dtDetails"></param>
		/// <returns></returns>
		public bool IsStraightThroughRenewal(string dataSource,
			string MemberId,
			string ClubCode,
			string AssociateId,
			ref DataTable dtDetails) 
		{

			SqlConnection sqlCon = null;

			try 
			{
				sqlCon = new SqlConnection(dataSource);
				sqlCon.Open();

				SqlCommand sqlCmd = new SqlCommand();
				sqlCmd.CommandText = "MSCR_LIST_INELIGIBILITIES_SP";
				sqlCmd.CommandType = CommandType.StoredProcedure;
				sqlCmd.Connection = sqlCon;

				sqlCmd.Parameters.Add(_BuildParam("@MbrId",		MemberId,	SqlDbType.Char, 8));
				sqlCmd.Parameters.Add(_BuildParam("@ClubCd",	ClubCode,	SqlDbType.Char, 3));
				sqlCmd.Parameters.Add(_BuildParam("@AssocId",	AssociateId,SqlDbType.Char, 1));

				SqlDataReader rdr = sqlCmd.ExecuteReader();

				// @outEligible returns 1 if eligible (straight thru renewal), 0 if *NOT* eligible
				bool bIsElig = false;

				DataTable dtElig = new DataTable();

				dtElig.Columns.Add("AssociateId", typeof(string));
				dtElig.Columns.Add("Description", typeof(string));

				// build a datatable only if we have to... if not eligible we need reasons why
				while (rdr.Read()) 
				{
					// TODO: magic value for CA/NV primary associate!
					// eligible means primary record eligibility flag is 1
					if (rdr["AssocId"].ToString() == "1") 
					{
						bIsElig = (rdr["Eligibility"].ToString() == "1");
					}
					dtElig.Rows.Add(new string [] { rdr["AssocId"].ToString(),
													  rdr["DscIneligibility"].ToString()} );
					dtDetails = dtElig;
				}

				// if member is eligible, then it's a straight through renewal
				return bIsElig;
			}
			catch (Exception ex)
			{
				Logger.Log(ex);
				throw ex;
			}
			finally 
			{
				sqlCon.Close();
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="RequesterAppId"></param>
		/// <param name="RequesterUserNm"></param>
		/// <param name="UserName"></param>
		/// <param name="AppId"></param>
		/// <returns></returns>
		public DataSet GetUserActivity(string ConnStr,
			string RequesterAppId,
			string RequesterUserNm,
			string UserName,
			string AppId) 
		{

			DataSet ds = new DataSet();

			// calls sp_GetOrdersForUser
			SqlConnection sqlCn = new SqlConnection(ConnStr);

			try 
			{
				string sql = "exec MSCR_GET_USER_ORDERS_SP "
					+ RequesterAppId + ", '" + RequesterUserNm 
					+ "', '" + UserName + "', " + AppId;

				SqlDataAdapter sqlDa = new SqlDataAdapter(sql, sqlCn);
				sqlDa.Fill(ds);
			}
			catch  (Exception ex) 
			{
				Logger.Log(ex);
			}
			finally 
			{
				if (sqlCn.State == ConnectionState.Open) 
				{
					sqlCn.Close();
				}
			}

			return ds;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="RequesterAppId"></param>
		/// <param name="RequesterUserNm"></param>
		/// <param name="ConnString"></param>
		/// <param name="ZipCode"></param>
		/// <returns>City,State</returns>
		public Array GetCityState(string ConnStr,
			string ZipCode)
		{

			string[] ArrCityState = new string[2] ;
			SqlConnection sqlCn = null;

			try 
			{
				sqlCn = new SqlConnection(ConnStr);
				sqlCn.Open();

				SqlCommand sqlCmd = new SqlCommand();
				sqlCmd.CommandText = "MSCR_GET_CITYSTATE_SP";
				sqlCmd.CommandType = CommandType.StoredProcedure;
				sqlCmd.Connection = sqlCn;
				
				sqlCmd.Parameters.Add(_BuildParam("@inZipCode",	ZipCode, SqlDbType.Char, 10));
				SqlParameter paramCity = sqlCmd.Parameters.Add("@outCity", SqlDbType.VarChar, 50);
				paramCity.Direction = ParameterDirection.Output;
				SqlParameter paramState = sqlCmd.Parameters.Add("@outState", SqlDbType.Char, 4);
				paramState.Direction = ParameterDirection.Output;				
				
				sqlCmd.ExecuteNonQuery();

				ArrCityState.SetValue(paramCity.Value.ToString(),0);
                ArrCityState.SetValue(paramState.Value.ToString(),1);
				

			}
			catch  (Exception ex) 
			{
				Logger.Log(ex);
			}
			finally 
			{
				if (sqlCn.State == ConnectionState.Open) 
				{
					sqlCn.Close();
				}
			}

			return ArrCityState;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="lst"></param>
		public static void LoadMonthValues(ref System.Web.UI.WebControls.DropDownList lst) 
		{

			lst.Items.Add(new System.Web.UI.WebControls.ListItem("Select", string.Empty));
			lst.Items.Add(new System.Web.UI.WebControls.ListItem("January", "1"));
			lst.Items.Add(new System.Web.UI.WebControls.ListItem("February", "2"));
			lst.Items.Add(new System.Web.UI.WebControls.ListItem("March", "3"));
			lst.Items.Add(new System.Web.UI.WebControls.ListItem("April", "4"));
			lst.Items.Add(new System.Web.UI.WebControls.ListItem("May", "5"));
			lst.Items.Add(new System.Web.UI.WebControls.ListItem("June", "6"));
			lst.Items.Add(new System.Web.UI.WebControls.ListItem("July", "7"));
			lst.Items.Add(new System.Web.UI.WebControls.ListItem("August", "8"));
			lst.Items.Add(new System.Web.UI.WebControls.ListItem("September", "9"));
			lst.Items.Add(new System.Web.UI.WebControls.ListItem("October", "10"));
			lst.Items.Add(new System.Web.UI.WebControls.ListItem("November", "11"));
			lst.Items.Add(new System.Web.UI.WebControls.ListItem("December", "12"));

		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="lst"></param>
		/// <param name="Span"></param>
		public static void LoadYearValues(ref System.Web.UI.WebControls.DropDownList lst, int Span) 
		{

			lst.Items.Add(new System.Web.UI.WebControls.ListItem("Select", string.Empty));

			int nCurrYear= System.DateTime.Now.Year;
			for (int idx = nCurrYear; idx < nCurrYear + Span; idx++) 
			{
				string sYr = idx.ToString();
				lst.Items.Add(new System.Web.UI.WebControls.ListItem(sYr, sYr));
			}

		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="lst"></param>
		/// <param name="dt"></param>
		public static void LoadStateCodes(ref System.Web.UI.WebControls.DropDownList lst, 
			DataTable dt, string SelectedState) 
		{

			// TODO: standardize code table processing
			foreach (DataRow dr in dt.Rows) 
			{
				string stateCode = dr["StateCode"].ToString();
				System.Web.UI.WebControls.ListItem itm = new System.Web.UI.WebControls.ListItem(stateCode, stateCode);
				itm.Selected = Convert.ToBoolean(stateCode == SelectedState);
				lst.Items.Add(itm);
			}

		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="lst"></param>
		/// <param name="dt"></param>
		/// <param name="selectedcode"></param>
		public static void LoadCreditCardValues(ref System.Web.UI.WebControls.DropDownList lst,
			DataTable dt, string selectedcode) 
		{

			// TODO: standardize code table processing
			foreach (DataRow dr in dt.Rows) 
			{
				string cccode = dr["Code"].ToString();
				string ccname = dr["Name"].ToString();
				System.Web.UI.WebControls.ListItem itm = new System.Web.UI.WebControls.ListItem(ccname, cccode);
				itm.Selected = Convert.ToBoolean(cccode == selectedcode);
				lst.Items.Add(itm);
			}
		}

		private static SqlParameter _BuildParam(string nm,
			string val,
			SqlDbType typ,
			int sz, 
			bool convertNull) 
		{

			SqlParameter p	= new SqlParameter();
			p.ParameterName = nm;
			if ((convertNull) && (val.Length == 0)) 
			{
				p.Value		= DBNull.Value;
			}
			else 
			{
				p.Value		= val;
			}
			p.SqlDbType		= typ;

			if (sz > 0) 
			{
				p.Size = sz;
			}

			return p;
		}

		// default to input dir w/ specified size
		/// <summary>
		/// Helper function for building SQL parameter
		/// </summary>
		/// <param name="nm"></param>
		/// <param name="val"></param>
		/// <param name="typ"></param>
		/// <param name="sz"></param>
		/// <returns></returns>
		private static SqlParameter _BuildParam(string nm,
			string val,
			SqlDbType typ,
			int sz) 
		{

			return _BuildParam(nm, val, typ, ParameterDirection.Input, sz);
		}

		// default to input dir, no specific size
		/// <summary>
		/// Overloaded version of parameter help function
		/// </summary>
		/// <param name="nm"></param>
		/// <param name="val"></param>
		/// <param name="typ"></param>
		/// <returns></returns>
		private static SqlParameter _BuildParam(string nm,
			string val,
			SqlDbType typ) 
		{

			return _BuildParam(nm, val, typ, ParameterDirection.Input, 0);
		}

		// no defaults
		/// <summary>
		/// Overloaded version of parameter help function, no defaults
		/// </summary>
		/// <param name="nm"></param>
		/// <param name="val"></param>
		/// <param name="typ"></param>
		/// <param name="dir"></param>
		/// <param name="sz"></param>
		/// <returns></returns>
		private static SqlParameter _BuildParam(string nm, 
			string val, 
			SqlDbType typ,
			ParameterDirection dir,
			int sz) 
		{

			SqlParameter p	= new SqlParameter();
			p.ParameterName = nm;
			p.Value			= val;
			p.SqlDbType		= typ;
			p.Direction		= dir;

			if (sz > 0) 
			{
				p.Size = sz;
			}

			return p;
		}

        //CHGXXXXXXX - CH1 - Server Upgrade 4.6.1 - Removing the methods _GetEligibleAssociateCount with respect to Member.cs - March 2016
		
	}
}


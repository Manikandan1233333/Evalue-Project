/*MODIFIED BY COGNIZANT AS PART OF PT MAIG Changes
 * This page has been newly created for PT MAIG Changes.
 * CHG0115410 - Modified the input request, query used & the response class objects in order to use the Upgraded Customer Search API
 *  CHG0115410 - CH1 - Modified the input request passed to the Customer Search API
 *  CHG0115410 - CH2 - Modified the query with respect to the new response generated from Upgraded Customer Search API.
 *  CHG0115410 - CH3 - Modified the GetAddresss function to pass partyAddress as new array parameter for fetching customer address information from response.
 *  CHG0116140 - CH1 - Added FirstCharToUpper function to modify 1st Letter of string as Upper Case
 *  CHG0116140 - CH2 - Modified SingleOrDefault to LastorDefault to fetch the last address from the response
 * CHG0131014 - OTSP/SUBRO/PRB0047380/SECURITY FIXES - Updated Radio Button Selected Index Changing for OTSP changes
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OrderClassesII;
using System.Net;
using CSAAWeb;
using CSAAWeb.AppLogger;
using System.IO;
using System.Text;
using System.Web.Script.Serialization;
using System.Runtime.Serialization.Json;
using CSAAWeb.WebControls;
using System.Data;

namespace MSC.Controls
{
    [Serializable]
    public class SearchResultEntity
    {
        public string SourceSystem { get; set; }
        public string policyNumber { get; set; }
        public string policyStatus { get; set; }
        public string productCode { get; set; }
        public string NamedInsured { get; set; }
        public string address { get; set; }
    }

    public class SemiNumericComparer : IComparer<string>
    {
        public int Compare(string s1, string s2)
        {
            if (IsNumeric(s1) && IsNumeric(s2))
            {
                if (Convert.ToInt32(s1) > Convert.ToInt32(s2)) return 1;
                if (Convert.ToInt32(s1) < Convert.ToInt32(s2)) return -1;
                if (Convert.ToInt32(s1) == Convert.ToInt32(s2)) return 0;
            }

            if (IsNumeric(s1) && !IsNumeric(s2))
                return -1;

            if (!IsNumeric(s1) && IsNumeric(s2))
                return 1;

            return string.Compare(s1, s2, true);
        }

        public static bool IsNumeric(object value)
        {
            try
            {
                int i = Convert.ToInt32(value.ToString());
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
    }

    /// <summary>
    /// Class to Display the Name Search Results
    /// </summary>
    public partial class NameSearch : System.Web.UI.UserControl
    {
        public List<SearchResultEntity> SearchResult;
        TextBox hiddenData;
        Validator productType;
        Validator revenueType;
        Validator policy;
        DropDownList drpproductType;
        TextBox policyNumber;

        /// <summary>
        /// Page Load event that loads the controls
        /// </summary>
        /// <param name="sender">Sender of the event</param>
        /// <param name="e">Event Argument e</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            drpproductType = (DropDownList)this.Parent.FindControl("_ProductType");
            if (Page.Request.AppRelativeCurrentExecutionFilePath.Contains("Insurance.aspx"))
            {
                hiddenData = (TextBox)this.Parent.FindControl("HiddenNameSearch");
                productType = (Validator)this.Parent.FindControl("vldProductType");
                revenueType = (Validator)this.Parent.FindControl("vldRevenueType");
                policy = (Validator)this.Parent.FindControl("_Policy").FindControl("LabelValidator");
                policyNumber = (TextBox)this.Parent.FindControl("_Policy").FindControl("_Policy");
            }
            //LastNameCheck = vldLastNameReq;
            //MailingZipCheck = vldMailingZipCheck;
            //LastName = vldLastName;
            //MailingZip = vldMailingZip;

            if (IsPostBack)
            {
                //SkipChildValidation.Value = "false";
            }
        }

        #region commented Code
        /*protected void ReqValLastName(object source, CSAAWeb.WebControls.ValidatorEventArgs args)
        {
            if (!Convert.ToBoolean(SkipChildValidation.Value))
            {
                args.IsValid = ((_LastName.Text != "" && CSAAWeb.Validate.IsAllChars(_LastName.Text)) || false);
                if (!args.IsValid) vldLastName.MarkInvalid();
            }
            //else
            //{
            //    args.IsValid = true;
            //    SkipChildValidation.Value = "false";
            //}
        }

        protected void CheckMailingZip(object source, CSAAWeb.WebControls.ValidatorEventArgs args)
        {
            if (!Convert.ToBoolean(SkipChildValidation.Value))
            {
                args.IsValid = ((_MailingZip.Text != "" && CSAAWeb.Validate.IsValidZip(_MailingZip.Text)) || false);
                if (!args.IsValid) vldMailingZip.MarkInvalid();
            }
            //else
            //{
            //    args.IsValid = true;
            //    SkipChildValidation.Value = "false";
            //}
        }*/
        //protected void NameSearchResults_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    if (e.Row.RowType == DataControlRowType.Footer)
        //    {

        //        //// Setting footer content to the first cell
        //        //e.Row.Cells[0].Text = "Showing Pages " + (NameSearchResults.PageIndex + 1) + " of " + NameSearchResults.PageCount;

        //        //// Setting first cell to occupy the entire width
        //        //e.Row.Cells[0].ColumnSpan = e.Row.Cells.Count;

        //        //// Removing all the cells except the first cell in the footer
        //        //for (int i = 1; i < e.Row.Cells.Count; )
        //        //{
        //        //    e.Row.Cells.RemoveAt(i);
        //        //}

        //    }
        //}

        //if (e.Row.RowType == DataControlRowType.Footer)
        //{
        //    int cellCount = e.Row.Cells.Count;
        //    for (int i = cellCount - 1; i >= 1; i += -1)
        //    {
        //        e.Row.Cells.RemoveAt(i);
        //    }
        //    e.Row.Cells[0].ColumnSpan = cellCount;
        //    e.Row.Cells[0].Text = "Showing Pages " + (NameSearchResults.PageIndex + 1) + " of " + NameSearchResults.PageCount;
        //}
        //if (e.Row.RowType == DataControlRowType.Footer)
        //{
        //    int cellCount = e.Row.Cells.Count;
        //    for (int i = cellCount - 1; i >= 1; i += -1)
        //    {
        //        e.Row.Cells.RemoveAt(i);
        //    }
        //    e.Row.Cells[0].ColumnSpan = cellCount;
        //    e.Row.Cells[0].Text = "Showing Pages " + (NameSearchResults.PageIndex + 1) + " of " + NameSearchResults.PageCount;
        //}


        /*protected void rbtnSelect_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton selectButton = (RadioButton)sender;
            GridViewRow row = (GridViewRow)selectButton.Parent.Parent;
            int a = row.RowIndex;
            foreach (GridViewRow rw in NameSearchResults.Rows)
            {
                if (selectButton.Checked)
                {
                    if (rw.RowIndex != a)
                    {
                        RadioButton rd = rw.FindControl("rbtnSelect") as RadioButton;
                        rd.Checked = false;
                    }
                }
            }
        }*/

        /*public void PromptError() 
        {
            if (_LastName.Text != "" && CSAAWeb.Validate.isValidLastName(_LastName.Text))
            {
                //lblLastName.ForeColor = System.Drawing.Color.Red;
                lblLastName.Attributes.Add("style", "color:red;font-weight: bold;font-family: arial, helvetica, sans-serif; font-size: 12px;");
            }
            if (_MailingZip.Text != "" && CSAAWeb.Validate.IsValidZip(_MailingZip.Text))
            {
                //lblMailingZip.ForeColor = System.Drawing.Color.Red;
                lblMailingZip.Attributes.Add("style", "color:red;font-weight: bold;font-family: arial, helvetica, sans-serif; font-size: 12px;");
            }
        }*/

        /*switch (item["policystatus"].ToString())
                {
                    case Constants.PC_POL_ACTIVE_NOTATION:
                        {
                            PolStatus = Constants.PC_POL_ACTIVE;
                            break;
                        }
                    case Constants.PC_POL_LAPSED_NOTATION:
                        {
                            PolStatus = Constants.PC_POL_LAPSED;
                            break;
                        }
                    case Constants.PC_POL_CANCEL_NOTATION:
                        {
                            PolStatus = Constants.PC_POL_CANCEL;
                            break;
                        }
                }
                item["policystatus"] = PolStatus;
                */

        //                        if (searchResponse.responseJson != null && searchResponse.responseJson.searchedPolicies != null && searchResponse.responseJson.searchedPolicies.Count > 0)
        //                        {
        //                            //SearchResult = searchResponse.responseJson.searchedPolicies.Where(s => s.SourceSystem != "MNCNU").SelectMany(s => s.consumers, (s, c) => new
        //                            //|| s.consumers.FirstOrDefault().partyroles.FirstOrDefault().roleType == "Named Insured" || s.consumers.FirstOrDefault().partyroles.FirstOrDefault().roleType == "Insured"
        ////                            SearchResult = searchResponse.responseJson.searchedPolicies.Where(s => s.SourceSystem != "MNCNU" && (s.consumers.FirstOrDefault().partyroles.FirstOrDefault().roleType == "Named Insured" || s.consumers.FirstOrDefault().partyroles.FirstOrDefault().roleType == "Insured")).SelectMany(s => s.consumers, (s, c) => new SearchResultEntity
        //                            SearchResult = searchResponse.responseJson.searchedPolicies.Where(s => s.SourceSystem != "MNCNU").SelectMany(s => s.consumers, (s, c) => new SearchResultEntity
        //                            {
        //                                SourceSystem = (s.SourceSystem == "COGEN") ? "KIC" : (s.SourceSystem == "PUP") ? "PUPSYS" : (s.SourceSystem == "MAIS") ? "KIC" : s.SourceSystem,
        //                                policyNumber = (s.SourceSystem == "MAIS") ? s.policyNumber.Substring(1) : (s.SourceSystem == "SIS" && s.policyNumber.StartsWith(Config.Setting("SISpolicyPrefix"))) ? s.policyNumber.Substring(2) : (s.SourceSystem == "SIS") ? s.policyNumber.Substring(3) : s.policyNumber,
        //                                policyStatus=s.PolicyStatus,
        //                                productCode = (s.prodTypeCode == "HO") ? "HOME" : (s.prodTypeCode == "AU") ? "AUTO" : (s.prodTypeCode =="SPCL" && s.productCode =="MC")? "MotorCycle" :(s.prodTypeCode =="SPCL" && s.productCode == "PWC")? "Watercraft": s.prodTypeCode,
        //                                NamedInsured = c.firstName + " " + c.lastName,    
        //                                //(c.partyroles.FirstOrDefault().roleType == "Named Insured" || c.partyroles.FirstOrDefault().roleType == "Insured")? c.firstName + "" + c.lastName : c.firstName + "" + c.lastName,   
        //                                //address = string.Format("{0} - {1}", s.policyaddress.FirstOrDefault().address1,s.policyaddress.FirstOrDefault().zipCode)
        //                                address = string.Format("{0}, {1}, {2},{3}", (s.policyaddress.FirstOrDefault().addressType == "billingaddress" && s.SourceSystem =="COGEN") ? s.policyaddress.FirstOrDefault().address1 : s.policyaddress.FirstOrDefault().address1, s.policyaddress.FirstOrDefault().city, s.policyaddress.FirstOrDefault().state, s.policyaddress.FirstOrDefault().zipCode)    
        //                                //address = string.Format("{0} - {1}", (s.policyaddress.FirstOrDefault().addressType == "billingaddress" && s.policyaddress.FirstOrDefault().address1 != null) ? s.policyaddress.FirstOrDefault().address1 : s.policyaddress.FirstOrDefault().address1, s.policyaddress.FirstOrDefault().zipCode)    
        //                                //address = string.Format("{0} - {1}", c.address.FirstOrDefault().address1, c.address.FirstOrDefault().zipCode)

        //                            }).Distinct(). OrderBy(t=>t.policyNumber).ToList();


        //                            //Assigns grid data
        //                            NameSearchResults.DataSource = SearchResult;
        //                            //Keeps in Session object for maintaining pagination
        //                            //Session["NameSearchResult"] = SearchResult;
        //                        }

        //Validator productType = (Validator)this.Parent.FindControl("vldProductType");
        //productType.IsValid = true;
        //Validator revenueType = (Validator)this.Parent.FindControl("vldRevenueType");
        //revenueType.IsValid = true;
        //Validator policy = (Validator)this.Parent.FindControl("_Policy").FindControl("LabelValidator");
        //policy.IsValid = true;


        //11/05/2014 commented below code to make the hard coded values into constants. After unit testing we will remove this commented code 11/05/2014
        //select (new SearchResultEntity
        //{
        //    SourceSystem = (result.SourceSystem == "COGEN") ? "KIC" : (result.SourceSystem == "PUP") ? "PUPSYS" : (result.SourceSystem == "MAIS") ? "KIC" : result.SourceSystem,
        //    policyNumber = (result.SourceSystem == "MAIS") ? result.policyNumber.Substring(1) : (result.SourceSystem == "SIS" && result.policyNumber.StartsWith(Config.Setting("SISpolicyPrefix"))) ? result.policyNumber.Substring(2) : (result.SourceSystem == "SIS") ? result.policyNumber.Substring(3) : result.policyNumber,
        //    policyStatus = result.PolicyStatus,
        //    productCode = (result.prodTypeCode == "HO") ? "HOME" : (result.prodTypeCode == "AU" || result.prodTypeCode == "PA") ? "AUTO" : (result.prodTypeCode == "SPCL" && result.productCode == "MC" || result.prodTypeCode == "MC") ? "MotorCycle" : (result.prodTypeCode == "SPCL" && result.productCode == "PWC" || result.prodTypeCode == "WC") ? "Watercraft" : (result.prodTypeCode == "HO" && result.productCode == "DP3" || result.prodTypeCode == "DF") ? "Dwelling Fire" : (result.prodTypeCode == "PUP" || result.prodTypeCode == "PU") ? "Personal Umbrella" : result.prodTypeCode,
        //    NamedInsured = cons.firstName + "  " + cons.lastName,
        //    address = string.Format("{0}, {1}, {2},{3}", (result.policyaddress.FirstOrDefault().addressType == "billingaddress" && result.SourceSystem == "COGEN") ? result.policyaddress.FirstOrDefault().address1 : result.policyaddress.FirstOrDefault().address1, result.policyaddress.FirstOrDefault().city, result.policyaddress.FirstOrDefault().state, result.policyaddress.FirstOrDefault().zipCode)
        //    //(result.policyaddress.FirstOrDefault().address1 != string.Empty : result.policyaddress.FirstOrDefault().address1: ""): result.policyaddress.FirstOrDefault().address1,result.policyaddress.FirstOrDefault().city, result.policyaddress.FirstOrDefault().state, result.policyaddress.FirstOrDefault().zipCode)
        //})).Distinct().OrderBy(t => t.policyNumber).ToList();
        //11/05/2014 commented above code to make the hard coded values into constants. After unit testing we will remove this commented code 11/05/2014

        #endregion

        #region Methods
        /// <summary>
        /// Populate RBPS data.
        /// </summary>
        /// <param name="srcdata">DataTable containing the RPBS Service data</param>
        public void PopulateRpbsData(DataTable srcdata)
        {
            DataTable RpbsdataPopulationDt = new DataTable();
            NameSearchResults.DataSource = null;
            NameSearchResults.DataBind();
            NameSearchResults.PageIndex = 0;
            RpbsdataPopulationDt = srcdata.DefaultView.ToTable(false, Constants.PC_BILL_POL_NUMBER, Constants.PC_BILL_SOURCE_SYSTEM, Constants.PC_BILL_POL_TYPE, Constants.PC_BILL_STATUS_DESCRIPTION, Constants.PC_BILL_INS_FULL_NAME, Constants.PC_BILL_ADDRESS);
            RpbsdataPopulationDt.Columns[Constants.PC_BILL_POL_NUMBER].ColumnName = "policyNumber";
            RpbsdataPopulationDt.Columns[Constants.PC_BILL_SOURCE_SYSTEM].ColumnName = "sourceSystem";
            RpbsdataPopulationDt.Columns[Constants.PC_BILL_POL_TYPE].ColumnName = "productCode";
            RpbsdataPopulationDt.Columns[Constants.PC_BILL_STATUS_DESCRIPTION].ColumnName = "policystatus";
            RpbsdataPopulationDt.Columns[Constants.PC_BILL_INS_FULL_NAME].ColumnName = "NamedInsured";
            RpbsdataPopulationDt.Columns[Constants.PC_BILL_ADDRESS].ColumnName = "Address";
            string ProductCode = string.Empty;
            //string PolStatus = string.Empty;//commented this line as part of FXCop - Unused Variable.
            foreach (DataRow item in RpbsdataPopulationDt.Rows)
            {
                switch (item["productCode"].ToString())
                {
                    case "PA":
                        {
                            ProductCode = "AUTO";
                            break;
                        }
                    case "HO":
                        {
                            ProductCode = "HOME";
                            break;
                        }
                    case "DF":
                        {
                            ProductCode = "DWELLING FIRE";
                            break;
                        }
                    case "WC":
                        {
                            ProductCode = "WATERCRAFT";
                            break;
                        }
                    case "MC":
                        {
                            ProductCode = "MOTORCYCLE";
                            break;
                        }
                    case "PU":
                        {
                            ProductCode = "PERSONAL UMBRELLA";
                            break;
                        }
                }
                item["productCode"] = ProductCode;

            }
            //targetData.Rows.Add(
            NameSearchResults.DataSource = RpbsdataPopulationDt;
            NameSearchResults.DataBind();
        }

        /// <summary>
        /// populateGrid - This method which populates the values in NameSearchResults gridview
        /// </summary>
        public void PopulateGrid()
        {
            if ((_LastName.Text != string.Empty && _MailingZip.Text != string.Empty))
            {
                try
                {
                    Results.Visible = true;
                    //// MAIG - Begin - 11122014
                    lblerrorMessage.Text = string.Empty;
                    lblerrorMessage.Visible = false;
                    NameSearchResults.Visible = true;
                    //// MAIG - End - 11122014
                    //Constructs request parameter for Elastic Search
                    SearchResultRequest searchRequest = new SearchResultRequest();
                    searchRequest.Parameter.RequestParam.Search = string.Empty;
                    searchRequest.Parameter.RequestParam.DocType = "Policy";
                    searchRequest.Parameter.RequestParam.FirstName = (!string.IsNullOrEmpty(_FirstName.Text.Trim())) ? _FirstName.Text.Trim() : default(string);
                    searchRequest.Parameter.RequestParam.LastName = (!string.IsNullOrEmpty(_LastName.Text.Trim())) ? _LastName.Text.Trim() : default(string);

                    //CHG0115410 - BEGIN - CH1 - Modified the input request passed to the Customer Search API
                    if (Convert.ToString(_MailingZip.Text).Trim().Length > 0)
                        //Passed zipcode type as the new parameter along with the input request to the API
                        searchRequest.Parameter.RequestParam.ZipCode.Type = new string[4] { "risk", "billing", "mailing", "home" };
                    //Modified the type of zipcode from string to zipcode class
                    searchRequest.Parameter.RequestParam.ZipCode.value = (!string.IsNullOrEmpty(_MailingZip.Text.Trim())) ? _MailingZip.Text.Trim() : default(string);
                    //CHG0115410 - END - CH1 - Modified the input request passed to the Customer Search API

                    searchRequest.Parameter.Header.ChannelType = "DSU";

                    //Constructs service call request
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Config.Setting("jsonURL"));
                    request.Method = "POST";
                    request.ContentType = "application/json; charset=utf-8";
                    using (MemoryStream ms = new MemoryStream())
                    {
                        DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(searchRequest.GetType());
                        jsonSerializer.WriteObject(ms, searchRequest);
                        using (StreamWriter writer = new StreamWriter(request.GetRequestStream()))
                        {
                            String json = Encoding.UTF8.GetString(ms.ToArray());
                            CSAAWeb.AppLogger.Logger.Log("Elastic Search Request Received. UserID: " + Page.User.Identity.Name);
                            if (Config.Setting("Logging.ElasticSearch").Equals("1"))
                            {
                                CSAAWeb.AppLogger.Logger.Log("Elastic Search Request Details." + json);
                            }
                            writer.Write(json);
                            writer.Close();
                            //txtRequest.Text = json; //TO DO: Remove this line, if you dont want to show raw request
                        }
                    }

                    //Invokes service call & gets response
                    var httpResponse = (HttpWebResponse)request.GetResponse();

                    //Constructs response entity and bind the datasource into grid
                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        //Gets the raw JSON response                        
                        string jsonResponse = streamReader.ReadToEnd();
                        //txtResponse.Text = jsonResponse.ToString(); //TO DO: Remove this line, if you dont want to show raw response

                        //TO DO: Try to use DataContractJsonSerializer instead of JavaScriptSerializer
                        //       to follow the code standard of search response (Property Name should be PASCAL CASE). 
                        //       Need to follow the same coding standard which is in the SearchRequest.

                        //Converts raw JSON response to business entity
                        JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
                        SearchResultResponse searchResponse = jsSerializer.Deserialize<SearchResultResponse>(jsonResponse);
                        CSAAWeb.AppLogger.Logger.Log(String.Format("Elastic Search Response Received. Total Number of Hits: {0}  UserID: {1} ", searchResponse.responseJson.totalHits, Page.User.Identity.Name));
                        if (Config.Setting("Logging.ElasticSearch").Equals("1"))
                        {
                            CSAAWeb.AppLogger.Logger.Log("Elastic Search Response Details." + jsonResponse);
                        }
                        //Session["NameSearchResult"] = null;
                        NameSearchResults.DataSource = null;
                        NameSearchResults.DataBind();
                        NameSearchResults.PageIndex = 0;
                        int val = 0;
                        if (searchResponse.responseJson != null && searchResponse.responseJson.searchedPolicies != null && searchResponse.responseJson.searchedPolicies.Count() > 0)
                        {
                            //CHG0115410 - BEGIN - CH2 - Modified the query with respect to the new response generated from Upgraded Customer Search API.
                            //CHG0116140 - BEGIN - CH1 - Added FirstCharToUpper function to modify 1st Letter of string as Upper Case
                            SearchResult = (from result in searchResponse.responseJson.searchedPolicies
                                            from cons in result.partyList
                                            from party in cons.partyRoles
                                            where ((!string.IsNullOrEmpty(result.sourceSystem) && result.sourceSystem != "NCNU_MEM") && (!string.IsNullOrEmpty(party.roleType) && (party.roleType.ToUpper() == "NAMEDINSURED" || party.roleType.ToUpper() == "NAMED INSURED" || party.roleType.ToUpper() == "INSURED")))
                                            select (new SearchResultEntity
                                            {
                                                SourceSystem = (result.sourceSystem.ToUpper() == "MAIG_HOME") ? "KIC" : (result.sourceSystem.ToUpper() == "PUP") ? "PUPSYS" : (result.sourceSystem.ToUpper() == "MAIG_AUTO") ? "KIC" : result.sourceSystem,
                                                policyNumber = string.IsNullOrEmpty(result.policyNumber) ? string.Empty : (result.sourceSystem.ToUpper() == "MAIG_AUTO") ? result.policyNumber.Substring(1) : (result.sourceSystem.ToUpper() == "SIS" && result.policyNumber.StartsWith(Config.Setting("SISpolicyPrefix"))) ? result.policyNumber.Substring(2) : (result.sourceSystem == "SIS") ? result.policyNumber.Substring(3) : result.policyNumber,
                                                policyStatus = string.IsNullOrEmpty(result.policyStatus) ? string.Empty : result.policyStatus,
                                                //Added logic for HOME : if prodTypeCode:"DP" & productCode:"DP3" & sourceSystem:"COGEN"
                                                productCode = string.IsNullOrEmpty(result.prodTypeCode) ? string.Empty : ((result.prodTypeCode.ToUpper() == "HO" && (string.IsNullOrEmpty(result.productCode) || result.productCode.ToUpper() != "DP3")) || (result.prodTypeCode.ToUpper().Equals("DP") && (!string.IsNullOrEmpty(result.productCode) && result.productCode.ToUpper().Equals("DP3")) && result.sourceSystem.ToUpper().Equals("MAIG_HOME"))) ? "Home" : (result.prodTypeCode.ToUpper() == "AU" || result.prodTypeCode.ToUpper() == "PA") ? "Auto" : ((result.prodTypeCode.ToUpper() == "SPCL" && !string.IsNullOrEmpty(result.productCode) && result.productCode.ToUpper() == "MC") || result.prodTypeCode.ToUpper() == "MC") ? "Motorcycle" : ((result.prodTypeCode.ToUpper() == "SPCL" && !string.IsNullOrEmpty(result.productCode) && result.productCode.ToUpper() == "PWC") || result.prodTypeCode.ToUpper() == "WC") ? "Watercraft" : ((result.prodTypeCode.ToUpper() == "HO" && !string.IsNullOrEmpty(result.productCode) && result.productCode.ToUpper() == "DP3") || result.prodTypeCode.ToUpper() == "DF") ? "Dwelling Fire" : (result.prodTypeCode.ToUpper() == "PUP" || result.prodTypeCode.ToUpper() == "PU") ? "Personal Umbrella" : result.prodTypeCode,
                                                NamedInsured = FirstCharToUpper(((string.IsNullOrEmpty(cons.firstName)) ? string.Empty : cons.firstName)) +
                                                                  "  " + FirstCharToUpper(((string.IsNullOrEmpty(cons.lastName)) ? string.Empty : cons.lastName)),
                                                address = ((cons.partyAddress != null) ? GetAddress(cons.partyAddress) : "")
                                            })).Distinct().OrderBy(SearchEntity => SearchEntity.policyNumber, new SemiNumericComparer()).ToList();
                            //CHG0116140 - END - CH1 - Added FirstCharToUpper function to modify 1st Letter of string as Upper Case
                            //CHG0115410 - END - CH2 - Modified the query with respect to the new response generated from Upgraded Customer Search API.

                            ViewState.Add("SearchResult", SearchResult);
                            NameSearchResults.DataSource = SearchResult;
                            //Keeps in Session object for maintaining pagination
                            //Session["NameSearchResult"] = SearchResult;
                        }
                        //Binds the search result in Grid
                        NameSearchResults.DataBind();
                        //if (searchResponse.responseJson.totalHits == 0)
                        //{
                        //    NameSearchResults.EmptyDataText = "There is no records for the given search criteria";
                        //}
                        lblCustomerNameSearch.Visible = true;
                    }

                }
                catch (Exception ex)
                {
                    lblerrorMessage.Visible = true;
                    lblerrorMessage.Text = CSAAWeb.Constants.ELASTIC_SEARCH_ERROR_MESSAGE;
                    Logger.Log("There is an exception in Namesearch - PopulateGrid : " + ex.ToString());
                    //// MAIG - Begin - 11122014
                    NameSearchResults.Visible = false;
                    //// MAIG - End - 11122014

                }
            }
        }
        //CHG0116140 - CH1 - BEGIN - Added FirstCharToUpper function to modify 1st Letter of string as Upper Case
        /// <summary>
        /// To modify 1st letter of string to Upper case
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string FirstCharToUpper(string input)
        {
            if (String.IsNullOrEmpty(input))
                return string.Empty;
            return input.First().ToString().ToUpper() + input.Substring(1);
        }
        //CHG0116140 - CH1 - END - Added FirstCharToUpper function to modify 1st Letter of string as Upper Case

        //CHG0115410 - BEGIN - CH3 - Modified the GetAddresss function to pass partyAddress as new array parameter for fetching customer address information from response.
        /// <summary>
        /// Get Address - retrieve party address and validate billing address. 
        /// If billing address is available then we will display. 
        /// If not then we will validate and display mailing Address. 
        /// If not then we will validate and display home Address. 
        /// If home Address is not available then display empty. 
        /// we call this method from populateGrid - Name Search.
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        private string GetAddress(Partyaddress[] address)
        {
            string resultAddress = string.Empty;
            //CHG0116140 - CH2 - BEGIN - Modified SingleOrDefault to LastorDefault to fetch the last address from the response
            Partyaddress billingAddress = address.Where(getAddress => getAddress.addressType.ToLower().Equals("billing")).Select(result => result).LastOrDefault();
            if (billingAddress == null)
            {
                Partyaddress mailingAddress = address.Where(getAddress => getAddress.addressType.ToLower().Equals("mailing")).Select(result => result).LastOrDefault();
                if (mailingAddress == null)
                {
                    Partyaddress homeAddress = address.Where(getAddress => getAddress.addressType.ToLower().Equals("home")).Select(result => result).LastOrDefault();
                    //CHG0116140 - CH2 - END - Modified SingleOrDefault to LastorDefault to fetch the last address from the response
                    if (homeAddress != null)
                    {
                        resultAddress = String.Format("{0}, {1}, {2},{3}",
                        string.IsNullOrEmpty(homeAddress.address1) ? string.Empty : homeAddress.address1,
                        string.IsNullOrEmpty(homeAddress.city) ? string.Empty : homeAddress.city,
                        string.IsNullOrEmpty(homeAddress.state) ? string.Empty : homeAddress.state,
                        string.IsNullOrEmpty(homeAddress.zipCode) ? string.Empty : homeAddress.zipCode);
                    }
                }
                else
                {
                    resultAddress = String.Format("{0}, {1}, {2},{3}",
                    string.IsNullOrEmpty(mailingAddress.address1) ? string.Empty : mailingAddress.address1,
                    string.IsNullOrEmpty(mailingAddress.city) ? string.Empty : mailingAddress.city,
                    string.IsNullOrEmpty(mailingAddress.state) ? string.Empty : mailingAddress.state,
                    string.IsNullOrEmpty(mailingAddress.zipCode) ? string.Empty : mailingAddress.zipCode);
                }
            }
            else
            {
                resultAddress = String.Format("{0}, {1}, {2},{3}",
                    string.IsNullOrEmpty(billingAddress.address1) ? string.Empty : billingAddress.address1,
                    string.IsNullOrEmpty(billingAddress.city) ? string.Empty : billingAddress.city,
                    string.IsNullOrEmpty(billingAddress.state) ? string.Empty : billingAddress.state,
                    string.IsNullOrEmpty(billingAddress.zipCode) ? string.Empty : billingAddress.zipCode);
            }
            return resultAddress;
        }
        //CHG0115410 - END - CH3 - Modified the GetAddresss function to pass partyAddress as new array parameter for fetching customer address information from response.

        //private IOrderedEnumerable<SearchResultEntity> SortPolicyNumber(SearchResultEntity result)
        //{
        //    foreach (var item in result)
        //    {

        //    }
        //}
        #endregion

        #region events
        /// <summary>
        /// On mailing Zip text change, the Policy number and Product Type is disabled.
        /// </summary>
        /// <param name="sender">Sender of the event</param>
        /// <param name="e">Event argument</param>
        protected void _MailingZip_TextChanged(object sender, EventArgs e)
        {
            drpproductType.Enabled = false;
            drpproductType.SelectedValue = "0";
            policyNumber.Enabled = false;
            policyNumber.Text = "";
            hiddenData.Text = string.Empty;
        }
        /// <summary>
        /// On Last name text change, the Policy number and Product Type is disabled.
        /// </summary>
        /// <param name="sender">Sender of the event</param>
        /// <param name="e">Event argument</param>
        protected void _LastName_TextChanged(object sender, EventArgs e)
        {
            drpproductType.Enabled = false;
            drpproductType.SelectedValue = "0";
            policyNumber.Enabled = false;
            policyNumber.Text = "";
            hiddenData.Text = string.Empty;
        }
        /// <summary>
        /// When any one of the radio button with the policy details is selected. 
        /// </summary>
        /// <param name="sender">Sender of the event</param>
        /// <param name="e">Event argument</param>
        protected void RbtnSelect_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                RadioButton selectButton = (RadioButton)sender;
                GridViewRow row = (GridViewRow)selectButton.Parent.Parent;
                int gridRowIndex = row.RowIndex;
                string txtProductType = string.Empty;
                string txtPolicyNumber = string.Empty;
                string txtSrcSystem = string.Empty;
                RadioButton radiobutton;
                foreach (GridViewRow gridviewrow in NameSearchResults.Rows)
                {
                    radiobutton = gridviewrow.FindControl("rbtnSelect") as RadioButton;
                    if (selectButton.Checked)
                    {
                        if (gridviewrow.RowIndex != gridRowIndex)
                        {

                            radiobutton.Checked = false;
                        }
                        else
                        {
                            txtProductType = gridviewrow.Cells[3].Text;
                            txtSrcSystem = gridviewrow.Cells[2].Text;
                            radiobutton = gridviewrow.FindControl("rbtnSelect") as RadioButton;
                            txtPolicyNumber = gridviewrow.Cells[1].Text;
                            if (Page.Request.AppRelativeCurrentExecutionFilePath.Contains("Insurance.aspx"))
                            {
                                policyNumber.Enabled = false;
                            }
                        }
                    }
                }
                //DropDownList productType = (DropDownList)this.Parent.FindControl("_ProductType");
                //TextBox policyNumber=(TextBox)this.Parent.FindControl("_Policy").FindControl("_Policy");
                //TextBox hiddenData = (TextBox)this.Parent.FindControl("HiddenNameSearch");

                //productType.SelectedItem.Text = txtProductType;
                if (Page.Request.AppRelativeCurrentExecutionFilePath.Contains("Insurance.aspx"))
                {
                    policyNumber.Text = txtPolicyNumber;
                    SkipParentValidation.Value = "false";
                }
                else
                {
                    TextBox policy = (TextBox)this.Parent.FindControl("_PolicyNumber");
                    policy.Text = txtPolicyNumber;
                }
                string productCode = string.Empty;
                switch (txtProductType.ToLower())
                {
                    case "auto":
                        productCode = MSC.SiteTemplate.ProductCodes.PA.ToString();
                        break;
                    case "home":
                        productCode = MSC.SiteTemplate.ProductCodes.HO.ToString();
                        break;
                    case "motorcycle":
                        productCode = MSC.SiteTemplate.ProductCodes.MC.ToString();
                        break;
                    case "watercraft":
                        productCode = MSC.SiteTemplate.ProductCodes.WC.ToString();
                        break;
                    case "personal umbrella":
                        productCode = MSC.SiteTemplate.ProductCodes.PU.ToString();
                        break;
                    case "dwelling fire":
                        productCode = MSC.SiteTemplate.ProductCodes.DF.ToString();
                        break;
                }
                var itm = drpproductType.Items.FindByValue(productCode);
                int itemIndex = drpproductType.Items.IndexOf(itm);
                if (itemIndex >= 0)
                {
                    drpproductType.SelectedIndex = itemIndex;
                }
                if (Page.Request.AppRelativeCurrentExecutionFilePath.Contains("Insurance.aspx"))
                {
                    hiddenData.Text = productCode + "|" + txtPolicyNumber + "|" + gridRowIndex + "|" + NameSearchResults.PageIndex;
                }
                else
                {   
                    //CHG0131014 - OTSP/SUBRO/PRB0047380/SECURITY FIXES - Added Placholder for OTSP changes - Start
                    if (((PlaceHolder)this.Parent.FindControl("policySearchDetails") != null))
                    {
                        PlaceHolder policyGrid = (PlaceHolder)this.Parent.FindControl("policySearchDetails");
                        policyGrid.Visible = false;
                    }
                    //CHG0131014 - OTSP/SUBRO/PRB0047380/SECURITY FIXES - Including Placeholder of ScheduledPayments.aspx
                    else if (((PlaceHolder)this.Parent.FindControl("ScheduledRecurring") != null))
                    {
                        PlaceHolder policyGrid = (PlaceHolder)this.Parent.FindControl("ScheduledRecurring");
                        policyGrid.Visible = false;
                    }
                    //CHG0131014 - OTSP/SUBRO/PRB0047380/SECURITY FIXES - Added Placholder for OTSP changes - End
                    // PlaceHolder policyGrid = (PlaceHolder)this.Parent.FindControl("policySearchDetails");
                    //policyGrid.Visible = false;
                }
                TextBox DuplicatePolicy = (TextBox)this.Parent.FindControl("HiddenSelectedDuplicatePolicy");
                DuplicatePolicy.Text = txtSrcSystem;
                LnkSearchByCustomerName.Enabled = false;
                // SkipParentValidation.Value = "false";

                //NameSearchResults.FooterRow.Cells[0].Text = "Showing Pages " + (NameSearchResults.PageIndex + 1) + " of " + NameSearchResults.PageCount;

                //// Setting first cell to occupy the entire width
                //NameSearchResults.FooterRow.Cells[0].ColumnSpan = NameSearchResults.FooterRow.Cells.Count;
                // PlaceHolder policyGrid = (PlaceHolder)this.Parent.FindControl("policySearchDetails");
                //// Removing all the cells except the first cell in the footer
                //for (int i = 1; i < NameSearchResults.FooterRow.Cells.Count; )
                //{
                //    NameSearchResults.FooterRow.Cells.RemoveAt(i);
                //}
            }
            catch (Exception ex)
            {
                Logger.Log("Exception in Radio Button Select" + ex.ToString());
            }



        }
        /// <summary>
        /// Serach By Customer Name Link button click
        /// </summary>
        /// <param name="sender">Sender of the event</param>
        /// <param name="e">Event argument</param>
        protected void LnkSearchByCustomerName_Click(object sender, EventArgs e)
        {
            try
            {
                if (pnlCustomerNameMain.Visible == false)
                {
                    lblCustomerNameSearch.Visible = false;
                    _LastName.ForeColor = System.Drawing.Color.Black;
                    _MailingZip.ForeColor = System.Drawing.Color.Black;
                    productType.IsValid = true;
                    revenueType.IsValid = true;
                    policy.IsValid = true;
                    hiddenData.Text = "";
                    SearchResult = null;
                    NameSearchResults.DataSource = null;
                    NameSearchResults.DataBind();
                    _FirstName.Text = string.Empty;
                    _LastName.Text = string.Empty;
                    _MailingZip.Text = string.Empty;
                    pnlCustomerNameMain.Visible = true;
                    drpproductType.Enabled = false;
                    drpproductType.SelectedValue = "0";
                    policyNumber.Enabled = false;
                    policyNumber.Text = "";
                    this.Parent.Parent.FindControl("PageValidator2").Visible = false;
                    lblerrorMessage.Visible = false;
                    Results.Visible = false;
                }
                else
                {
                    lblCustomerNameSearch.Visible = false;
                    productType.IsValid = true;
                    revenueType.IsValid = true;
                    policy.IsValid = true;
                    pnlCustomerNameMain.Visible = false;
                    //TextBox hiddenData = (TextBox)this.Parent.FindControl("HiddenNameSearch");
                    hiddenData.Text = "";
                    SearchResult = null;
                    NameSearchResults.DataSource = null;
                    NameSearchResults.DataBind();
                    _FirstName.Text = string.Empty;
                    _LastName.Text = string.Empty;
                    _MailingZip.Text = string.Empty;
                    //vldLastNameReq.IsValid = true;
                    //vldMailingZipCheck.IsValid = true;
                    //DropDownList drpproductType = (DropDownList)this.Parent.FindControl("_ProductType");
                    drpproductType.Enabled = true;
                    drpproductType.SelectedValue = "0";
                    //TextBox policyNumber = (TextBox)this.Parent.FindControl("_Policy").FindControl("_Policy");
                    policyNumber.Enabled = true;
                    policyNumber.Text = "";
                    lblerrorMessage.Visible = false;
                    this.Parent.Parent.FindControl("PageValidator2").Visible = false;
                    SkipParentValidation.Value = "false";
                    //LastName.IsValid = true;
                    //MailingZip.IsValid = true;
                    //LastNameCheck.IsValid = true;
                    //MailingZipCheck.IsValid = true;
                }
            }
            catch (Exception ex)
            {
                Logger.Log("Exception occured upon Customer Name search link button click : " + ex.ToString());
            }
        }
        /// <summary>
        /// Displays the footer of the grid
        /// </summary>
        /// <param name="sender">Sender of the event</param>
        /// <param name="e">Event argument</param>
        protected void NameSearchResults_RowCreated(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.Pager)
                {
                    NameSearchResults.PagerSettings.Mode = PagerButtons.NumericFirstLast;
                    NameSearchResults.PagerSettings.FirstPageText = "<<";
                    NameSearchResults.PagerSettings.LastPageText = ">>";
                    NameSearchResults.PagerSettings.PreviousPageText = "<";
                    NameSearchResults.PagerSettings.NextPageText = ">";

                    Label control = new Label(); //new Label control
                    control.Text = "Showing Page " + (NameSearchResults.PageIndex + 1) + " of " + NameSearchResults.PageCount; // add text

                    Table table = e.Row.Cells[0].Controls[0] as Table;  // get the pager table
                    table.Attributes.Add("Class", "arial_12");
                    table.Width = Unit.Percentage(100); //apply width style
                    TableCell newCell = new TableCell(); //Create new cell
                    newCell.Attributes.Add("align", "right"); // apply align right
                    newCell.Attributes.Add("Class", "arial_12");
                    newCell.Controls.Add(control);  //Add contol
                    table.Rows[0].Cells.Add(newCell); //add cell

                }
            }
            catch (Exception ex)
            {
                Logger.Log("Exception occured upon NameSearchResults_RowCreated method : " + ex.ToString());
            }
        }
        /// <summary>
        /// Displays when the user selects the Next page. 
        /// </summary>
        /// <param name="sender">Sender of the event</param>
        /// <param name="e">Event argument</param>
        protected void NameSearchResults_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //if (Session["NameSearchResult"] != null)
            //{
            if (Page.Request.AppRelativeCurrentExecutionFilePath.Contains("Insurance.aspx"))
            {
                policyNumber.Enabled = false;
            }
            NameSearchResults.PageIndex = e.NewPageIndex;
            NameSearchResults.DataSource = ViewState["SearchResult"];
            NameSearchResults.DataBind();
            //}
        }


        #endregion

    }
}
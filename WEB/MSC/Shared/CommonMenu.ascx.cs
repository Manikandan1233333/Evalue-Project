/* Created this page on 01/09/2014 By Cognizant as part of MAIG Project 
 * Version 1.0
 * CHG0131014 - OTSP/SUBRO/PRB0047380/SECURITY FIXES - Updated code to fix the IP address issue for external URL and Defect 210 HttpOnly to true
 */
using System;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;
using System.Management;
using System.Configuration;
using System.Web;
using CSAAWeb;

namespace MSC.Shared
{
    public partial class CommonMenu : System.Web.UI.UserControl
    {
        string[] indexvalue = new string[6];
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //CHG0131014 - OTSP/SUBRO/PRB0047380/SECURITY FIXES - IP Address fix for external URL - Start
                if (Config.Setting("GetIP").Equals("1"))
                {
                    if (Request.Cookies[".ASPXFORMSAUTH"] != null)
                    {
                        string authCookie = Request.Cookies[".ASPXFORMSAUTH"].Value;
                        string userInfoCookie = Request.Cookies["ASP.NET_Cookie"] != null ? Request.Cookies["ASP.NET_Cookie"].Value : string.Empty;
                        string RemoteKey = HttpContext.Current.Request.ServerVariables[Convert.ToString(ConfigurationManager.AppSettings["GetServerIP"])];

                        var ip = RemoteKey;
                        if (ip != null)
                        {
                            if (ip.Contains(","))
                                ip = ip.Split(',').First().Trim();
                        }

                        if (!string.IsNullOrEmpty(userInfoCookie))
                        {
                            //string[] UserData = ((System.Web.Security.FormsIdentity)(this.Page.User.Identity)).Ticket.UserData.Split(';');
                            string decrpytCookie = CSAAWeb.Cryptor.Decrypt(userInfoCookie, Convert.ToString(ConfigurationManager.AppSettings["CryptorKey"]));
                            string[] validCookie = decrpytCookie.Split('&');
                            if (authCookie != validCookie[0] || ip != validCookie[1])
                            {
                                Response.Redirect("/PaymentToolmscr/Forms/logout.aspx");
                            }
                        }
                        else
                        {
                            Response.Redirect("/PaymentToolmscr/Forms/logout.aspx");
                        }
                    }
                }
                //CHG0131014 - OTSP/SUBRO/PRB0047380/SECURITY FIXES - IP Address fix for external URL - End
                /* if (!Page.IsPostBack)
                 {
                     string[] UserData = ((System.Web.Security.FormsIdentity)(this.Page.User.Identity)).Ticket.UserData.Split(';');
                     if (UserData.Length > 0)
                     {
                         if (UserData[0].Contains("PSS"))
                         {
                             string LocationID = UserData[UserData.Length - 3];
                             Page.ClientScript.RegisterStartupScript(this.GetType(), "UpdateLocation", "SetLocationID(" + LocationID + ");", true);
                         }
                     }
                 }*/

                aspMenu.Items.Clear();
                CSAAWeb.Navigation.MenuData menu = new CSAAWeb.Navigation.MenuData();
                string sInputText = menu.CommonMenuGenerate();
                if (CSAAWeb.Config.Setting("Logging.Menu").Equals("1"))
                {
                    if (Request.Cookies["LoggedMSC"] == null)
                    {
                        CSAAWeb.AppLogger.Logger.Log("Menu retrieved for the User: " + Page.User.Identity.Name + " Details: " + sInputText);
                        Response.Cookies.Add(new System.Web.HttpCookie("LoggedMSC", "true"));
                        //CHG0131014 - OTSP/SUBRO/PRB0047380/SECURITY FIXES - Defect 210 - Set HttpOnly for Cookie - Start
                        Response.Cookies["LoggedMSC"].HttpOnly = true;
                        //CHG0131014 - OTSP/SUBRO/PRB0047380/SECURITY FIXES - Defect 210 - Set HttpOnly for Cookie - End
                    }
                }
                int iStartIndex = sInputText.IndexOf('[') + 2;
                int iEndIndex = sInputText.LastIndexOf(']') - 1;
                string sTableString = sInputText.Substring(iStartIndex, iEndIndex - iStartIndex);
                sTableString = sTableString.Replace(",,", ",'',");
                sTableString = sTableString.Replace("\\r\\n", "");
                string[] sSeparatorArray = { "],[" };
                string[] sRowStrings = sTableString.Split(sSeparatorArray, StringSplitOptions.None);
                DataTable dtMenuTable = new DataTable();
                dtMenuTable.Columns.AddRange(new DataColumn[] {
                                                                    new DataColumn("Menu_ID"),
                                                                    new DataColumn("Depth"),
                                                                    new DataColumn("Menu_Text"),
                                                                    new DataColumn("URL"),
                                                                    new DataColumn("Sort_Order",System.Type.GetType("System.Int32")),
                                                                    new DataColumn("ACL"),
                                                                    new DataColumn("Enabled"),
                                                                    new DataColumn("Parent_ID")
                                                              }
                                            );
                foreach (string row in sRowStrings)
                {
                    string[] sColumnValues = new string[8] { "", "", "", "", "", "", "", "" };
                    int iElementPosition = 0;
                    int iStartPosition = 0;
                    int iEndPosition = 0;
                    string sRowString = row;
                    sRowString = sRowString.Substring(iStartPosition);
                    while (!sRowString.Equals(string.Empty))
                    {
                        if (sRowString.StartsWith("'"))
                        {
                            iEndPosition = sRowString.IndexOf("'", 1);
                            if ((iEndPosition > 0) && (sRowString.Substring(iEndPosition - 1, 1) == "\\"))
                            {
                                iEndPosition = sRowString.IndexOf("'", iEndPosition + 1);
                            }
                            if (iEndPosition != -1)
                            {
                                sColumnValues[iElementPosition++] = sRowString.Substring(0, iEndPosition).Trim(new char[] { '\'' });
                                sColumnValues[iElementPosition - 1] = sColumnValues[iElementPosition - 1].Replace("\\'", "'");
                                iStartPosition = iEndPosition + 2;
                            }
                            else
                            {
                                iStartPosition = 0;
                            }
                            sRowString = sRowString.Substring(iStartPosition);
                        }
                        else
                        {
                            iEndPosition = sRowString.IndexOf(',');
                            if (iEndPosition != -1)
                            {
                                sColumnValues[iElementPosition++] = sRowString.Substring(0, iEndPosition);
                                iStartPosition = iEndPosition + 1;
                                sRowString = sRowString.Substring(iStartPosition);
                            }
                            else
                            {
                                sColumnValues[iElementPosition] = sRowString.Substring(0);
                                iStartPosition = 0;
                                sRowString = string.Empty;
                            }
                        }
                    }
                    dtMenuTable.Rows.Add(sColumnValues);
                }

                DataRow[] drParentMenuItems = dtMenuTable.Select("Parent_ID = 0", "Sort_Order");
                foreach (DataRow row in drParentMenuItems.AsEnumerable())
                {
                    MenuItem parentMenuItem = new MenuItem(row.Field<string>("Menu_Text"), row.Field<string>("Menu_ID").ToString(),
                        "", row.Field<string>("URL"));
                    parentMenuItem.Selectable = false;
                    aspMenu.Items.Add(parentMenuItem);
                }
                //Modified on 08212014 - Start

                aspMenu.Items.Add(new MenuItem("", "", ""));
                /*if (!drParentMenuItems.Any())
                {
                    aspMenu.Items.Add(new MenuItem("", "", ""));
                }*/
                //Commented on 08212014 - End
                DataRow[] drChildMenuItems = dtMenuTable.Select("Parent_ID <> 0", "Sort_Order");
                foreach (DataRow row in drChildMenuItems.AsEnumerable())
                {
                    MenuItem childMenuItem = null;
                    if (row.Field<string>("Menu_Text").Equals("Down Payment"))
                    {
                        childMenuItem = new MenuItem(row.Field<string>("Menu_Text"), row.Field<string>("Menu_ID").ToString(),
                                              "", row.Field<string>("URL"));// + "?IsDown=" + Server.UrlEncode(Encrypt("true")));
                    }
                    else if (row.Field<string>("Menu_Text").Equals("Installment Payment"))
                    {
                        childMenuItem = new MenuItem(row.Field<string>("Menu_Text"), row.Field<string>("Menu_ID").ToString(),
                                               "", row.Field<string>("URL"));// + "?IsDown=" + Server.UrlEncode(Encrypt("false")));
                    }
                    else
                    {
                        childMenuItem = new MenuItem(row.Field<string>("Menu_Text"), row.Field<string>("Menu_ID").ToString(),
                           "", row.Field<string>("URL"));
                    }
                    aspMenu.FindItem(row["Parent_ID"].ToString()).ChildItems.Add(childMenuItem);
                }
                for (int i = 0; i < aspMenu.Items.Count; i++)
                {
                    if (aspMenu.Items[i].ChildItems.Count == 0)
                    {
                        aspMenu.Items.RemoveAt(i--);
                    }
                    //Commented on 08/21/2014 START - Not used since Parent Menu highligting is populated by Jquery
                    /*else
                    {
                        for (int j = 0; j < aspMenu.Items[i].ChildItems.Count; j++)
                        {
                            //aspMenu.Items[i].ChildItems[j].NavigateUrl += "?Index=" + i;
                            indexvalue[i] += aspMenu.Items[i].ChildItems[j].NavigateUrl;
                        }
                    }*/
                    //Commented on 08/21/2014 END - Not used since Parent Menu highligting is populated by Jquery
                }
                //Session["ind"] = indexvalue;
            }
            catch (Exception ex)
            {
                CSAAWeb.AppLogger.Logger.Log(ex);
            }
        }

        protected void aspMenu_MenuItemClick(object sender, MenuEventArgs e)
        {
            Context.Items.Add("PaymentMode", e.Item.Text);
        }

        #region Private Methods
        /*private static string Encrypt(string val)
        {
            var bytes = System.Text.Encoding.UTF8.GetBytes(val);
            var encBytes = System.Security.Cryptography.ProtectedData.Protect(bytes, new byte[0], System.Security.Cryptography.DataProtectionScope.LocalMachine);
            return Convert.ToBase64String(encBytes);
        }*/
        #endregion
    }
}
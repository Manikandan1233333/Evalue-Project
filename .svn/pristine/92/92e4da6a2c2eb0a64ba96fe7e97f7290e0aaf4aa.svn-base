/*	CREATION HISTORY:
 *	CREATED BY COGNIZANT
 *	01/04/2013 -  TO LOG THE CODE FLOW DETAILS FOR LIVE WEB SERVICE AS PART OF PAYMENT CENTRAL PHASE II
 *		
 *
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace PaymentIDPProxyService
{
    public class Logging
    {
        private static string _LogPath=null;
        public const string strThreadAbort = "System.Threading.Thread.Abort";
        
		public Logging()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		private string logger()
		{
			string strSiteRootPath = HttpRuntime.AppDomainAppPath.ToLower();
			if(!Directory.Exists(strSiteRootPath + "Logs"))
				Directory.CreateDirectory(strSiteRootPath + "Logs");
			string strSiteLogFile = strSiteRootPath + "Logs\\Log" + DateTime.Now.ToString("yyyyMMdd") + ".txt";
			if(!File.Exists(strSiteLogFile))
			{
				StreamWriter sw = new StreamWriter(strSiteLogFile);
				sw.Close();
			}
			_LogPath = strSiteLogFile;
			return _LogPath;
		}
        //CHG 0055956.CH1 - Start
		public void WriteLog(string Message)
		{
            if (!Message.Contains(strThreadAbort))
            {
			StreamWriter sw=null;
			try 
			{
				string Path = logger();
				sw = new StreamWriter(new FileStream(Path, FileMode.Append, FileAccess.Write,FileShare.ReadWrite));
                sw.WriteLine(DateTime.Now.ToString() + ":" + Message.ToString() );
               // sw.WriteLine(" " + Message.ToString());
                sw.WriteLine("-----------------------------------------"); 
			} 
			finally 
			{
				try {sw.Close();} 
				catch(Exception) {}
			}
                //CHG 0055956.CH1 - End
		}
		}
        //public void WriteLog(string Message,string Uid)
        //{
        //    if (!Message.Contains(strThreadAbort))
        //    {
        //    StreamWriter sw=null;
        //    try 
        //    {
        //        string Path = logger();
        //        sw = new StreamWriter(new FileStream(Path, FileMode.Append, FileAccess.Write,FileShare.ReadWrite));
        //        sw.WriteLine(DateTime.Now.ToString() +"- Logged in Agent- " + Uid + ":" + HttpContext.Current.Request.Url.Host.ToString() + ":" + Message);
        //    } 

        //    finally 
        //    {
        //        try {sw.Close();} 
        //        catch(Exception) {}
        //    }
        //}
	}

    }


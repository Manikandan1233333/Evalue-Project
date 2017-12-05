/*MODIFIED BY COGNIZANT AS PART OF PT MAIG Changes
 * This page has been newly created for PT MAIG Changes.
 * Version 1.0
 * Created Date : 09/20/2014
 */
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Xml;
using OrderClassesII.ReportService2010;
using OrderClassesII.ReportExecution2005;
using System.Configuration;
using System.Text;
using System.Linq;
using OrderClassesII;
using CSAAWeb;
using System.Security.Cryptography;


namespace OrderClassesII
{
    /// <summary>
    /// Helper class that invokes the SSRS Reporting Server
    /// </summary>
    public class ReportGeneratorUtilHelper
    {
        public enum ReportGeneratorReportStatus
        {
            Success,
            Skipped,
            Failure
        }

        OrderClassesII.ReportExecution2005.ExecutionInfo executionInfo;
        OrderClassesII.ReportExecution2005.ReportExecutionService res;
        ReportingService2010 rs;

        /// <summary>
        /// Constructor for the class. Sets the credentials and declares the report server information
        /// </summary>
        public ReportGeneratorUtilHelper()
        {
            //XmlConfigurator.Configure();
            executionInfo = new OrderClassesII.ReportExecution2005.ExecutionInfo();
            res = new OrderClassesII.ReportExecution2005.ReportExecutionService();
            rs = new ReportingService2010();

                res.Credentials = System.Net.CredentialCache.DefaultCredentials;
                rs.Credentials = System.Net.CredentialCache.DefaultCredentials;
            
        }

        /// <summary>
        /// This is what will generate the reports. It takes in a list of ReportDetails and will parse the parameters out of the information for the report.
        /// It then iterates over the reports and delivers them according to the distribution type that was set. The centrralPrintDistributionLists is used to 
        /// track the CentralPrint distribution types specified so that it can batch the files created that are targeted and then finally after all the logic
        /// is done output the information into an xml file.
        /// </summary>
        /// <param name="reports">This is a list of Reports passed in.</param>
        public byte[] GenerateAndExecuteDelivery(List<ReportDetail> reports)
        {
            //Dictionary<string, List<string>> centralPrintDistributionLists = new Dictionary<string, List<string>>();
            //WriteLogInfo("Request & Report configuration - validation completed, Generating SSRS reports started.", outputFileName);
            byte[] renderResult = null;
            foreach (ReportDetail report in reports)
            {
                string fullReportPath = report.ReportPath + report.ReportName;
                //ThreadContext.Properties["StackTrace"] = string.Empty;
                DateTime startTime = DateTime.Now;
                try
                {
                    if (string.IsNullOrEmpty(report.ReportDetailIdentifier) || string.IsNullOrEmpty(report.ReportName))
                    {
                        return null;
                    }
                    var reportTimeout = Convert.ToInt32(Config.Setting("ReportServiceTimeOut"));
                    res.Timeout = reportTimeout;

                    executionInfo = res.LoadReport(fullReportPath, null);

                    //creates the list of parameters to be used
                    OrderClassesII.ReportExecution2005.ParameterValue[] parameters = new OrderClassesII.ReportExecution2005.ParameterValue[report.Parameters.Count];
                    int parametersTracker = 0;
                    foreach (OrderClassesII.ReportParameter parameter in report.Parameters)
                    {
                        parameters[parametersTracker] = new OrderClassesII.ReportExecution2005.ParameterValue();
                        parameters[parametersTracker].Name = parameter.Name;
                        if (parameter.Type.Equals("DateTime"))
                        {
                            parameters[parametersTracker].Value = Convert.ToDateTime(parameter.Value).ToString();
                        }
                        else if (parameter.Type.Equals("String"))
                        {
                            parameters[parametersTracker].Value = parameter.Value;
                        }
                        parameters[parametersTracker].Value = parameter.Value;
                        parametersTracker++;
                    }
                    res.SetExecutionParameters(parameters, null);

                    foreach (DistributionDetail distributionDetail in report.Distributions)
                    {
                        string format = distributionDetail.OutputFileType.ToLower();
                        if (distributionDetail.OutputFileType.Trim().ToLower().Equals("png"))
                        {
                            format = "image";
                        }
                        string deviceInfo = null;
                        string extension = distributionDetail.OutputFileType.ToLower();
                        string mimeType = "";
                        string encoding = "";
                        OrderClassesII.ReportExecution2005.Warning[] warnings = null;
                        string[] streamIDs = null;

                        mimeType = GetMimeType(distributionDetail.OutputFileType);

                        renderResult = res.Render(format, deviceInfo, out extension, out mimeType, out encoding, out warnings, out streamIDs);

                        if (warnings != null)
                        {
                            StringBuilder errorMessage = new StringBuilder();
                            foreach (var warning in warnings)
                            {
                                errorMessage.Append(warning.Message);
                            }
                            throw new Exception(errorMessage.ToString());
                        }

                        string fileName = distributionDetail.FileName;
                        fileName += GetFileExtension(distributionDetail.OutputFileType);

                        return renderResult;
                        
                    }

                    string timeDifference = DateTime.Now.Subtract(startTime).ToString();
                }
                catch (Exception ex)
                {
                    string timeDifference = DateTime.Now.Subtract(startTime).ToString();
                    string errorMessage = string.Format("The report {0}, failed to generate files. Error Message: {1}", report.ReportName, ex.Message);
                }
            }
            return renderResult;
        }



        /// <summary>
        /// This will take in the output file type provided and return the mime type to be associated with that file type
        /// </summary>
        /// <param name="outputFileType">This is the output file type specified for the report. It'll be in the form of PDF, PNG, Image, etc.</param>
        /// <returns>This returns a string that represents the mime type to be used for the report generated.</returns>
        public string GetMimeType(string outputFileType)
        {
            string loweredOutputFileType = outputFileType.ToLower();
            string mimeType = "";

            //configures the MIME type prior to rendering
            if (loweredOutputFileType.Equals("pdf"))
            {
                mimeType = "application/pdf";
            }
            else if (loweredOutputFileType.Equals("csv"))
            {
                mimeType = "text/csv";
            }
            else if (loweredOutputFileType.Equals("image")
                || loweredOutputFileType.Equals("png"))
            {
                mimeType = "image/png";
            }
            else if (loweredOutputFileType.Equals("html4.0"))
            {
                mimeType = "text/html";
            }
            else if (loweredOutputFileType.Equals("xml"))
            {
                mimeType = "text/xml";
            }
            else if (loweredOutputFileType.Equals("excel"))
            {
                mimeType = "application/vnd.ms-excel";
            }
            else if (loweredOutputFileType.Equals("word"))
            {
                mimeType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
            }
            //else if (loweredOutputFileType.Equals("excelopenxml"))
            //{
            //    mimeType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            //}
            return mimeType;
        }

        /// <summary>
        /// This takes in the output file type specified, and then returns the file type that the file will be labelled with.
        /// The file extension will always return with a period in the front of it.
        /// </summary>
        /// <param name="outputFileType">This is the output file type specified</param>
        /// <returns>Returns a string that represents the output file type</returns>
        public string GetFileExtension(string outputFileType)
        {
            string fileExtenstion = string.Empty;
            string loweredOutputFileType = outputFileType.ToLower();

            if (loweredOutputFileType.Equals("excel"))
            {
                fileExtenstion += ".xls";
            }
            else if (loweredOutputFileType.Equals("word"))
            {
                fileExtenstion += ".doc";
            }
            else if (loweredOutputFileType.Equals("excelopenxml"))
            {
                fileExtenstion += ".xlsx";
            }
            else
            {
                fileExtenstion += "." + loweredOutputFileType;
            }

            return fileExtenstion;
        }
        #region TripleDES Encryption/Decryption
        /// <summary>
        /// Method to encrypt the string using Triple DES
        /// </summary>
        /// <param name="toEncrypt">Source String</param>
        /// <param name="useHashing">Hash value</param>
        /// <returns></returns>
        public static string Encrypt(string toEncrypt, bool useHashing)
        {
            byte[] keyArray;
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);

            System.Configuration.AppSettingsReader settingsReader =
                                                new AppSettingsReader();
            // Get the key from config file

            //string key = (string)settingsReader.GetValue("SecurityKey",typeof(String));
            string key = Config.Setting("SSRSTripleDESKey"); 

            //If hashing use get hashcode regards to your key
            if (useHashing)
            {
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                //Always release the resources and flush data
                // of the Cryptographic service provide. Best Practice

                hashmd5.Clear();
            }
            else
                keyArray = UTF8Encoding.UTF8.GetBytes(key);

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            //set the secret key for the tripleDES algorithm
            tdes.Key = keyArray;
            //mode of operation. there are other 4 modes.
            //We choose ECB(Electronic code Book)
            tdes.Mode = CipherMode.ECB;
            //padding mode(if any extra byte added)

            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateEncryptor();
            //transform the specified region of bytes array to resultArray
            byte[] resultArray =
              cTransform.TransformFinalBlock(toEncryptArray, 0,
              toEncryptArray.Length);
            //Release resources held by TripleDes Encryptor
            tdes.Clear();
            //Return the encrypted data into unreadable string format
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        /// <summary>
        /// Method to decrypt the data using Triple DES 
        /// </summary>
        /// <param name="cipherString">Encrypted target string</param>
        /// <param name="useHashing">Hash value</param>
        /// <returns></returns>
        public static string Decrypt(string cipherString, bool useHashing)
        {
            byte[] keyArray;
            //get the byte code of the string

            byte[] toEncryptArray = Convert.FromBase64String(cipherString);

            System.Configuration.AppSettingsReader settingsReader =
                                                new AppSettingsReader();
            //Get your key from config file to open the lock!
            //string key = (string)settingsReader.GetValue("SecurityKey",typeof(String));
            string key = Config.Setting("SSRSTripleDESKey");
            if (useHashing)
            {
                //if hashing was used get the hash code with regards to your key
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                //release any resource held by the MD5CryptoServiceProvider

                hashmd5.Clear();
            }
            else
            {
                //if hashing was not implemented get the byte code of the key
                keyArray = UTF8Encoding.UTF8.GetBytes(key);
            }

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            //set the secret key for the tripleDES algorithm
            tdes.Key = keyArray;
            //mode of operation. there are other 4 modes. 
            //We choose ECB(Electronic code Book)

            tdes.Mode = CipherMode.ECB;
            //padding mode(if any extra byte added)
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(
                                 toEncryptArray, 0, toEncryptArray.Length);
            //Release resources held by TripleDes Encryptor                
            tdes.Clear();
            //return the Clear decrypted TEXT
            return UTF8Encoding.UTF8.GetString(resultArray);
        }
        #endregion

    }
}
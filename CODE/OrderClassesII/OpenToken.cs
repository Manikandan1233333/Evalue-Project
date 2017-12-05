/* 
 * History:
 *	Creted By Cognizant for SSO integration wit Payment tool on 09/20/2010
 *	This class is created to read the OpenToken from the Soap header information.
 *	This class contains a readonly property named Token to get the Token value.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.Web.Services.Protocols;

namespace OrderClasses
{
    public class OpenToken: SoapHeader
    {
        private string openToken = string.Empty;

        /// <summary>
        /// Property to read the OpenToken value from the Soap header information.
        /// </summary>
        [XmlAttribute]
        public string Token
        {
            get { return openToken; }
            set { openToken = value; }
        }

    }
}

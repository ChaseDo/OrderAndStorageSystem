using KN_Order_Storage.Common;
using KN_Order_Storage.Logic.Interfaces;
using KN_Order_Storage.Models;
using KN_Order_Storage.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Xml.Linq;

namespace KN_Order_Storage.Logic.Implementor
{
    public class OptionsProvider : IOptionsProvider
    {
        public ClientSourceOption GetClientSourceOption()
        {
            string file = AppDomain.CurrentDomain.BaseDirectory + WebConfigurationManager.AppSettings[WebConstants.ClientSourceAdd];
            using (Stream stream = new FileStream(file, FileMode.Open, FileAccess.Read))
            {
                XDocument xDocument = XDocument.Load(stream);
                string xml = xDocument.ToString();
                return xml.XMLToModelDeserialize<ClientSourceOption>();
            }
        }

    }
}
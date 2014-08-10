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
using System.Web.Mvc;

namespace KN_Order_Storage.Logic.Implementor
{
    public class OptionsProvider : IOptionsProvider
    {
        private ClientSourceOption GetClientSourceOption()
        {
            string file = AppDomain.CurrentDomain.BaseDirectory + WebConfigurationManager.AppSettings[WebConstants.cClientSourceAdd];
            using (Stream stream = new FileStream(file, FileMode.Open, FileAccess.Read))
            {
                XDocument xDocument = XDocument.Load(stream);
                string xml = xDocument.ToString();
                return xml.XMLToModelDeserialize<ClientSourceOption>();
            }
        }


        //Populate ClientSourceOption
        public dynamic PopulateClientSourceOption(bool bIndex)
        {
            List<SelectListItem> list = new List<SelectListItem>();

            if (bIndex)
            {
                list.Add(new SelectListItem() { Text = WebConstants.cSearchAll, Value = WebConstants.cSearchAll });

                foreach (var x in GetClientSourceOption().Sources)
                {
                    list.Add(new SelectListItem() { Text = x.Name, Value = x.Name });
                }
            }
            else
            {
                foreach (var x in GetClientSourceOption().Sources)
                {
                    list.Add(new SelectListItem() { Text = x.Name, Value = x.Name });
                }
            }

            return list;
        }
    }
}
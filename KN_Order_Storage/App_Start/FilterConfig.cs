﻿using System.Web;
using System.Web.Mvc;

namespace KN_Order_Storage
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}

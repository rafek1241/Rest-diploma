﻿using System.Web;
using System.Web.Mvc;

namespace Rest.Web.Engineer
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}

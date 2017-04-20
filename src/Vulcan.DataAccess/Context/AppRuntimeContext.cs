﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Vulcan.DataAccess.Context
{
    public static class AppRuntimeContext
    {
        private static IHttpContextAccessor HttpContextAccessor;
        public static void Configure(IHttpContextAccessor httpContextAccessor)
        {
            HttpContextAccessor = httpContextAccessor;

        }

        public static bool Contains(string key)
        {
            return HttpContextAccessor.HttpContext.Items.ContainsKey(key);
        }

        public static void SetItem(string key,object item)
        {
            if (!Contains(key))
            {
                HttpContextAccessor.HttpContext.Items[key] = item;
            }

        }
        public static object GetItem(string key)
        {
            object item = null;
            if (Contains(key))
            {
                item = HttpContextAccessor.HttpContext.Items[key];
            }
            return item;
        }
        public static void RemoveItem(string key)
        {
            if (Contains(key))
            {
                HttpContextAccessor.HttpContext.Items.Remove(key);
            }
        }


    }
}

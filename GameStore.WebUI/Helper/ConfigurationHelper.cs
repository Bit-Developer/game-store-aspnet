using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace GameStore.WebUI.Helper
{
    public class ConfigurationHelper
    {
        public static string GetDefaultPassword()
        {
            return WebConfigurationManager.AppSettings["configFile"];
        }
        public static string GetAppId()
        {
            return WebConfigurationManager.AppSettings["CreditAppId"];
        }
        public static string GetSharedKey()
        {
            return WebConfigurationManager.AppSettings["CreditAppSharedKey"];
        }
        public static string GetAppId2()
        {
            return WebConfigurationManager.AppSettings["CreditAppId2"];
        }
        public static string GetSharedKey2()
        {
            return WebConfigurationManager.AppSettings["CreditAppSharedKey2"];
        }
    }
}
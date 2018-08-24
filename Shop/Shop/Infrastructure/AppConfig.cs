using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Shop.Infrastructure
{
    public class AppConfig
    {
        private static string _ImagesFolderRelative = ConfigurationManager.AppSettings["ImagesFolder"];
        public static string ImagesFolderRelative
        {
            get
            {
                return _ImagesFolderRelative;
            }
        }
    }
}
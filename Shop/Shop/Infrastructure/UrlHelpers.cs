using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shop.Infrastructure
{
    public static class UrlHelpers
    {
        public static string ImagesPath ( this UrlHelper helper, string nameImage )
        {
            var ImagesFolder = AppConfig.ImagesFolderRelative;
            var path = Path.Combine(ImagesFolder, nameImage);
            var pathAbsolute = helper.Content(path);
            return pathAbsolute;
        }
    }
}
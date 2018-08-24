using Postal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shop.ViewModels
{
    public class OrderRealizedEmail : Email
    {
        public string To { get; set; }
        public string From { get; set; }
        public int OrderId { get; set; }
    }
}
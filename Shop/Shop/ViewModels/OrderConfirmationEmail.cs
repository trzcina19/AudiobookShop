using Postal;
using Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shop.ViewModels
{
    public class OrderConfirmationEmail : Email
    {
        public string To { get; set; }
        public string From { get; set; }
        public decimal OrderValue { get; set; }
        public int OrderId { get; set; }
        public List<PositionOrder> PositionsOrder { get; set; }
    }


}
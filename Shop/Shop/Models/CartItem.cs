using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shop.Models
{
    public class CartItem
    {
        public Audiobook Audiobook { get; set; }
        public int Amount { get; set; }
        public decimal ValueCart { get; set; }
    }
}
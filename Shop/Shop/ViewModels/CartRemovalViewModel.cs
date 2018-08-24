using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shop.ViewModels
{
    public class CartRemovalViewModel
    {
        public decimal CartPriceTotal { get; set; }
        public int CartAmountItems { get; set; }
        public int AmountItemsRemoved { get; set; }
        public int IdItemRemoved { get; set; }
    }
}
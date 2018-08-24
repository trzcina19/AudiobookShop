using Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shop.ViewModels
{
    public class CartViewModel
    {
        public List<CartItem> CartItemS { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
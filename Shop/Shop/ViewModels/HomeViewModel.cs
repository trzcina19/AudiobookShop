using Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shop.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Audiobook> News { get; set; }
        public IEnumerable<Audiobook> Bestsellers { get; set; }
    }
}
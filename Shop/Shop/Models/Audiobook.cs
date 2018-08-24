using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shop.Models
{
    public class Audiobook
    {
        public int AudiobookId { get; set; }
        public int CategoryId { get; set; } // Maker
        public string NameAudiobook { get; set; }
        public DateTime DateAdded { get; set; }
        public string ImageFileName { get; set; }
        public string DescriptionAudiobook { get; set; }
        public decimal PriceAudiobook { get; set; }
        public bool LackAudiobook{ get; set; } // When no offer
        public bool Bestseller { get; set; }
        public virtual Category Category { get; set; }
    }
}
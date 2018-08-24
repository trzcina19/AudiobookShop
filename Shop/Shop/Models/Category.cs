using System.Collections.Generic;

namespace Shop.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string CategoryDescription { get; set; }
        public string FileNameCategory { get; set; }
        
        public virtual ICollection<Audiobook> Audiobooks { get; set; }
    }
}
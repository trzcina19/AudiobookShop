using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Shop.Models
{
    public class Order
    {
        public int OrderId { get; set; }

        public string UserId { get; set; }
      
        public virtual ApplicationUser User { get; set; }

        [Required(ErrorMessage = "To pole jest wymagane.")]
        [StringLength(50, ErrorMessage = "Błędny format imienia.", MinimumLength = 1)]
        public string Name { get; set; }

        [Required(ErrorMessage = "To pole jest wymagane.")]
        [StringLength(50)]
        public string Surname { get; set; }


        [Required(ErrorMessage = "To pole jest wymagane.")]
        [StringLength(100)]
        public string Street { get; set; }

        [Required(ErrorMessage = "To pole jest wymagane.")]
        [StringLength(100)]
        public string City { get; set; }

        [Required(ErrorMessage = "To pole jest wymagane.")]
        [RegularExpression(@"[0-9]{2}-[0-9]{3}", ErrorMessage = "Błędny format kodu pocztowego.")]
        [StringLength(6)]
        public string PostalCode { get; set; }

        [Required(ErrorMessage = "To pole jest wymagane.")]
        [StringLength(20)]
        [RegularExpression(@"(\+\d{2})*[\d\s-]+", ErrorMessage = "Błędny format numeru telefonu.")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "To pole jest wymagane.")]
        [EmailAddress(ErrorMessage = "Błędny format adresu e-mail.")]
        public string Email{ get; set; }

        public string Comment { get; set; }
        public DateTime DateAdded { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public decimal OrderValue { get; set; }

        public List<PositionOrder> PositionsOrder { get; set; }

    }

    public enum OrderStatus
    {
        Nowe,
        Zrealizowane,
    }
}
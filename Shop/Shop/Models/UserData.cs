using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

/*
    User data 
*/
namespace Shop.Models
{
    public class UserData
    {

        [StringLength(50, ErrorMessage = "Błędny format imienia.", MinimumLength = 1)]
        public string Name { get; set; }

        [StringLength(50, ErrorMessage = "Błędny format nazwiska", MinimumLength = 1)]
        public string Surname { get; set; }

        [StringLength(100, ErrorMessage = "Błędny format adresu", MinimumLength = 1)]
        public string Street { get; set; }

        [StringLength(100, ErrorMessage = "Błędny format nazwy miasta", MinimumLength = 1)]
        public string City { get; set; }

        [RegularExpression(@"[0-9]{2}-[0-9]{3}", ErrorMessage = "Błędny format kodu pocztowego.")]
        public string PostalCode { get; set; }

        [RegularExpression(@"(\+\d{2})*[\d\s-]+", ErrorMessage = "Błędny format numeru telefonu.")]
        public string PhoneNumber { get; set; }

        [EmailAddress(ErrorMessage = "Błędny format adresu e-mail.")]
        public string Email { get; set; }
    }
}
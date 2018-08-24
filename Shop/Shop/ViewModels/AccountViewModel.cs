using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Shop.ViewModels
{
    /*
     Login View Model
     */
    public class LoginViewModel
    {
        [Required(ErrorMessage = "To pole jest wymagane.")]
        [Display(Name = "E-mail")]
        [EmailAddress(ErrorMessage ="Niepoprawny format adresu e-mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "To pole jest wymagane.")]
        [DataType(DataType.Password)]
        [Display(Name = "Hasło")]
        public string Password { get; set; }

        [Display(Name = "Zapamiętaj mnie")]
        public bool RememberMe { get; set; }
    }

    /*
     Register View Model
     */
    public class RegisterViewModel
    {
        [EmailAddress(ErrorMessage = "Pole '{0}' nie jest poprawnym adresem e-mail")]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [StringLength(30, ErrorMessage = "Pole {0} musi mieć co najmniej {2} znaków.", MinimumLength = 6)]
        [Display(Name = "Hasło")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

     
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "'Hasło' i 'Potwierdzenie hasła' nie pasują do siebie.")]
        public string ConfirmPassword { get; set; }
    }
}

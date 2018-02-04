using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TicketsBookingOnlineSystem.ViewModels
{
    public class UserLoginViewModel
    {
        [DisplayName("Hasło")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Proszę podać swoje hasło.", AllowEmptyStrings = false)]
        [StringLength(50, MinimumLength = 8, ErrorMessage = "Hasło musi posiadać conajmniej 8 znaków.")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Proszę podać swój email.")]
        [EmailAddress(ErrorMessage = "Podany email jest nieprawidłowy")]
        public string Email { get; set; }
        [Display(Name ="Zapamiętaj hasło")]
        public bool RemeberMe { get; set; }
    }
}
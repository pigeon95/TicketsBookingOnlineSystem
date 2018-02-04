using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TicketsBookingOnlineSystem.ViewModels
{
    public class ResetPasswordViewModel
    {
        [DisplayName("Hasło")]
        [Required(ErrorMessage = "Podaj swoje nowe hasło.", AllowEmptyStrings = false)]
        [DataType(DataType.Password)]
        [StringLength(50, MinimumLength = 8, ErrorMessage = "Hasło musi posiadać conajmniej 8 znaków.")]
        public string NewPassword { get; set; }
        [DisplayName("Powtórz hasło")]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "Podane hasło się nie zgadza.")]
        public string ConfirmPassword { get; set; }
        public string ResetPasswordCode { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TicketsBookingOnlineSystem.ViewModels
{
    public class EmailFormModel
    {
        [Required(ErrorMessage = "Proszę uzupełnić pole."), Display(Name = "Do:"), EmailAddress(ErrorMessage = "Wprowadzona wartość nie jest prawidłowym adresem e-mail.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Proszę uzupełnić pole."), Display(Name = "Wiadomość:")]
        public string Message { get; set; }
        [Required(ErrorMessage = "Proszę uzupełnić pole."), Display(Name = "Temat:")]
        public string Subject { get; set; }
    }
}


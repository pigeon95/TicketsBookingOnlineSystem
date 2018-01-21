using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace TicketsBookingOnlineSystem.ViewModels
{
    public class UserViewModel
    {
        public int Id { get; set; }
        [DisplayName("Imię")]
        public string Name { get; set; }
        [DisplayName("Nazwisko")]
        public string Surname { get; set; }
        public string Email { get; set; }
    }
}
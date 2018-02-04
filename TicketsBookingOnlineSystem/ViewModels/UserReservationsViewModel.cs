using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace TicketsBookingOnlineSystem.ViewModels
{
    public class UserReservationsViewModel
    {
        public int Id { get; set; }
        [DisplayName("Nazwa seansu")]
        public string SeanceName { get; set; }
        [DisplayName("Film")]
        public string SeanceFilmTitle { get; set; }
        [DisplayName("Data seansu")]
        public DateTime Date { get; set; }
        [DisplayName("Sala")]
        public string SeanceAuditoriumName { get; set; }
        [DisplayName("Zarezerwowane miejsca")]
        public int SpotCount { get; set; }
    }
}
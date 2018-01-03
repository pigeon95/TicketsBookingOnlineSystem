using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace TicketsBookingOnlineSystem.ViewModels
{
    public class SeanceViewModel
    {
        public int Id { get; set; }
        [DisplayName("Nazwa seansu")]
        public string Name { get; set; }
        [DisplayName("Cena")]
        public decimal Price { get; set; }
        [DisplayName("Data seansu")]
        public DateTime Date { get; set; }
        [DisplayName("Film")]
        public string FilmTitle { get; set; }
    }
}
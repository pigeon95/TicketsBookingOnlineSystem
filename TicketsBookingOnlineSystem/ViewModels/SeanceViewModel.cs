using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TicketsBookingOnlineSystem.ViewModels
{
    public class SeanceViewModel
    {
        public int Id { get; set; }
        [DisplayName("Nazwa seansu:")]
        public string Name { get; set; }
        [DisplayName("Cena:")]
        public decimal Price { get; set; }
        [DisplayName("Data seansu:")]
        public DateTime Date { get; set; }
        [DisplayName("Film:")]
        public string FilmTitle { get; set; }
        [DisplayName("Sala:")]
        public string AuditoriumName { get; set; }
        public string LocationArrangement { get; set; }

    }
}
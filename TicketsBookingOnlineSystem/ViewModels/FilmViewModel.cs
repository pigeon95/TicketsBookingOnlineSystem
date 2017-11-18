using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TicketsBookingOnlineSystem.ViewModels
{
    public class FilmViewModel
    {
        public int Id { get; set; }
        public TimeSpan Duration { get; set; }
        public string Title { get; set; }
        public string FilmGenreName { get; set; }
    }
}
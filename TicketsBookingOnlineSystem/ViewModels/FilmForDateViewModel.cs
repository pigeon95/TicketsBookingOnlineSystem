using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TicketsBookingOnlineSystem.ViewModels
{
    public class FilmForDateViewModel
    {
        public FilmViewModel Film { get; set; }
        public List<SeanceViewModel> Seances { get; set; }
    }
}
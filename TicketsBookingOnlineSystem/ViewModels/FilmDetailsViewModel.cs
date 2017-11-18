using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TicketsBookingOnlineSystem.ViewModels
{
    public class FilmDetailsViewModel : FilmViewModel 
    {
        public decimal Price { get; set; }
        public DateTime Date { get; set; }
        public List<CreatorViewModel> Creators { get; set; }
        public List<FilmImageViewModel> FilmImages { get; set; }
        public List<SeanceViewModel> Seances { get; set; }
    }
}


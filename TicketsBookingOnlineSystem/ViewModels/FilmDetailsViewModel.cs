using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TicketsBookingOnlineSystem.ViewModels
{
    public class FilmDetailsViewModel : FilmViewModel 
    {
        public string CreatorName { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }

        //public List<FilmImageViewModel> FilmImages { get; set; }
        public List<SeanceViewModel> Seances { get; set; }
    }
}


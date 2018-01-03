using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TicketsBookingOnlineSystem.ViewModels
{
    public class FilmForListViewModel
    {
        public List<FilmViewModel> Film { get; set; }
        public List<AddFilmViewModel> Top10ByRating { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TicketsBookingOnlineSystem.ViewModels
{
    public class SeanceDateViewModel
    {
        public List<MenuDateViewModel> DateMenu{ get; set; }
        public List<FilmForDateViewModel> Films{ get; set; }
    }
}
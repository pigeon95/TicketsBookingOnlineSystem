using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TicketsBookingOnlineSystem.Models
{
    public class Film
    {
        public int Id { get; set; }
        public FilmImage FilmImage { get; set; }
        public TimeSpan Duration { get; set; }
        public string Title { get; set; }
        public Creator Creator { get; set; }
        public bool Deleted { get; set; }
    }
}
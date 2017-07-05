using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TicketsBookingOnlineSystem.Models
{
    public class Film
    {
        public int Id { get; set; }
        public TimeSpan Duration { get; set; }
        public string Title { get; set; }
        public bool Deleted { get; set; }

        public virtual IList<FilmImage> FilmImages { get; set; }
        public virtual IList<Creator> Creators { get; set; }
    }

}
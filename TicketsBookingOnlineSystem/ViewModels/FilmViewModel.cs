using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace TicketsBookingOnlineSystem.ViewModels
{
    public class FilmViewModel
    {
        public int Id { get; set; }
        [DisplayName("Tytuł")]
        public string Title { get; set; }
        [DisplayName("Gatunek")]
        public string FilmGenreName { get; set; }
        [DisplayName("Czas trwania")]
        public TimeSpan Duration { get; set; }
        public string Image { get; set; }
    }
}
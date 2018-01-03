using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TicketsBookingOnlineSystem.ViewModels
{
    public class AddFilmViewModel
    {
        
        [DisplayName("Tytuł:")]
        [Required(ErrorMessage = "Proszę podać tytuł filmu.", AllowEmptyStrings = false)]
        public string Title { get; set; }
        //[Required(ErrorMessage = "Proszę podać gatunek filmowy.", AllowEmptyStrings = false)]
        [DisplayName("Czas trwania:")]
        [RegularExpression(@"^(?:(?:([01]?\d|2[0-3]):)?([0-5]?\d):)?([0-5]?\d)$", ErrorMessage = "Nieprawidłowy format czasu")]
        [Required(ErrorMessage = "Proszę podać czas trwania filmu.", AllowEmptyStrings = false)]
        public TimeSpan Duration { get; set; }
        [DisplayName("Usunięty:")]
        public bool Deleted { get; set; }
        public IList<string> FilmImages { get; set; }
        [DisplayName("Reżyser:")]
        public int Creator { get; set; }
        public List<SelectListItem> Creators { get; set; }
        [DisplayName("Gatunek:")]
        public int FilmGenre { get; set; }
        public List<SelectListItem> FilmGenres { get; set; }
        [DisplayName("Opis:")]
        public string Description { get; set; }
        [DisplayName("Grafika:")]
        public string Image { get; set; }
        public HttpPostedFileBase ImageFile { get; set; }
    }
}
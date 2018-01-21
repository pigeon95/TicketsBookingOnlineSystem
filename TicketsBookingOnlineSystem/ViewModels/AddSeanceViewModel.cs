using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TicketsBookingOnlineSystem.Models;

namespace TicketsBookingOnlineSystem.ViewModels
{
    public class AddSeanceViewModel
    {
        public int Id { get; set; }
        [DisplayName("Nazwa:")]
        [Required(ErrorMessage = "Proszę uzupełnić pole.", AllowEmptyStrings = false)]
        public string Name { get; set; }
        [DisplayName("Cena:")]
        [Required(ErrorMessage = "Proszę uzupełnić pole.", AllowEmptyStrings = false)]
        public decimal Price { get; set; }
        [DisplayName("Data seansu:")]
        [Required(ErrorMessage = "Proszę uzupełnić pole.", AllowEmptyStrings = false)]
        public DateTime Date { get; set; }
        public List<SelectListItem> Auditoriums { get; set; }
        [DisplayName("Nazwa sali:")]
        public int Auditorium { get; set; }
        public List<SelectListItem> Films { get; set; }
        [DisplayName("Film:")]
        public int Film { get; set; }
        [DisplayName("Usunięty:")]
        public bool Deleted { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TicketsBookingOnlineSystem.ViewModels
{
    public class AddCreatorViewModel
    {
        public int Id { get; set; }
        [DisplayName("Tożsamość")]
        [Required(ErrorMessage = "Proszę podać tożsamość reżysera.", AllowEmptyStrings = false)]
        public string Name { get; set; }
        [DisplayName("Opis")]
        public string Description { get; set; }
        public bool Deleted { get; set; }
    }
}
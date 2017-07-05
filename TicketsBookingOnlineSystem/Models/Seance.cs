using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TicketsBookingOnlineSystem.Models
{
    public class Seance
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public DateTime Date { get; set; }
        public bool Deleted { get; set; }

        public virtual Auditorium Auditorium { get; set; }
        public virtual Film Film { get; set; }
        public virtual IList<Reservation> Reservations { get; set; }
    }
}
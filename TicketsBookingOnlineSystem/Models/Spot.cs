using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TicketsBookingOnlineSystem.Models
{
    public class Spot
    {
        public int Id { get; set; }
        public int Col { get; set; }
        public int Row { get; set; }
        public string Name { get; set; }
        public bool Deleted { get; set; }

        public virtual Auditorium Auditorium { get; set; }
        public virtual IList<Reservation> Reservations { get; set; }
    }
}
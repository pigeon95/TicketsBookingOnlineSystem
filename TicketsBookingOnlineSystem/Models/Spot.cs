using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TicketsBookingOnlineSystem.Models
{
    public class Spot
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Deleted { get; set; }

        public virtual Row Row { get; set; }
        public virtual List<Reservation> Reservations { get; set; }
    }
}
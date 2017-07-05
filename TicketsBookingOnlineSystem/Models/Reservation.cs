using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TicketsBookingOnlineSystem.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int Spot { get; set; }
        public int SeanceId { get; set; }
        public int Row { get; set; }
        public DateTime Date { get; set; }
        public bool Deleted { get; set; }

        public virtual User User { get; set; }
        public virtual Seance Seance { get; set; }
        public virtual IList<Spot> Spots { get; set; }
    }
}
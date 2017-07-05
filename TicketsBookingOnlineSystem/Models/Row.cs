using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TicketsBookingOnlineSystem.Models
{
    public class Row
    {
        public int Id { get; set; }
        public string RowId { get; set; } //to potrzebne?
        public int SpotCount { get; set; }
        public string Name { get; set; }
        public bool Deleted { get; set; }

        public virtual Auditorium Auditorium { get; set; }
        public virtual IList<Spot> Spots { get; set; }
    }
}
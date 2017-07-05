using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TicketsBookingOnlineSystem.Models
{
    public class Auditorium
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Deleted { get; set; }

        public virtual IList<Seance> Seances { get; set; }
        public virtual IList<Row> Rows { get; set; }
    }
}
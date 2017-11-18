using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TicketsBookingOnlineSystem.Models
{
    public class User
    {   
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public DateTime? BirthDate { get; set; }
        public bool Deleted { get; set; }

        public virtual City City { get; set; }
        public virtual IList<Reservation> Reservations { get; set; }
        public UserRoleEnum Role { get; set; }
    }
}
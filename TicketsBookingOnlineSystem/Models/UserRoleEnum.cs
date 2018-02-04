using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TicketsBookingOnlineSystem.Models
{
    public enum UserRoleEnum: int
    {
        User = 0,
        Admin = 1,
        Dealer = 2,
    }
}
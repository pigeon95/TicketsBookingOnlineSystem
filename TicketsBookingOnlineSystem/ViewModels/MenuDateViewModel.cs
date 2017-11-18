using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TicketsBookingOnlineSystem.ViewModels
{
    public class MenuDateViewModel
    {
        public DateTime Date { get; set; }
        public string Label { get; set; }
        public bool Selected { get; set; }
    }
}
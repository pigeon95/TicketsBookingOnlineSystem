using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TicketsBookingOnlineSystem.Context;
using TicketsBookingOnlineSystem.Models;
using TicketsBookingOnlineSystem.ViewModels;

namespace TicketsBookingOnlineSystem.Controllers
{
    public class RegisterController : Controller
    {
        // GET: Register
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(UserViewModel user)
        {
            if (ModelState.IsValid)
            {
                CinemaDbContext db = new CinemaDbContext();

                User entity = new User();

                entity.Name = user.Name;
                entity.Surname = user.Surname;
                entity.Password = user.Password;
                entity.BirthDate = user.BirthDate;
                entity.City = user.City;
                entity.Email = user.Email;

                db.Users.Add(entity);
                db.SaveChanges();

                ViewBag.Message = "Udało się stworzyć konto";
            }       
            
            return View(user);
        }
    }
}
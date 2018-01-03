using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using TicketsBookingOnlineSystem.Context;
using TicketsBookingOnlineSystem.Models;
using TicketsBookingOnlineSystem.ViewModels;

namespace TicketsBookingOnlineSystem.Controllers
{
    public class AccountController : Controller
    {
        CinemaDbContext db = new CinemaDbContext();

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserLoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            using (db)
            {
                var v = db.Users.FirstOrDefault(a => a.Email == model.Email);
                if (v == null)
                {
                    return View(model);
                }

                if (v.Password != model.Password)
                {
                    //komunikat o złym haśle
                    return View(model);
                }

                List<Claim> claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, v.Email),
                        new Claim(ClaimTypes.NameIdentifier, v.Id.ToString()),
                        new Claim(ClaimTypes.Role, v.Role.ToString()),
                        //new Claim("Avatar", user.Avatar)
            };


                var identity = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);

                Request.GetOwinContext().Authentication.SignIn(identity);

            }
            return RedirectToAction("Repertoire", "Repertoire");
        }

        [Authorize]
        public ActionResult AfterLogin()
        {
            return View();
        }

        [Authorize]
        public ActionResult LogOut()
        {
            Request.GetOwinContext().Authentication.SignOut();
            //FormsAuthentication.SignOut();
            //Session.Abandon(); // it will clear the session at the end of request
            return RedirectToAction("Login");
        }


        // GET: Register
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(UserRegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            
            User user = new User();

            user.Name = model.Name;
            user.Surname = model.Surname;
            user.Password = model.Password;
            user.BirthDate = model.BirthDate;
            user.Address = model.Address;
            user.Phone = model.Phone;
            user.Email = model.Email;

            var city = db.Cities.FirstOrDefault(c => c.Name == model.City);

            if (city == null)
            {
                city = new City();
                city.Name = model.City;
                user.City = city;
                db.Cities.Add(city);
            }

            if (db.Users.Any(x => x.Email == model.Email))
            {
                ModelState.AddModelError(string.Empty, "Podany Email jest już zajety. Proszę wybrać inny.");

                //komunikat ten email jest zajety
                return View(model);
            }

            db.Users.Add(user);
            db.SaveChanges();

            //przekierowanie do logowania
            return RedirectToAction("Login");
        }

        public JsonResult IsUserExists(string Email)
        {
            CinemaDbContext db = new CinemaDbContext();
            //check if any of the Email matches the Email specified in the Parameter using the ANY extension method.  
            return Json(!db.Users.Any(x => x.Email == Email), JsonRequestBehavior.AllowGet);
        }

    }
}
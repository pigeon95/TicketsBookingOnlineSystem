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

            using (CinemaDbContext db = new CinemaDbContext())
            {
                var v = db.Users.FirstOrDefault(a => a.Email == model.Email);
                if (v == null)
                {
                    return View(model);
                }

                if (v.Password != model.Password)
                {
                    //kominikat o złym haśle
                    return View(model);
                }
                //Session["LoggedUserID"] = v.Id;
                // Session["LoggedUserEmail"] = v.Email.ToString();

                List<Claim> claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, v.Email),
                        new Claim(ClaimTypes.NameIdentifier, v.Id.ToString()),
                        new Claim(ClaimTypes.Role, "User"),
                        //new Claim("Avatar", user.Avatar)
                    };


                var identity = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);

                Request.GetOwinContext().Authentication.SignIn(identity);

            }
            return RedirectToAction("AfterLogin");
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
                return View(model);
            }

            CinemaDbContext db = new CinemaDbContext();

            if(db.Users.Any(x => x.Email == model.Email))
            {
                ModelState.AddModelError(string.Empty, "komunikat ten email jest zajety.");

                //komunikat ten email jest zajety
                return View(model);
            }


            User entity = new User();

            entity.Name = model.Name;
            entity.Surname = model.Surname;
            entity.Password = model.Password;
            entity.BirthDate = model.BirthDate;
            entity.Address = model.Address;
            entity.Phone = model.Phone;

            var city = db.Cities.FirstOrDefault(c => c.Name == model.City);

            if (city == null)
            {
                city = new City();
                city.Name = model.City;
                entity.City = city;
                db.Cities.Add(city);
            }
            entity.Email = model.Email;

            db.Users.Add(entity);
            db.SaveChanges();

            ViewBag.Message = "Udało się stworzyć konto";

            //przekierowanie do lodowania
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
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
    public class HomeController : Controller
    {
        CinemaDbContext db = new CinemaDbContext();

        public ActionResult Index()
        {
            return View();
        }

        //public ActionResult About()
        //{
        //    ViewBag.Message = "Your application description page.";

        //    return View();
        //}

        //public ActionResult Contact()
        //{
        //    ViewBag.Message = "Your contact page.";

        //    return View();
        //}

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserViewLogin u)
        {
            if (!ModelState.IsValid)
            {
                return View(u);
            }

            using (CinemaDbContext db = new CinemaDbContext())
            {
                var v = db.Users.FirstOrDefault(a => a.Email == u.Email && a.Password == u.Password);
                if (v != null)
                {
                    Session["LoggedUserID"] = v.Id;
                    Session["LoggedUserEmail"] = v.Email.ToString();
                    return RedirectToAction("AfterLogin");
                }
            }
            return View(u);
        }

        public ActionResult AfterLogin()
        {
            if (Session["LoggedUserID"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
    }

}
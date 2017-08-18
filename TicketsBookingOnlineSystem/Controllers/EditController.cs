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
    public class EditController : Controller
    {
        // GET: Edit
        public ActionResult Edit()
        {
            CinemaDbContext db = new CinemaDbContext();

            var sessionId = Session["LoggedUserID"];
            if (sessionId == null)
            {
                return View("");
            }
            int userId = (int)sessionId;

            var entity = db.Users.FirstOrDefault(UserViewEdit => UserViewEdit.Id == userId);
            var model = new UserViewEdit();
            if (entity != null)
            {
                model.Name = entity.Name;
                model.Surname = entity.Surname;
                model.Password = entity.Password;
                model.BirthDate = entity.BirthDate;
                //model.City = entity.City;
                model.Email = entity.Email;
            }
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserViewEdit model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            CinemaDbContext db = new CinemaDbContext();

            var sessionId = Session["LoggedUserID"];
            if (sessionId == null)
            {
                return View("");
            }
            int userId = (int)sessionId;

            var entity = db.Users.FirstOrDefault(UserViewEdit => UserViewEdit.Id == userId);

            if (entity != null)
            {
                entity.Name = model.Name;
                entity.Surname = model.Surname;
                entity.Password = model.Password;
                entity.BirthDate = model.BirthDate;
                //entity.City = model.City;
                var city = db.Cities.FirstOrDefault(c => c.Name == model.City);

                entity.Email = model.Email;

                db.Users.Attach(entity);
                var entry = db.Entry(entity);

                entry.Property(e => e.Name).IsModified = true;
                entry.Property(e => e.Surname).IsModified = true;
                entry.Property(e => e.Password).IsModified = true;
                entry.Property(e => e.BirthDate).IsModified = true;
                entry.Property(e => e.City).IsModified = true;
                entry.Property(e => e.Email).IsModified = true;

                db.SaveChanges();
            }
            ViewBag.Message = "Twoje konto zostało edytowane";


            return View(model);
        }
    }
}
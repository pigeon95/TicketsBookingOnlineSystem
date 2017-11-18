using AutoMapper;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TicketsBookingOnlineSystem.Context;
using TicketsBookingOnlineSystem.Models;
using TicketsBookingOnlineSystem.ViewModels;

namespace TicketsBookingOnlineSystem.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        // GET: Edit
        public ActionResult Edit()
        {
            CinemaDbContext db = new CinemaDbContext();

            int userId = HttpContext.User.Identity.GetUserId<int>();

            //var entity = db.Users.FirstOrDefault(UserViewEdit => UserViewEdit.Id == userId);
            var entity = db.Users
                .AsQueryable()
                .Include(u => u.City)
                .FirstOrDefault(u => u.Id == userId);
            //var user = new UserViewEdit();

            var model = Mapper.Map<UserEditViewModel>(entity);
            //if (entity != null)
            //{
            //    user.Name = entity.Name;
            //    user.Surname = entity.Surname;
            //    user.Password = entity.Password;
            //    user.BirthDate = entity.BirthDate;
            //    user.Address = entity.Address;
            //    user.Phone = entity.Phone;
            //    if(entity.City != null)
            //    {
            //        user.City = entity.City.Name;
            //    }
            //    user.Email = entity.Email;
            //}
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserEditViewModel user)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
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
                entity.Name = user.Name;
                entity.Surname = user.Surname;
                entity.Password = user.Password;
                entity.BirthDate = user.BirthDate;
                entity.Address = user.Address;
                entity.Phone = user.Phone;
                var city = db.Cities.FirstOrDefault(c => c.Name == user.City);
                if (city == null)
                {
                    city = new City();
                    city.Name = user.City;
                    entity.City = city;
                    db.Cities.Add(city);
                }

                entity.Email = user.Email;

                db.Users.Attach(entity);
                var entry = db.Entry(entity);

                entry.Property(e => e.Name).IsModified = true;
                entry.Property(e => e.Surname).IsModified = true;
                entry.Property(e => e.Password).IsModified = true;
                entry.Property(e => e.BirthDate).IsModified = true;
                entry.Property(e => e.Address).IsModified = true;
                entry.Property(e => e.Phone).IsModified = true;
                entry.Property(e => e.Email).IsModified = true;

                db.SaveChanges();
            }
            ViewBag.Message = "Twoje konto zostało edytowane";

            return View(user);
        }
    }
}
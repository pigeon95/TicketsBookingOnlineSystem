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
        CinemaDbContext db = new CinemaDbContext();
        // GET: Edit
        public ActionResult Edit()
        {   
            int userId = HttpContext.User.Identity.GetUserId<int>();

            var user = db.Users
                //.AsQueryable()
                .Include(u => u.City)
                .FirstOrDefault(u => u.Id == userId);

            var model = new UserEditViewModel()
            {
                Name = user.Name,
                Surname = user.Surname,
                //Password = user.Password,
                Address = user.Address,
                Phone = user.Phone,
                BirthDate = user.BirthDate,
                Email = user.Email,
                City = user.City.Name
            };

            //var model = Mapper.Map<UserEditViewModel>(entity);

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

            int userId = HttpContext.User.Identity.GetUserId<int>();

            var entity = db.Users.FirstOrDefault(UserViewEdit => UserViewEdit.Id == userId);

            if (entity != null)
            {
                entity.Name = user.Name;
                entity.Surname = user.Surname;
                //entity.Password = user.Password;
                entity.BirthDate = user.BirthDate;
                if (entity.BirthDate > DateTime.Now.AddYears(-5))
                {
                    ModelState.AddModelError("", "Data urodzenia jest nieprawidłowa.");
                    return View(user);
                }
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

                entity.City = city;
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
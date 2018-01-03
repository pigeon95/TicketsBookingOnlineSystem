using AutoMapper;
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
    public class FilmDetailsController : Controller
    {
        // GET: FilmDetails
        public ActionResult Index(int id)
        {
            CinemaDbContext db = new CinemaDbContext();

            var film = db.Films
                .FirstOrDefault(x => x.Id == id);
         
            if(film == null)
            {
                //errorfilmu nie znaleziono
                return View();
            }


            var model = Mapper.Map<FilmDetailsViewModel>(film);

            return View(model);
        }
    }
}
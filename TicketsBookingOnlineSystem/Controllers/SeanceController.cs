using AutoMapper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TicketsBookingOnlineSystem.Context;
using TicketsBookingOnlineSystem.ViewModels;

namespace TicketsBookingOnlineSystem.Controllers
{
    public class SeanceController : Controller
    {
        // GET: Seance
        public ActionResult Index(int id)
        {
            CinemaDbContext db = new CinemaDbContext();

            var entity = db.Seances
                .FirstOrDefault(x => x.Id == id);

            if (entity == null)
            {
                //errorfilmu nie znaleziono
                return View();
            }

            var model = Mapper.Map<SeanceViewModel>(entity);

            return View(model);
        }

        //JsonResult
        public ActionResult GetReservedSpots(int id)
        {
            CinemaDbContext db = new CinemaDbContext();

            var seats = db.Reservations.Where(x => x.SeanceId == id).ToList();
            var model = Mapper.Map<List<JsonReservationInfoViewModel>>(seats);

            var json = JsonConvert.SerializeObject(model);

            return Content(json, "application/json");
        }
    }
}
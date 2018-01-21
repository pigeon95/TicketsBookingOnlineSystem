using AutoMapper;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
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
    public class SeanceController : Controller
    {
        CinemaDbContext db = new CinemaDbContext();
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
            model.LocationArrangement = entity.Auditorium.LocationArrangement;

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "User")]
        public ActionResult Index(NewReservationModel model)
        {
            var spots = new List<Spot>();

            var seance = db.Seances.FirstOrDefault(x => x.Id == model.Id);
            var auditorium = seance.Auditorium;

            foreach(var id in model.Spots.Split(','))
            {
                var tmp = id.Split('_');
                var row = Int32.Parse(tmp[1]);
                var col = Int32.Parse(tmp[2]);

                var spot = auditorium
                    .Spots
                    .FirstOrDefault(x => x.Row == row && x.Col == col);

                spots.Add(spot);
            }

            int userId = HttpContext.User.Identity.GetUserId<int>();

            Reservation reservation = new Reservation();
            reservation.Seance = seance;
            reservation.Spots = spots;
            reservation.UserId = userId;
            reservation.Date = DateTime.Now;

            db.Reservations.Add(reservation);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        //JsonResult
        public ActionResult GetReservedSpots(int id)
        {
            var reservations = db.Reservations.Where(x => x.SeanceId == id).ToList();

            var model = new List<JsonReservationInfoViewModel>();

            foreach(var reservation in reservations)
            {
                var spots = reservation.Spots;
                foreach(var spot in spots)
                {
                    model.Add(new JsonReservationInfoViewModel {
                        Col = spot.Col,
                        Row = spot.Row
                    });
                }
            }


            var json = JsonConvert.SerializeObject(model);

            return Content(json, "application/json");
        }
    }
}
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TicketsBookingOnlineSystem.Context;
using TicketsBookingOnlineSystem.Models;
using TicketsBookingOnlineSystem.ViewModels;

namespace TicketsBookingOnlineSystem.Controllers
{
    public class RepertoireController : Controller
    {
        // GET: Repertoire
        public ActionResult Repertoire(string id)
        {
            CinemaDbContext db = new CinemaDbContext();
            DateTime dt;

            if (id == null)
            {
                dt = DateTime.Now;
            }
            else
            {
                dt = DateTime.ParseExact(id, "dd-MM-yyyy", CultureInfo.InvariantCulture);
            }

            var seances = db.Seances.Where(s => s.Date.Day == dt.Day && s.Date.Month == dt.Month && s.Date.Year == dt.Year).ToList();

            var model = new SeanceDateViewModel();  

            var dates = new List<MenuDateViewModel>();

            var date = DateTime.Now;

            for (int i = 0; i < 7; i++)
            {
                var dayModel = new MenuDateViewModel();
                dayModel.Date = date;

                dates.Add(dayModel);
                date = date.AddDays(1);
            }

            var films = seances.GroupBy(
                f => f.Film.Id,
                (key, value) => new FilmForDateViewModel {
                    Film = Mapper.Map<FilmViewModel>(value.First().Film),
                    Seances = Mapper.Map<List<SeanceViewModel>>(value)
                }).ToList();

            model.Films = films;
            model.DateMenu = dates;
            return View(model);
        }

        public ActionResult choosenSeance(int id)
        {
            CinemaDbContext db = new CinemaDbContext();

            var entity = db.Seances
                .FirstOrDefault(x => x.Id == id);


            if (entity == null)
            {
                //errorseansu nie znaleziono
                return View();
            }

            var model = Mapper.Map<SeanceViewModel>(entity);

            return View(model);

        }
        
    }

}
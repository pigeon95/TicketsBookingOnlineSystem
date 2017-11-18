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

            if (id ==null)
            {
                dt = DateTime.Now;
            }
            else
            {
                dt = DateTime.ParseExact(id, "dd-MM-yyyy", CultureInfo.InvariantCulture);
            }
            

            //var films = db.Films.ToList();

            //var entity = db.Seances
            //    .FirstOrDefault(x => x.Date == dt);

            //var entity = db.Films.Where(x => x.Seances.Any(s => s.Date.Date == dt.Value.Date));

            var entity = db.Films.Where(x => x.Seances.Any(s => s.Date.Day == dt.Day && s.Date.Month == dt.Month && s.Date.Year == dt.Year)).ToList();

            var model = new SeanceDateViewModel();  //wczesniej FilmListViewModel

            var dates = new List<MenuDateViewModel>();

            var date = DateTime.Now;

            for (int i = 0; i < 7; i++)
            {
                var dayModel = new MenuDateViewModel();
                dayModel.Date = date;

                dates.Add(dayModel);

                date = date.AddDays(1);
            }

            model.Films = Mapper.Map<List<FilmForDateViewModel>>(entity); //wczesniej FilmViewModel
            model.DateMenu = dates;
            return View(model);
        }
        
    }

}
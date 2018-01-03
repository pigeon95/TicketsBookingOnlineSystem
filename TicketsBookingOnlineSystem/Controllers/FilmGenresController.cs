using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TicketsBookingOnlineSystem.Context;
using TicketsBookingOnlineSystem.ViewModels;

namespace TicketsBookingOnlineSystem.Controllers
{
    public class FilmGenresController : Controller
    {
        CinemaDbContext db = new CinemaDbContext();
        // GET: FilmGenres
        public ActionResult Drama()
        {
            var films = db.Films.Where(x => x.FilmGenre.Id == 3).ToList();

            var model = Mapper.Map<List<FilmViewModel>>(films);

            return View(model);
        }

        public ActionResult Comedy()
        {
            var films = db.Films.Where(x => x.FilmGenre.Id == 4).ToList();

            var model = Mapper.Map<List<FilmViewModel>>(films);

            return View(model);
        }

        public ActionResult Horror()
        {
            var films = db.Films.Where(x => x.FilmGenre.Id == 6).ToList();

            var model = Mapper.Map<List<FilmViewModel>>(films);

            return View(model);
        }

        public ActionResult Action()
        {
            var films = db.Films.Where(x => x.FilmGenre.Id == 7).ToList();

            var model = Mapper.Map<List<FilmViewModel>>(films);

            return View(model);
        }
    }
}
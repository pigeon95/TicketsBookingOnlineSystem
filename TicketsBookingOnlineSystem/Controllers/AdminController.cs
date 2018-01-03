using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TicketsBookingOnlineSystem.Context;
using TicketsBookingOnlineSystem.Models;
using TicketsBookingOnlineSystem.ViewModels;

namespace TicketsBookingOnlineSystem.Controllers
{
    public class AdminController : Controller
    {
        CinemaDbContext db = new CinemaDbContext();

        // GET: Admin
        public ActionResult CreateFilm()
        {
            var model = new AddFilmViewModel
            {
                FilmGenres = db.FilmGenres.Select(x =>
                        new SelectListItem
                        {
                            Text = x.Name,
                            Value = x.Id.ToString()
                        }).ToList(),

                Creators = db.Creators.Select(x =>
                        new SelectListItem
                        {
                            Text = x.Name,
                            Value = x.Id.ToString()
                        }).ToList(),
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateFilm(AddFilmViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model = new AddFilmViewModel
                {
                    FilmGenres = db.FilmGenres.Select(x =>
                            new SelectListItem
                            {
                                Text = x.Name,
                                Value = x.Id.ToString()
                            }).ToList(),

                    Creators = db.Creators.Select(x =>
                            new SelectListItem
                            {
                                Text = x.Name,
                                Value = x.Id.ToString()
                            }).ToList(),
                };

                return View(model);
            }

            if (db.Films.Any(x => x.Title == model.Title))
            {
                model = new AddFilmViewModel
                {
                    FilmGenres = db.FilmGenres.Select(x =>
                            new SelectListItem
                            {
                                Text = x.Name,
                                Value = x.Id.ToString()
                            }).ToList(),

                    Creators = db.Creators.Select(x =>
                            new SelectListItem
                            {
                                Text = x.Name,
                                Value = x.Id.ToString()
                            }).ToList(),
                };

                ModelState.AddModelError(string.Empty, "Ten tytuł jest już używany dla innego filmu.");

                //komunikat ten tytuł jest zajety
                return View(model);
            }

            var fileName = Path.GetFileNameWithoutExtension(model.ImageFile.FileName);
            string extension = Path.GetExtension(model.ImageFile.FileName);
            fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
            model.Image = "~/Content/img/" + fileName;
            fileName = Path.Combine(Server.MapPath("~/Content/img/"), fileName);
            model.ImageFile.SaveAs(fileName);

            Film film = new Film();

            film.Duration = model.Duration;
            film.Title = model.Title;
            film.Description = model.Description;
            film.Image = model.Image;
            film.Deleted = model.Deleted;

            var filmGenre = db.FilmGenres.FirstOrDefault(c => c.Id == model.FilmGenre);
            film.FilmGenre = filmGenre;
            var creator = db.Creators.FirstOrDefault(c => c.Id == model.Creator);
            film.Creator = creator;

            db.Films.Add(film);
            db.SaveChanges();

            ViewBag.Message = "Film dodano do bazy.";

            //przekierowanie do lodowania
            ModelState.Clear();
            return View("EditFilmTest");
        }

        public ActionResult EditFilm(int id)
        {
            //var film = db.Films.FirstOrDefault(n => n.Id == id);

            var film = db.Films.FirstOrDefault(u => u.Id == id);

            //var model = new EditFilmViewModel
            //{
            //    FilmGenres = db.FilmGenres.Select(x =>
            //            new SelectListItem
            //            {
            //                Text = x.Name,
            //                Value = x.Id.ToString()
            //            }).ToList(),

            //    Creators = db.Creators.Select(x =>
            //            new SelectListItem
            //            {
            //                Text = x.Name,
            //                Value = x.Id.ToString()
            //            }).ToList(),
            //};


            var model = Mapper.Map<EditFilmViewModel>(film);

            return View(film);
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult EditFilm(AddFilmViewModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View();
        //    }

        //    var film = db.Films.FirstOrDefault(AddFilmViewModel => AddFilmViewModel.Id == id);

        //    if (film != null)
        //    {
        //        var model = Mapper.Map<List<FilmViewModel>>(film);

        //        db.Films.Attach(film);
        //        //var entry = db.Entry(film);

        //        //entry.Property(e => e.Title).IsModified = true;

        //        db.SaveChanges();
        //    }

        //    return View("FilmList");
        //}

        public ActionResult FilmList()
        {
            var films = db.Films.ToList();

            var model = Mapper.Map<List<FilmViewModel>>(films);

            return View(model);
        }



























        public ActionResult CreateSeance()
        {
            var model = new AddSeanceViewModel
            {
                Auditoriums = db.Auditoriums.Select(x =>
                        new SelectListItem
                        {
                            Text = x.Name,
                            Value = x.Id.ToString()
                        }).ToList(),

                Films = db.Films.Select(x =>
                        new SelectListItem
                        {
                            Text = x.Title,
                            Value = x.Id.ToString()
                        }).ToList()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateSeance(AddSeanceViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (db.Seances.Any(x => x.Name == model.Name))
            {
                ModelState.AddModelError(string.Empty, "Ten tytuł jest już używany dla innego filmu.");

                //komunikat ten tytuł jest zajety
                return View(model);
            }

            Seance entity = new Seance();

            entity.Name = model.Name;
            entity.Price = model.Price;
            entity.Deleted = model.Deleted;
            entity.Date = model.Date;

            var film = db.Films.FirstOrDefault(c => c.Id == model.Film);
            entity.Film = film;

            var auditorium = db.Auditoriums.FirstOrDefault(c => c.Id == model.Auditorium);
            entity.Auditorium = auditorium;

            db.Seances.Add(entity);
            db.SaveChanges();

            ViewBag.Message = "Seans dodano do bazy.";

            //przekierowanie do lodowania
            return View("EditSeance");
        }

        public ActionResult SeanceList()
        {
            var seances = db.Seances.ToList();

            var model = Mapper.Map<List<SeanceViewModel>>(seances);

            return View(model);
        }

        public ActionResult Users()
        {
            var users = db.Users.ToList();

            var model = Mapper.Map<List<UserViewModel>>(users);

            return View(model);
        }











        public ActionResult CreateCreator()
        {
            var model = new AddFilmViewModel
            {
                FilmGenres = db.FilmGenres.Select(x =>
                        new SelectListItem
                        {
                            Text = x.Name,
                            Value = x.Id.ToString()
                        }).ToList(),

                Creators = db.Creators.Select(x =>
                        new SelectListItem
                        {
                            Text = x.Name,
                            Value = x.Id.ToString()
                        }).ToList(),
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCreator(AddFilmViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (db.Films.Any(x => x.Title == model.Title))
            {
                ModelState.AddModelError(string.Empty, "Ten tytuł jest już używany dla innego filmu.");

                //komunikat ten tytuł jest zajety
                return View(model);
            }

            Film entity = new Film();

            entity.Duration = model.Duration;
            entity.Title = model.Title;
            entity.Deleted = model.Deleted;

            var filmGenre = db.FilmGenres.FirstOrDefault(c => c.Id == model.FilmGenre);
            entity.FilmGenre = filmGenre;

            //var filmCreator = new FilmCreator();
            //filmCreator.Film = entity;
            //filmCreator.Creator = db.Creators.FirstOrDefault(c => c.Id == model.Creator);

            //var creator = filmCreator.Creator;

            db.Films.Add(entity);
            //creator.FilmCreators.Add(filmCreator);

            db.SaveChanges();

            ViewBag.Message = "Film dodano do bazy.";

            //przekierowanie do lodowania
            return View("EditFilm");
        }

    }
  
}

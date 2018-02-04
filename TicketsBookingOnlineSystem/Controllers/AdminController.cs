using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TicketsBookingOnlineSystem.Context;
using TicketsBookingOnlineSystem.Models;
using TicketsBookingOnlineSystem.ViewModels;

namespace TicketsBookingOnlineSystem.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        CinemaDbContext db = new CinemaDbContext();

        //FILMY
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

            ModelState.Clear();
            return RedirectToAction("FilmList");
        }


        public ActionResult EditFilm(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var film = db.Films.Find(id);

            if (film == null)
            {
                return HttpNotFound();
            }
            else
            {
                var model = new AddFilmViewModel()
                {
                    Id = film.Id,
                    Deleted = film.Deleted,
                    Duration = film.Duration,
                    Image = film.Image,
                    Title = film.Title,
                    Description = film.Description,
                    Creator = film.Creator.Id,
                    FilmGenre = film.FilmGenre.Id,
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
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditFilm(AddFilmViewModel model)
        {
            //var film = db.Films.Find(id);
            var film = db.Films.FirstOrDefault(x => x.Id == model.Id);

            if (!ModelState.IsValid)
            {
                model.FilmGenres = db.FilmGenres.Select(x =>
                        new SelectListItem
                        {
                            Text = x.Name,
                            Value = x.Id.ToString()
                        }).ToList();

                model.Creators = db.Creators.Select(x =>
                        new SelectListItem
                        {
                            Text = x.Name,
                            Value = x.Id.ToString()
                        }).ToList();

                model.Image = film.Image;

                return View(model);
            }

            if (db.Films.Any(x => x.Title == model.Title))
            {
                model.FilmGenres = db.FilmGenres.Select(x =>
                        new SelectListItem
                        {
                            Text = x.Name,
                            Value = x.Id.ToString()
                        }).ToList();

                model.Creators = db.Creators.Select(x =>
                        new SelectListItem
                        {
                            Text = x.Name,
                            Value = x.Id.ToString()
                        }).ToList();

                model.Image = film.Image;

                ModelState.AddModelError(String.Empty, "Ten tytuł jest już w bazie.");

                return View(model);
            }

            if (Request.Files.Count > 0)
            {
                var file = Request.Files[0];

                if (file != null && file.ContentLength > 0)
                {
                    var fileName = Path.GetFileNameWithoutExtension(model.ImageFile.FileName);
                    string extension = Path.GetExtension(model.ImageFile.FileName);
                    fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    model.Image = "~/Content/img/" + fileName;
                    fileName = Path.Combine(Server.MapPath("~/Content/img/"), fileName);
                    model.ImageFile.SaveAs(fileName);

                    film.Image = model.Image;
                }
            }

            film.Deleted = model.Deleted;
            film.Duration = model.Duration;
            film.Title = model.Title;
            film.Description = model.Description;
            var filmGenre = db.FilmGenres.FirstOrDefault(c => c.Id == model.FilmGenre);
            film.FilmGenre = filmGenre;
            var creator = db.Creators.FirstOrDefault(c => c.Id == model.Creator);
            film.Creator = creator;

            db.Entry(film).State = EntityState.Modified;
            db.SaveChanges();
            ModelState.Clear();

            return RedirectToAction("FilmList");
        }

        [HttpPost, ActionName("DeleteFilm")]
        public ActionResult DeleteFilmConfirmed(int id)
        {
            var film = db.Films.Find(id);

            var seances = db.Seances.Where(s => s.Film.Id == film.Id).ToList();

            foreach (var seance in seances)
            {
                db.Seances.Remove(seance);
            }

            db.Films.Remove(film);
            db.SaveChanges();
            return RedirectToAction("FilmList");
        }

        public ActionResult FilmList()
        {
            var films = db.Films.ToList();

            var model = Mapper.Map<List<FilmViewModel>>(films);


            return View(model);
        }

        //SEANSE

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
                model = new AddSeanceViewModel
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

            if (db.Seances.Any(x => x.Name == model.Name))
            {
                model = new AddSeanceViewModel
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

                ModelState.AddModelError(string.Empty, "Ten seans jest już używany dla innego filmu.");

                //komunikat ten tytuł jest zajety
                return View(model);
            }

            Seance seance = new Seance();

            seance.Name = model.Name;
            seance.Price = model.Price;
            seance.Deleted = model.Deleted;
            seance.Date = model.Date;

            var film = db.Films.FirstOrDefault(c => c.Id == model.Film);
            seance.Film = film;

            var auditorium = db.Auditoriums.FirstOrDefault(c => c.Id == model.Auditorium);
            seance.Auditorium = auditorium;

            db.Seances.Add(seance);
            db.SaveChanges();

            return RedirectToAction("SeanceList");
        }


        public ActionResult EditSeance(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var seance = db.Seances.Find(id);

            if (seance == null)
            {
                return HttpNotFound();
            }
            else
            {
                var model = new AddSeanceViewModel()
                {
                    Id = seance.Id,
                    Name = seance.Name,
                    Price = seance.Price,
                    Date = seance.Date,
                    Film = seance.Film.Id,
                    Auditorium = seance.Auditorium.Id,
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
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditSeance(AddSeanceViewModel model)
        {
            var seance = db.Seances.FirstOrDefault(x => x.Id == model.Id);

            if (!ModelState.IsValid)
            {
                model.Auditoriums = db.Auditoriums.Select(x =>
                           new SelectListItem
                           {
                               Text = x.Name,
                               Value = x.Id.ToString()
                           }).ToList();

                model.Films = db.Films.Select(x =>
                        new SelectListItem
                        {
                            Text = x.Title,
                            Value = x.Id.ToString()
                        }).ToList();

                return View(model);
            }

            if (db.Seances.Any(x => x.Name == model.Name))
            {
                model.Auditoriums = db.Auditoriums.Select(x =>
                           new SelectListItem
                           {
                               Text = x.Name,
                               Value = x.Id.ToString()
                           }).ToList();

                model.Films = db.Films.Select(x =>
                        new SelectListItem
                        {
                            Text = x.Title,
                            Value = x.Id.ToString()
                        }).ToList();

                ModelState.AddModelError(String.Empty, "Ten seans jest już używany dla innego filmu.");

                return View(model);
            }

            seance.Id = model.Id;
            seance.Name = model.Name;
            seance.Price = model.Price;
            seance.Date = model.Date;
            var auditorium = db.Auditoriums.FirstOrDefault(c => c.Id == model.Auditorium);
            seance.Auditorium = auditorium;
            var film = db.Films.FirstOrDefault(c => c.Id == model.Film);
            seance.Film = film;

            db.SaveChanges();

            return RedirectToAction("SeanceList");
        }


        public ActionResult SeanceList()
        {
            var seances = db.Seances.ToList();

            var model = Mapper.Map<List<SeanceViewModel>>(seances);

            return View(model);
        }

        [HttpPost, ActionName("DeleteSeance")]
        public ActionResult DeleteSeanceConfirmed(int id)
        {
            var seance = db.Seances.Find(id);
            db.Seances.Remove(seance);
            db.SaveChanges();
            return RedirectToAction("SeanceList");
        }

        //REZYSERZY
        public ActionResult CreateCreator()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCreator(AddCreatorViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (db.Creators.Any(x => x.Name == model.Name))
            {
                ModelState.AddModelError(String.Empty, "Ta tożsamość jest już używana dla innego twórcy.");

                return View(model);
            }

            Creator creator = new Creator();

            creator.Id = model.Id;
            creator.Name = model.Name;
            creator.Description = model.Description;
            creator.Deleted = model.Deleted;

            db.Creators.Add(creator);
            db.SaveChanges();

            return RedirectToAction("CreatorList");
        }

        public ActionResult EditCreator(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var creator = db.Creators.Find(id);

            if (creator == null)
            {
                return HttpNotFound();
            }
            else
            {
                var model = new AddCreatorViewModel()
                {
                    Id = creator.Id,
                    Name = creator.Name,
                    Description = creator.Description,
                    Deleted = creator.Deleted,
                };

                return View(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCreator(AddCreatorViewModel model)
        {
            var creator = db.Creators.FirstOrDefault(x => x.Id == model.Id);

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (db.Creators.Any(x => x.Name == model.Name))
            {
                ModelState.AddModelError(String.Empty, "Ta tożsamość jest już używana dla innego twórcy.");

                return View(model);
            }

            creator.Id = model.Id;
            creator.Name = model.Name;
            creator.Description = model.Description;
            creator.Deleted = model.Deleted;

            db.SaveChanges();

            return RedirectToAction("CreatorList");
        }

        public ActionResult CreatorList()
        {
            var creators = db.Creators.ToList();

            var model = Mapper.Map<List<AddCreatorViewModel>>(creators);

            return View(model);
        }

        [HttpPost, ActionName("DeleteCreator")]
        public ActionResult DeleteCreatorConfirmed(int id)
        {
            var creator = db.Creators.Find(id);

            var films = db.Films.Where(s => s.Creator.Id == creator.Id).ToList();

            foreach (var film in films)
            {
                var seances = db.Seances.Where(f => f.Film.Id == film.Id).ToList();
                db.Seances.RemoveRange(seances);
            }

            db.Films.RemoveRange(films);
            db.Creators.Remove(creator);
            db.SaveChanges();
            return RedirectToAction("CreatorList");
        }

        //UZYTKOWNICY
        public ActionResult EditUser(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var user = db.Users.Find(id);

            if (user == null)
            {
                return HttpNotFound();
            }
            else
            {
                var model = new UserEditViewModel()
                {
                    Id = user.Id,
                    Name = user.Name,
                    Surname = user.Surname,
                    Address = user.Address,
                    Phone = user.Phone,
                    BirthDate = user.BirthDate,
                    Email = user.Email,
                    City = user.City.Name
                };

                return View(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditUser(UserEditViewModel model)
        {
            var user = db.Users.FirstOrDefault(x => x.Id == model.Id);

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (db.Users.Any(x => x.Email == model.Email))
            {
                ModelState.AddModelError(String.Empty, "Ten seans jest już używany dla innego filmu.");

                return View(model);
            }

            user.Id = model.Id;
            user.Name = model.Name;
            user.Surname = model.Surname;
            user.Address = model.Address;
            user.Phone = model.Phone;
            user.BirthDate = model.BirthDate;
            user.Email = model.Email;

            var city = db.Cities.FirstOrDefault(c => c.Name == model.City);

            if (city == null)
            {
                city = new City();
                city.Name = model.City;
                user.City = city;
                db.Cities.Add(city);
            }
            user.City = city;

            db.SaveChanges();

            return RedirectToAction("Users");
        }

        public ActionResult Users()
        {
            var users = db.Users.ToList();

            var model = Mapper.Map<List<UserViewModel>>(users);

            return View(model);
        }

        [HttpPost, ActionName("DeleteUser")]
        public ActionResult DeleteUserConfirmed(int id)
        {
            var user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Users");
        }

    }

}


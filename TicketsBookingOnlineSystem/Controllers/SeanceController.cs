using AutoMapper;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Web;
using System.Web.Mvc;
using TicketsBookingOnlineSystem.Context;
using TicketsBookingOnlineSystem.Models;
using TicketsBookingOnlineSystem.ViewModels;

namespace TicketsBookingOnlineSystem.Controllers
{
    [Authorize]
    public class SeanceController : Controller
    {
        CinemaDbContext db = new CinemaDbContext();
        // GET: Seance
        public ActionResult Index(int id)
        {
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
        public ActionResult Index(NewReservationModel model)
        {
            var spots = new List<Spot>();

            if (model.Spots == null)
            {
                return Json(new { isok = true, message = "Zajmij miejsce." });
                //return RedirectToAction("Index");
            }

            var seance = db.Seances.FirstOrDefault(x => x.Id == model.Id);
            var auditorium = seance.Auditorium;

            foreach (var id in model.Spots.Split(','))
            {
                var tmp = id.Split('_');
                var row = Int32.Parse(tmp[1]);
                var col = Int32.Parse(tmp[2]);

                var spot = auditorium
                    .Spots
                    .FirstOrDefault(x => x.Row == row && x.Col == col);

                spots.Add(spot);

                if (db.Seances.FirstOrDefault(s => s.Id == model.Id).Reservations.Any(t => t.Spots.Any(s => s.Row == row && s.Col == col)))
                {
                    return Json(new { isok = true, message = "To miejsce jest już zajęte." });
                }
            }

            int userId = HttpContext.User.Identity.GetUserId<int>();
            var user = db.Users.FirstOrDefault(x => x.Id == userId);

            Reservation reservation = new Reservation();
            reservation.Seance = seance;
            reservation.Spots = spots;
            reservation.UserId = userId;
            reservation.Date = seance.Date;

            db.Reservations.Add(reservation);
            db.SaveChanges();

            if (user.Role == UserRoleEnum.Dealer)
            {
                return Json(new
                {
                    isok = true,
                    message = "Dokonano poprawnej rezerwacji miejsca dla klienta."
                });
            }
            else
            {
                SendInformationkEmail(user.Email, seance.Name, seance.Date.ToString("dd.MM o HH:mm"), user.Name);

                return Json(new
                {
                    isok = true,
                    message = "Dokonano poprawnej rezerwacji." + "</br>"
                    + "Na Twój adres email przyjdzie QR kod, który zostanie przeskanowany na miejscu przez pracownika kina."
                });
            }

        }

        //JsonResult
        public ActionResult GetReservedSpots(int id)
        {
            var reservations = db.Reservations.Where(x => x.SeanceId == id).ToList();

            var model = new List<JsonReservationInfoViewModel>();

            foreach (var reservation in reservations)
            {
                var spots = reservation.Spots;
                foreach (var spot in spots)
                {
                    model.Add(new JsonReservationInfoViewModel
                    {
                        Col = spot.Col,
                        Row = spot.Row
                    });
                }
            }

            var json = JsonConvert.SerializeObject(model);

            return Content(json, "application/json");
        }

        [NonAction]
        public void SendInformationkEmail(string email, string seanceName, string seanceDate, string userName)
        {
            var fileName = Server.MapPath("~/Content/img/qr-code-sample.jpg");
            var fromEmail = new MailAddress("kino.email404@gmail.com", "Kino");
            var toEmail = new MailAddress(email);
            var fromEmailPassword = "kino_email_testowy44";
            string subject = "Zarezerwowałeś/aś bilet na seans!";

            string body = "Witaj " + userName + "," + "<br/><br/>zarezerwowałeś/aś bilet na seans: <b>" + seanceName + "</b>, który odbędzie się: " + seanceDate + "."
                + "<br/>Poniżej widnieje QR kod, który należy pokazać na miejscu, w celu przeskanowania go przez pracownika kina.";

            var attachment = new Attachment(fileName, MediaTypeNames.Image.Jpeg);

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com", 
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromEmail.Address, fromEmailPassword)
            };

            var message = new MailMessage();
            message.Attachments.Add(attachment);
            message.To.Add(toEmail);
            message.Subject = subject;
            message.Body = body;
            message.IsBodyHtml = true;

            smtp.Send(message);
        }
    }
}
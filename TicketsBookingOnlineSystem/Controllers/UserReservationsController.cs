using AutoMapper;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using TicketsBookingOnlineSystem.Context;
using TicketsBookingOnlineSystem.ViewModels;

namespace TicketsBookingOnlineSystem.Controllers
{
    [Authorize(Roles = "User, Dealer")]
    public class UserReservationsController : Controller
    {
        CinemaDbContext db = new CinemaDbContext();

        // GET: UserReservations
        public ActionResult Index()
        {
            int userId = HttpContext.User.Identity.GetUserId<int>();

            var reservations = db.Reservations
                .Include(s => s.Spots)
                .Include(s => s.Seance)
                .Include(s => s.Seance.Auditorium)
                .Include(f => f.Seance.Film)
                .ToList()
                .Where(r => r.UserId == userId);

            var model = Mapper.Map<List<UserReservationsViewModel>>(reservations);

            return View(model);
        }

        [HttpPost, ActionName("DeleteReservation")]
        public ActionResult DeleteReservationConfirmed(int id)
        {
            int userId = HttpContext.User.Identity.GetUserId<int>();
            var user = db.Users.FirstOrDefault(x => x.Id == userId);

            var reservation = db.Reservations.Find(id);
            db.Reservations.Remove(reservation);
            db.SaveChanges();

            SendInformationkEmail(user.Email);

            return RedirectToAction("Index");
        }

        [NonAction]
        public void SendInformationkEmail(string email)
        {
            var fromEmail = new MailAddress("kino.email404@gmail.com", "Kino");
            var toEmail = new MailAddress(email);
            var fromEmailPassword = "kino_email_testowy44";
            string subject = "Odwołana rezerwacja";

            string body = "Odwołano rezerwację.";

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
            message.To.Add(toEmail);
            message.Subject = subject;
            message.Body = body;
            message.IsBodyHtml = true;

            smtp.Send(message);
        }
    }
}
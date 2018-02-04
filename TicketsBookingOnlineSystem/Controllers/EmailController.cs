using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TicketsBookingOnlineSystem.ViewModels;

namespace TicketsBookingOnlineSystem.Controllers
{
    public class EmailController : Controller
    {
        [Authorize(Roles = "Admin, Dealer")]
        public ActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Dealer")]
        public async Task<ActionResult> Send(EmailFormModel model)
        {
            if (ModelState.IsValid)
            {
                var body = "<p>Witaj {1}</p><p>{2}</p>";
                var message = new MailMessage();
                message.To.Add(new MailAddress(model.Email));  // replace with valid value 
                message.Subject = model.Subject;
                message.Body = string.Format(body, model.Subject, model.Email, model.Message);
                message.IsBodyHtml = true;

                using (var smtp = new SmtpClient())
                {
                    await smtp.SendMailAsync(message);
                    //return RedirectToAction("Sent");
                    return Json(new { isok = true, message = "Wiadomość została wysłana poprawnie." });
                }
            }
            return View(model);
        }

        public ActionResult Sent()
        {
            return View();
        }
    }
}

//kino_email_testowy44
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;
using TicketsBookingOnlineSystem.Context;
using TicketsBookingOnlineSystem.Models;
using TicketsBookingOnlineSystem.ViewModels;

namespace TicketsBookingOnlineSystem.Controllers
{
    public class AccountController : Controller
    {
        CinemaDbContext db = new CinemaDbContext();

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserLoginViewModel model, string ReturnUrl = "")
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            using (db)
            {
                var v = db.Users.FirstOrDefault(a => a.Email == model.Email);
                if (v == null)
                {
                    ModelState.AddModelError("", "Podany login lub hasło jest nieprawidłowe.");
                    return View(model);
                }

                if (string.Compare(Crypto.Hash(model.Password), v.Password) == 0 )
                {
                    if(v.IsEmailVerified == false)
                    {
                        ModelState.AddModelError("", "Aby się zalogować, musisz najpierw aktywować konto.");
                        return View(model);
                    }

                    int timeout = model.RemeberMe ? 525600 : 20; // 1 year
                    var ticket = new FormsAuthenticationTicket(model.Email, model.RemeberMe, timeout);
                    string encrypted = FormsAuthentication.Encrypt(ticket);
                    var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encrypted);
                    cookie.Expires = DateTime.Now.AddMinutes(timeout);
                    cookie.HttpOnly = true;
                    Response.Cookies.Add(cookie);

                    List<Claim> claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, v.Email),
                        new Claim(ClaimTypes.NameIdentifier, v.Id.ToString()),
                        new Claim(ClaimTypes.Role, v.Role.ToString()),
                    };

                    var identity = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);

                    Request.GetOwinContext().Authentication.SignIn(identity);

                    if (Url.IsLocalUrl(ReturnUrl))
                    {
                        return Redirect(ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Repertoire", "Repertoire");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Podany login lub hasło jest nieprawidłowe.");
                    return View(model);
                }

            }
        }

        [Authorize]
        public ActionResult AfterLogin()
        {
            return View();
        }

        [Authorize]
        public ActionResult LogOut()
        {
            Request.GetOwinContext().Authentication.SignOut();
            //FormsAuthentication.SignOut();
            //Session.Abandon(); // it will clear the session at the end of request
            return RedirectToAction("Login");
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Exclude = "IsEmailVerified,ActivationCode")] UserRegisterViewModel model)
        {
            bool Status = false;

            if (!ModelState.IsValid)
            {
                return View();
            }

            User user = new User();

            user.ActivationCode = Guid.NewGuid();

            user.Name = model.Name;
            user.Surname = model.Surname;

            user.Password = Crypto.Hash(model.Password);
            model.ConfirmPassword = Crypto.Hash(model.ConfirmPassword);
            user.IsEmailVerified = false;

            user.BirthDate = model.BirthDate;
            user.Address = model.Address;
            user.Phone = model.Phone;
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

            if (db.Users.Any(x => x.Email == model.Email))
            {
                ModelState.AddModelError(string.Empty, "");
                return View(model);
            }

            db.Users.Add(user);
            db.SaveChanges();

            SendVerificationSendLinkEmail(user.Email, user.ActivationCode.ToString());
            //message = "Rejestracja przebiegła pomyślnie. Link aktywacyjny " +
            //    " został wysłany na twój email:" + user.Email;
            Status = true;


            return Json(new
            {
                isok = true,
                message = "Rejestracja przebiegła pomyślnie. Link aktywacyjny został wysłany na twój Email: "
                + user.Email
            });

            //return RedirectToAction("Login");
        }

        public ActionResult VerifyAccount(string id)
        {
            bool Status = false;

            using (db)
            {
                db.Configuration.ValidateOnSaveEnabled = false;

                var v = db.Users.Where(a => a.ActivationCode == new Guid(id)).FirstOrDefault();
                if (v != null)
                {
                    v.IsEmailVerified = true;
                    db.SaveChanges();
                    Status = true;
                }
                else
                {
                    ViewBag.Message = "Nieprawidłowe przekierowanie";
                }
            }
            ViewBag.Status = true;

            return View();
        }

        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ForgotPassword(string email)
        {
            string message = "";
            bool status = false;

            using (db)
            {
                var account = db.Users.Where(a => a.Email == email).FirstOrDefault();
                if(account != null)
                {
                    string resetCode = Guid.NewGuid().ToString();
                    SendVerificationSendLinkEmail(account.Email, resetCode, "ResetPassword");
                    ModelState.AddModelError("", "Link resetujący hasło został wysłany na podany Email.");
                    account.ResetPasswordCode = resetCode;

                    db.Configuration.ValidateOnSaveEnabled = false;

                    db.SaveChanges();
                }
                else
                {
                    ModelState.AddModelError("", "Nie znaleziono takiego konta.");
                }
            }

                return View();
        }

        public ActionResult ResetPassword(string id)
        {
            using (db)
            {
                var user = db.Users.Where(a => a.ResetPasswordCode == id).FirstOrDefault();
                if(user != null)
                {
                    ResetPasswordViewModel model = new ResetPasswordViewModel();
                    model.ResetPasswordCode = id;
                    return View(model);
                }
                else
                {
                    return HttpNotFound();
                }
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ResetPassword(ResetPasswordViewModel model)
        {
            var message = "";
            if(!ModelState.IsValid)
            {
                message = "Coś poszło nie tak.";
                ViewBag.Message = message;
                return View(model);
            }

            using (db)
            {
                var user = db.Users.Where(a => a.ResetPasswordCode == model.ResetPasswordCode).FirstOrDefault();
                if (user != null)
                {
                    user.Password = Crypto.Hash(model.NewPassword);
                    user.ResetPasswordCode = "";

                    db.Configuration.ValidateOnSaveEnabled = false;
                    db.SaveChanges();
                    message = "Hasło zostało zaktualizowane.";
                }
            }

            ViewBag.Message = message;
            return View(model);

        }

        [NonAction]
        public void SendVerificationSendLinkEmail(string email, string activationCode, string emailFor = "VerifyAccount")
        {
            var verifyUrl = "/Account/" + emailFor + "/" + activationCode;
            var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyUrl);

            var fromEmail = new MailAddress("kino.email404@gmail.com", "Kino");
            var toEmail = new MailAddress(email);
            var fromEmailPassword = "kino_email_testowy44";

            string subject = "";
            string body = "";
            if (emailFor == "VerifyAccount")
            {
                subject = "Twoje konto zostało pomyślnie utworzone!";

                body = "<br/><br/>Aby aktywować swoje konto, kliknij" +
                    "poniższy link aktywacyjny." +
                    "<br/><br/><a href='" + link + "'>" + link + "</a>";
            }
            else if (emailFor == "ResetPassword")
            {
                subject = "Reset hasła";

                body = "Witaj, <br/><br/> Poniżej znajduje się link z resetem. Kliknij, aby zresetować swoje hasło."
                    + "<br/><br/><a href='" + link + "'>" + link + "</a>";
            }

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromEmail.Address, fromEmailPassword)
            };

            using (var message = new MailMessage(fromEmail, toEmail)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            })

                smtp.Send(message);
        }

        public JsonResult IsUserExists(string Email)
        {
            //check if any of the Email matches the Email specified in the Parameter using the ANY extension method.  
            return Json(!db.Users.Any(x => x.Email == Email), JsonRequestBehavior.AllowGet);
        }
    }
}
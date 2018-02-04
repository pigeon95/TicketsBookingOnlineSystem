﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TicketsBookingOnlineSystem.Models;

namespace TicketsBookingOnlineSystem.ViewModels
{
    public class UserEditViewModel
    {
        public int Id { get; set; }
        [DisplayName("Imię")]
        [Required(ErrorMessage = "Proszę podać swoje imię.", AllowEmptyStrings = false)]
        public string Name { get; set; }
        [DisplayName("Nazwisko")]
        [Required(ErrorMessage = "Proszę podać swoje nazwisko.", AllowEmptyStrings = false)]
        public string Surname { get; set; }
        //[DisplayName("Hasło")]
        //[Required(ErrorMessage = "Proszę podać swoje hasło.", AllowEmptyStrings = false)]
        //[StringLength(50, MinimumLength = 8, ErrorMessage = "Hasło musi posiadać conajmniej 8 znaków.")]
        //public string Password { get; set; }
        //[DisplayName("Powtórz hasło")]
        //[Compare("Password", ErrorMessage = "Podane hasło się nie zgadza.")]
        //public string ConfirmPassword { get; set; }
        [DisplayName("Adres")]
        public string Address { get; set; }
        [DisplayName("Telefon")]
        public string Phone { get; set; }
        [DisplayName("Data urodzenia")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? BirthDate { get; set; }
        [DisplayName("Miasto")]
        [Required(ErrorMessage = "Proszę podać nazwę swojej miejscowości.", AllowEmptyStrings = false)]
        public string City { get; set; }
        [System.Web.Mvc.Remote("IsUserExists", "Register", ErrorMessage = "Użytkownik o podanym emailu już istnieje, proszę wybrać inny.")]
        [Required(ErrorMessage = "Proszę podać swój email")]
        [EmailAddress(ErrorMessage = "Podany email jest nieprawidłowy")]
        public string Email { get; set; }
    }
}
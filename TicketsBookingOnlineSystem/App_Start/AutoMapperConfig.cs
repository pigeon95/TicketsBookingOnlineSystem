using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TicketsBookingOnlineSystem.Models;
using TicketsBookingOnlineSystem.ViewModels;

namespace TicketsBookingOnlineSystem.App_Start
{
    public class AutoMapperConfig
    {
        public static void Config()
        {
            Mapper.Initialize(cfg => {
                cfg.CreateMap<Film, FilmDetailsViewModel>();
                cfg.CreateMap<Film, FilmViewModel>();
                cfg.CreateMap<Film, FilmForDateViewModel>();
                cfg.CreateMap<Seance, MenuDateViewModel>();
                cfg.CreateMap<Seance, SeanceViewModel>();
                cfg.CreateMap<FilmImage, FilmImageViewModel>();
                cfg.CreateMap<Reservation, JsonReservationInfoViewModel>();
                cfg.CreateMap<User, UserEditViewModel>()
                    .ForMember(x => x.Password, opt => opt.Ignore());

                //cfg.AddProfile<FooProfile>();
            });

        }
    }
}
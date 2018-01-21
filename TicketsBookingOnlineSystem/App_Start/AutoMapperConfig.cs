using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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
                //cfg.CreateMap<Film, AddFilmViewModel>();
                cfg.CreateMap<Film, AddFilmViewModel>()
                    .ForMember(dest => dest.FilmGenre, opt => opt.Ignore())
                    .ForMember(dest => dest.Creator, opt => opt.Ignore());
                cfg.CreateMap<Seance, MenuDateViewModel>();
                cfg.CreateMap<Seance, SeanceViewModel>();
                cfg.CreateMap<User, UserViewModel>();
                cfg.CreateMap<User, UserRegisterViewModel>();
                cfg.CreateMap<Creator, AddCreatorViewModel>();
                cfg.CreateMap<User, UserEditViewModel>()
                    .ForMember(x => x.Password, opt => opt.Ignore());
            });

        }
    }
}


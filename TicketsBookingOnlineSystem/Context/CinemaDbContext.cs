using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TicketsBookingOnlineSystem.Models;

namespace TicketsBookingOnlineSystem.Context
{
    public class CinemaDbContext : DbContext
    {
            public DbSet<Film> Films { get; set; }
            public DbSet<Auditorium> Auditoriums { get; set; }
            public DbSet<City> Cities { get; set; }
            public DbSet<Creator> Creators { get; set; }
            public DbSet<CreatorImage> CreatorImages { get; set; }
            public DbSet<FilmImage> FilmImages { get; set; }
            public DbSet<Reservation> Reservations { get; set; }
            public DbSet<Row> Rows { get; set; }
            public DbSet<Seance> Seances { get; set; }
            public DbSet<User> Users { get; set; }

            public CinemaDbContext() : base("DefaultConnection") { }
    }
}
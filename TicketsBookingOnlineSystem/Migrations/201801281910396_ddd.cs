namespace TicketsBookingOnlineSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ddd : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Auditoriums",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        LocationArrangement = c.String(),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Seances",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Date = c.DateTime(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                        Auditorium_Id = c.Int(),
                        Film_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Auditoriums", t => t.Auditorium_Id)
                .ForeignKey("dbo.Films", t => t.Film_Id)
                .Index(t => t.Auditorium_Id)
                .Index(t => t.Film_Id);
            
            CreateTable(
                "dbo.Films",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Duration = c.Time(nullable: false, precision: 7),
                        Title = c.String(),
                        Deleted = c.Boolean(nullable: false),
                        Description = c.String(),
                        Image = c.String(),
                        Creator_Id = c.Int(),
                        FilmGenre_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Creators", t => t.Creator_Id)
                .ForeignKey("dbo.FilmGenres", t => t.FilmGenre_Id)
                .Index(t => t.Creator_Id)
                .Index(t => t.FilmGenre_Id);
            
            CreateTable(
                "dbo.Creators",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.FilmGenres",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Deleted = c.Boolean(nullable: false),
                        FilmGenreId = c.String(),
                        Value = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Reservations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        SeanceId = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Seances", t => t.SeanceId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.SeanceId);
            
            CreateTable(
                "dbo.Spots",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Col = c.Int(nullable: false),
                        Row = c.Int(nullable: false),
                        Name = c.String(),
                        Deleted = c.Boolean(nullable: false),
                        Auditorium_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Auditoriums", t => t.Auditorium_Id)
                .Index(t => t.Auditorium_Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Surname = c.String(),
                        Password = c.String(),
                        Address = c.String(),
                        Phone = c.String(),
                        Email = c.String(),
                        BirthDate = c.DateTime(),
                        Deleted = c.Boolean(nullable: false),
                        IsEmailVerified = c.Boolean(nullable: false),
                        ActivationCode = c.Guid(nullable: false),
                        Role = c.Int(nullable: false),
                        City_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cities", t => t.City_Id)
                .Index(t => t.City_Id);
            
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SpotReservations",
                c => new
                    {
                        Spot_Id = c.Int(nullable: false),
                        Reservation_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Spot_Id, t.Reservation_Id })
                .ForeignKey("dbo.Spots", t => t.Spot_Id, cascadeDelete: true)
                .ForeignKey("dbo.Reservations", t => t.Reservation_Id, cascadeDelete: true)
                .Index(t => t.Spot_Id)
                .Index(t => t.Reservation_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reservations", "UserId", "dbo.Users");
            DropForeignKey("dbo.Users", "City_Id", "dbo.Cities");
            DropForeignKey("dbo.SpotReservations", "Reservation_Id", "dbo.Reservations");
            DropForeignKey("dbo.SpotReservations", "Spot_Id", "dbo.Spots");
            DropForeignKey("dbo.Spots", "Auditorium_Id", "dbo.Auditoriums");
            DropForeignKey("dbo.Reservations", "SeanceId", "dbo.Seances");
            DropForeignKey("dbo.Seances", "Film_Id", "dbo.Films");
            DropForeignKey("dbo.Films", "FilmGenre_Id", "dbo.FilmGenres");
            DropForeignKey("dbo.Films", "Creator_Id", "dbo.Creators");
            DropForeignKey("dbo.Seances", "Auditorium_Id", "dbo.Auditoriums");
            DropIndex("dbo.SpotReservations", new[] { "Reservation_Id" });
            DropIndex("dbo.SpotReservations", new[] { "Spot_Id" });
            DropIndex("dbo.Users", new[] { "City_Id" });
            DropIndex("dbo.Spots", new[] { "Auditorium_Id" });
            DropIndex("dbo.Reservations", new[] { "SeanceId" });
            DropIndex("dbo.Reservations", new[] { "UserId" });
            DropIndex("dbo.Films", new[] { "FilmGenre_Id" });
            DropIndex("dbo.Films", new[] { "Creator_Id" });
            DropIndex("dbo.Seances", new[] { "Film_Id" });
            DropIndex("dbo.Seances", new[] { "Auditorium_Id" });
            DropTable("dbo.SpotReservations");
            DropTable("dbo.Cities");
            DropTable("dbo.Users");
            DropTable("dbo.Spots");
            DropTable("dbo.Reservations");
            DropTable("dbo.FilmGenres");
            DropTable("dbo.Creators");
            DropTable("dbo.Films");
            DropTable("dbo.Seances");
            DropTable("dbo.Auditoriums");
        }
    }
}

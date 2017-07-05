namespace TicketsBookingOnlineSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialModels : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Auditoriums",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Rows",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RowId = c.String(),
                        SpotCount = c.Int(nullable: false),
                        Name = c.String(),
                        Deleted = c.Boolean(nullable: false),
                        Auditorium_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Auditoriums", t => t.Auditorium_Id)
                .Index(t => t.Auditorium_Id);
            
            CreateTable(
                "dbo.Spots",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Deleted = c.Boolean(nullable: false),
                        Row_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Rows", t => t.Row_Id)
                .Index(t => t.Row_Id);
            
            CreateTable(
                "dbo.Reservations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        Spot = c.Int(nullable: false),
                        SeanceId = c.Int(nullable: false),
                        Row = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Seances", t => t.SeanceId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.SeanceId);
            
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
                    })
                .PrimaryKey(t => t.Id);
            
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
                "dbo.CreatorImages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Alt = c.String(),
                        Description = c.String(),
                        Deleted = c.Boolean(nullable: false),
                        Creator_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Creators", t => t.Creator_Id)
                .Index(t => t.Creator_Id);
            
            CreateTable(
                "dbo.FilmImages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Alt = c.String(),
                        Description = c.String(),
                        Deleted = c.Boolean(nullable: false),
                        Film_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Films", t => t.Film_Id)
                .Index(t => t.Film_Id);
            
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
                "dbo.CreatorFilms",
                c => new
                    {
                        Creator_Id = c.Int(nullable: false),
                        Film_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Creator_Id, t.Film_Id })
                .ForeignKey("dbo.Creators", t => t.Creator_Id, cascadeDelete: true)
                .ForeignKey("dbo.Films", t => t.Film_Id, cascadeDelete: true)
                .Index(t => t.Creator_Id)
                .Index(t => t.Film_Id);
            
            CreateTable(
                "dbo.ReservationSpots",
                c => new
                    {
                        Reservation_Id = c.Int(nullable: false),
                        Spot_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Reservation_Id, t.Spot_Id })
                .ForeignKey("dbo.Reservations", t => t.Reservation_Id, cascadeDelete: true)
                .ForeignKey("dbo.Spots", t => t.Spot_Id, cascadeDelete: true)
                .Index(t => t.Reservation_Id)
                .Index(t => t.Spot_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Spots", "Row_Id", "dbo.Rows");
            DropForeignKey("dbo.Reservations", "UserId", "dbo.Users");
            DropForeignKey("dbo.Users", "City_Id", "dbo.Cities");
            DropForeignKey("dbo.ReservationSpots", "Spot_Id", "dbo.Spots");
            DropForeignKey("dbo.ReservationSpots", "Reservation_Id", "dbo.Reservations");
            DropForeignKey("dbo.Reservations", "SeanceId", "dbo.Seances");
            DropForeignKey("dbo.Seances", "Film_Id", "dbo.Films");
            DropForeignKey("dbo.FilmImages", "Film_Id", "dbo.Films");
            DropForeignKey("dbo.CreatorFilms", "Film_Id", "dbo.Films");
            DropForeignKey("dbo.CreatorFilms", "Creator_Id", "dbo.Creators");
            DropForeignKey("dbo.CreatorImages", "Creator_Id", "dbo.Creators");
            DropForeignKey("dbo.Seances", "Auditorium_Id", "dbo.Auditoriums");
            DropForeignKey("dbo.Rows", "Auditorium_Id", "dbo.Auditoriums");
            DropIndex("dbo.ReservationSpots", new[] { "Spot_Id" });
            DropIndex("dbo.ReservationSpots", new[] { "Reservation_Id" });
            DropIndex("dbo.CreatorFilms", new[] { "Film_Id" });
            DropIndex("dbo.CreatorFilms", new[] { "Creator_Id" });
            DropIndex("dbo.Users", new[] { "City_Id" });
            DropIndex("dbo.FilmImages", new[] { "Film_Id" });
            DropIndex("dbo.CreatorImages", new[] { "Creator_Id" });
            DropIndex("dbo.Seances", new[] { "Film_Id" });
            DropIndex("dbo.Seances", new[] { "Auditorium_Id" });
            DropIndex("dbo.Reservations", new[] { "SeanceId" });
            DropIndex("dbo.Reservations", new[] { "UserId" });
            DropIndex("dbo.Spots", new[] { "Row_Id" });
            DropIndex("dbo.Rows", new[] { "Auditorium_Id" });
            DropTable("dbo.ReservationSpots");
            DropTable("dbo.CreatorFilms");
            DropTable("dbo.Cities");
            DropTable("dbo.Users");
            DropTable("dbo.FilmImages");
            DropTable("dbo.CreatorImages");
            DropTable("dbo.Creators");
            DropTable("dbo.Films");
            DropTable("dbo.Seances");
            DropTable("dbo.Reservations");
            DropTable("dbo.Spots");
            DropTable("dbo.Rows");
            DropTable("dbo.Auditoriums");
        }
    }
}

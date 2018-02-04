namespace TicketsBookingOnlineSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FilmGenres", "FilmGenreId", c => c.String());
            AddColumn("dbo.FilmGenres", "Value", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.FilmGenres", "Value");
            DropColumn("dbo.FilmGenres", "FilmGenreId");
        }
    }
}

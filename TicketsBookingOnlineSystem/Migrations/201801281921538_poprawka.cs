namespace TicketsBookingOnlineSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class poprawka : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.FilmGenres", "FilmGenreId");
            DropColumn("dbo.FilmGenres", "Value");
        }
        
        public override void Down()
        {
            AddColumn("dbo.FilmGenres", "Value", c => c.String());
            AddColumn("dbo.FilmGenres", "FilmGenreId", c => c.String());
        }
    }
}

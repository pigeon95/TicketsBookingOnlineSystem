namespace TicketsBookingOnlineSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migracja2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FilmGenres",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Films", "FilmGenre_Id", c => c.Int());
            CreateIndex("dbo.Films", "FilmGenre_Id");
            AddForeignKey("dbo.Films", "FilmGenre_Id", "dbo.FilmGenres", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Films", "FilmGenre_Id", "dbo.FilmGenres");
            DropIndex("dbo.Films", new[] { "FilmGenre_Id" });
            DropColumn("dbo.Films", "FilmGenre_Id");
            DropTable("dbo.FilmGenres");
        }
    }
}

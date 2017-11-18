namespace TicketsBookingOnlineSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig5 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.FilmCreatorFilms", "FilmCreator_Id", "dbo.FilmCreators");
            DropForeignKey("dbo.FilmCreatorFilms", "Film_Id", "dbo.Films");
            DropIndex("dbo.FilmCreatorFilms", new[] { "FilmCreator_Id" });
            DropIndex("dbo.FilmCreatorFilms", new[] { "Film_Id" });
            CreateIndex("dbo.FilmCreators", "FilmId");
            AddForeignKey("dbo.FilmCreators", "FilmId", "dbo.Films", "Id", cascadeDelete: true);
            DropTable("dbo.FilmCreatorFilms");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.FilmCreatorFilms",
                c => new
                    {
                        FilmCreator_Id = c.Int(nullable: false),
                        Film_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.FilmCreator_Id, t.Film_Id });
            
            DropForeignKey("dbo.FilmCreators", "FilmId", "dbo.Films");
            DropIndex("dbo.FilmCreators", new[] { "FilmId" });
            CreateIndex("dbo.FilmCreatorFilms", "Film_Id");
            CreateIndex("dbo.FilmCreatorFilms", "FilmCreator_Id");
            AddForeignKey("dbo.FilmCreatorFilms", "Film_Id", "dbo.Films", "Id", cascadeDelete: true);
            AddForeignKey("dbo.FilmCreatorFilms", "FilmCreator_Id", "dbo.FilmCreators", "Id", cascadeDelete: true);
        }
    }
}

namespace TicketsBookingOnlineSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig4 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CreatorFilms", "Creator_Id", "dbo.Creators");
            DropForeignKey("dbo.CreatorFilms", "Film_Id", "dbo.Films");
            DropIndex("dbo.CreatorFilms", new[] { "Creator_Id" });
            DropIndex("dbo.CreatorFilms", new[] { "Film_Id" });
            CreateTable(
                "dbo.FilmCreators",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FilmId = c.Int(nullable: false),
                        CreatorId = c.Int(nullable: false),
                        Role = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Creators", t => t.CreatorId, cascadeDelete: true)
                .Index(t => t.CreatorId);
            
            CreateTable(
                "dbo.FilmCreatorFilms",
                c => new
                    {
                        FilmCreator_Id = c.Int(nullable: false),
                        Film_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.FilmCreator_Id, t.Film_Id })
                .ForeignKey("dbo.FilmCreators", t => t.FilmCreator_Id, cascadeDelete: true)
                .ForeignKey("dbo.Films", t => t.Film_Id, cascadeDelete: true)
                .Index(t => t.FilmCreator_Id)
                .Index(t => t.Film_Id);
            
            DropTable("dbo.CreatorFilms");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.CreatorFilms",
                c => new
                    {
                        Creator_Id = c.Int(nullable: false),
                        Film_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Creator_Id, t.Film_Id });
            
            DropForeignKey("dbo.FilmCreatorFilms", "Film_Id", "dbo.Films");
            DropForeignKey("dbo.FilmCreatorFilms", "FilmCreator_Id", "dbo.FilmCreators");
            DropForeignKey("dbo.FilmCreators", "CreatorId", "dbo.Creators");
            DropIndex("dbo.FilmCreatorFilms", new[] { "Film_Id" });
            DropIndex("dbo.FilmCreatorFilms", new[] { "FilmCreator_Id" });
            DropIndex("dbo.FilmCreators", new[] { "CreatorId" });
            DropTable("dbo.FilmCreatorFilms");
            DropTable("dbo.FilmCreators");
            CreateIndex("dbo.CreatorFilms", "Film_Id");
            CreateIndex("dbo.CreatorFilms", "Creator_Id");
            AddForeignKey("dbo.CreatorFilms", "Film_Id", "dbo.Films", "Id", cascadeDelete: true);
            AddForeignKey("dbo.CreatorFilms", "Creator_Id", "dbo.Creators", "Id", cascadeDelete: true);
        }
    }
}

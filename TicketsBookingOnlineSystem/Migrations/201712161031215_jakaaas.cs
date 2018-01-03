namespace TicketsBookingOnlineSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class jakaaas : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CreatorImages", "Creator_Id", "dbo.Creators");
            DropForeignKey("dbo.FilmCreators", "CreatorId", "dbo.Creators");
            DropForeignKey("dbo.FilmCreators", "FilmId", "dbo.Films");
            DropIndex("dbo.FilmCreators", new[] { "FilmId" });
            DropIndex("dbo.FilmCreators", new[] { "CreatorId" });
            DropIndex("dbo.CreatorImages", new[] { "Creator_Id" });
            AddColumn("dbo.Films", "Description", c => c.String());
            AddColumn("dbo.Films", "Creator_Id", c => c.Int());
            CreateIndex("dbo.Films", "Creator_Id");
            AddForeignKey("dbo.Films", "Creator_Id", "dbo.Creators", "Id");
            DropTable("dbo.FilmCreators");
            DropTable("dbo.CreatorImages");
        }
        
        public override void Down()
        {
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
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.FilmCreators",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FilmId = c.Int(nullable: false),
                        CreatorId = c.Int(nullable: false),
                        Role = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            DropForeignKey("dbo.Films", "Creator_Id", "dbo.Creators");
            DropIndex("dbo.Films", new[] { "Creator_Id" });
            DropColumn("dbo.Films", "Creator_Id");
            DropColumn("dbo.Films", "Description");
            CreateIndex("dbo.CreatorImages", "Creator_Id");
            CreateIndex("dbo.FilmCreators", "CreatorId");
            CreateIndex("dbo.FilmCreators", "FilmId");
            AddForeignKey("dbo.FilmCreators", "FilmId", "dbo.Films", "Id", cascadeDelete: true);
            AddForeignKey("dbo.FilmCreators", "CreatorId", "dbo.Creators", "Id", cascadeDelete: true);
            AddForeignKey("dbo.CreatorImages", "Creator_Id", "dbo.Creators", "Id");
        }
    }
}

namespace TicketsBookingOnlineSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sdd : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.FilmImages", "Film_Id", "dbo.Films");
            DropIndex("dbo.FilmImages", new[] { "Film_Id" });
            DropTable("dbo.FilmImages");
        }
        
        public override void Down()
        {
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
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.FilmImages", "Film_Id");
            AddForeignKey("dbo.FilmImages", "Film_Id", "dbo.Films", "Id");
        }
    }
}

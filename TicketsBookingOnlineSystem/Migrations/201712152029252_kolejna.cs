namespace TicketsBookingOnlineSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class kolejna : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Films", "Creator_Id", "dbo.Creators");
            DropIndex("dbo.Films", new[] { "Creator_Id" });
            DropColumn("dbo.Films", "Creator_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Films", "Creator_Id", c => c.Int());
            CreateIndex("dbo.Films", "Creator_Id");
            AddForeignKey("dbo.Films", "Creator_Id", "dbo.Creators", "Id");
        }
    }
}

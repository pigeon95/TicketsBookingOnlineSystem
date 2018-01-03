namespace TicketsBookingOnlineSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NextMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Films", "Creator_Id", c => c.Int());
            CreateIndex("dbo.Films", "Creator_Id");
            AddForeignKey("dbo.Films", "Creator_Id", "dbo.Creators", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Films", "Creator_Id", "dbo.Creators");
            DropIndex("dbo.Films", new[] { "Creator_Id" });
            DropColumn("dbo.Films", "Creator_Id");
        }
    }
}

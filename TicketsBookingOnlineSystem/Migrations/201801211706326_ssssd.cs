namespace TicketsBookingOnlineSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ssssd : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Auditoriums", "LocationArrangement", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Auditoriums", "LocationArrangement");
        }
    }
}

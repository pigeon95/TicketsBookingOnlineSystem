namespace TicketsBookingOnlineSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_image : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Films", "Image", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Films", "Image");
        }
    }
}

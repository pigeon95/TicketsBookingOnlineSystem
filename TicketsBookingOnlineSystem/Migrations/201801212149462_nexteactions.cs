namespace TicketsBookingOnlineSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nexteactions : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Rows", "Num", c => c.Int(nullable: false));
            AddColumn("dbo.Spots", "Num", c => c.Int(nullable: false));
            DropColumn("dbo.Rows", "RowId");
            DropColumn("dbo.Reservations", "Spot");
            DropColumn("dbo.Reservations", "Row");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Reservations", "Row", c => c.Int(nullable: false));
            AddColumn("dbo.Reservations", "Spot", c => c.Int(nullable: false));
            AddColumn("dbo.Rows", "RowId", c => c.String());
            DropColumn("dbo.Spots", "Num");
            DropColumn("dbo.Rows", "Num");
        }
    }
}

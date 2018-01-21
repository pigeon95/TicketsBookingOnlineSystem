namespace TicketsBookingOnlineSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dsfdff : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.ReservationSpots", newName: "SpotReservations");
            DropForeignKey("dbo.Rows", "Auditorium_Id", "dbo.Auditoriums");
            DropForeignKey("dbo.Spots", "Row_Id", "dbo.Rows");
            DropIndex("dbo.Rows", new[] { "Auditorium_Id" });
            DropIndex("dbo.Spots", new[] { "Row_Id" });
            DropPrimaryKey("dbo.SpotReservations");
            AddColumn("dbo.Spots", "Col", c => c.Int(nullable: false));
            AddColumn("dbo.Spots", "Row", c => c.Int(nullable: false));
            AddColumn("dbo.Spots", "Auditorium_Id", c => c.Int());
            AddPrimaryKey("dbo.SpotReservations", new[] { "Spot_Id", "Reservation_Id" });
            CreateIndex("dbo.Spots", "Auditorium_Id");
            AddForeignKey("dbo.Spots", "Auditorium_Id", "dbo.Auditoriums", "Id");
            DropColumn("dbo.Spots", "Num");
            DropColumn("dbo.Spots", "Row_Id");
            DropTable("dbo.Rows");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Rows",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Num = c.Int(nullable: false),
                        SpotCount = c.Int(nullable: false),
                        Name = c.String(),
                        Deleted = c.Boolean(nullable: false),
                        Auditorium_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Spots", "Row_Id", c => c.Int());
            AddColumn("dbo.Spots", "Num", c => c.Int(nullable: false));
            DropForeignKey("dbo.Spots", "Auditorium_Id", "dbo.Auditoriums");
            DropIndex("dbo.Spots", new[] { "Auditorium_Id" });
            DropPrimaryKey("dbo.SpotReservations");
            DropColumn("dbo.Spots", "Auditorium_Id");
            DropColumn("dbo.Spots", "Row");
            DropColumn("dbo.Spots", "Col");
            AddPrimaryKey("dbo.SpotReservations", new[] { "Reservation_Id", "Spot_Id" });
            CreateIndex("dbo.Spots", "Row_Id");
            CreateIndex("dbo.Rows", "Auditorium_Id");
            AddForeignKey("dbo.Spots", "Row_Id", "dbo.Rows", "Id");
            AddForeignKey("dbo.Rows", "Auditorium_Id", "dbo.Auditoriums", "Id");
            RenameTable(name: "dbo.SpotReservations", newName: "ReservationSpots");
        }
    }
}

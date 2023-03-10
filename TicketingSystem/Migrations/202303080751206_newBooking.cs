namespace TicketingSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newBooking : DbMigration
    {
        public override void Up()
        {

        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Bookings", "BusTripId", "dbo.BusTrips");
            DropIndex("dbo.Bookings", new[] { "BusTripId" });
            AlterColumn("dbo.BusInfoes", "BusCompanyLogo", c => c.Binary());
            DropTable("dbo.Payments");
            DropTable("dbo.Bookings");
        }
    }
}

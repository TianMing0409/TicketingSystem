namespace TicketingSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class paymentBooking : DbMigration
    {
        public override void Up()
        {
          
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Bookings", "BusTripId", "dbo.BusTrips");
            DropForeignKey("dbo.BusTrips", "OriginPlaceId", "dbo.OriginPlaces");
            DropForeignKey("dbo.BusTrips", "DestinationPlaceId", "dbo.DestinationPlaces");
            DropForeignKey("dbo.BusTrips", "BusId", "dbo.BusInfoes");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.BusTrips", new[] { "DestinationPlaceId" });
            DropIndex("dbo.BusTrips", new[] { "OriginPlaceId" });
            DropIndex("dbo.BusTrips", new[] { "BusId" });
            DropIndex("dbo.Bookings", new[] { "BusTripId" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Promotions");
            DropTable("dbo.Payments");
            DropTable("dbo.OriginPlaces");
            DropTable("dbo.DestinationPlaces");
            DropTable("dbo.BusInfoes");
            DropTable("dbo.BusTrips");
            DropTable("dbo.Bookings");
        }
    }
}

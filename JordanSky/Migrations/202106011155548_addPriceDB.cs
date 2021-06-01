namespace JordanSky.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addPriceDB : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Booking_package", "TotalPrice", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Booking_package", "TotalPrice");
        }
    }
}

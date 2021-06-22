namespace JordanSky.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class HotelDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Booking_Hotel",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Phone = c.String(),
                        StartDate = c.String(),
                        EndDate = c.String(),
                        Details = c.String(),
                        Status = c.Int(nullable: false),
                        Hotel_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Hotels", t => t.Hotel_id, cascadeDelete: true)
                .Index(t => t.Hotel_id);
            
            CreateTable(
                "dbo.Hotels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Ref_No = c.String(),
                        Name = c.String(),
                        Price_Day = c.Double(nullable: false),
                        Number = c.String(),
                        Maps = c.String(),
                        City_id = c.Int(nullable: false),
                        Type_Hotel_id = c.Int(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cities", t => t.City_id, cascadeDelete: true)
                .ForeignKey("dbo.Type_Hotel", t => t.Type_Hotel_id)
                .Index(t => t.City_id)
                .Index(t => t.Type_Hotel_id);
            
            CreateTable(
                "dbo.Cate_Hotel",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Hotel_id = c.Int(nullable: false),
                        Category_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.Category_id, cascadeDelete: true)
                .ForeignKey("dbo.Hotels", t => t.Hotel_id, cascadeDelete: true)
                .Index(t => t.Hotel_id)
                .Index(t => t.Category_id);
            
            CreateTable(
                "dbo.Type_Hotel",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Image_Hotel",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ImagePath = c.String(),
                        Hotel_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Hotels", t => t.Hotel_id, cascadeDelete: true)
                .Index(t => t.Hotel_id);
            
            AddColumn("dbo.Contacts", "Fax", c => c.String());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Booking_Hotel", "Hotel_id", "dbo.Hotels");
            DropForeignKey("dbo.Image_Hotel", "Hotel_id", "dbo.Hotels");
            DropForeignKey("dbo.Hotels", "Type_Hotel_id", "dbo.Type_Hotel");
            DropForeignKey("dbo.Hotels", "City_id", "dbo.Cities");
            DropForeignKey("dbo.Cate_Hotel", "Hotel_id", "dbo.Hotels");
            DropForeignKey("dbo.Cate_Hotel", "Category_id", "dbo.Categories");
            DropIndex("dbo.Image_Hotel", new[] { "Hotel_id" });
            DropIndex("dbo.Cate_Hotel", new[] { "Category_id" });
            DropIndex("dbo.Cate_Hotel", new[] { "Hotel_id" });
            DropIndex("dbo.Hotels", new[] { "Type_Hotel_id" });
            DropIndex("dbo.Hotels", new[] { "City_id" });
            DropIndex("dbo.Booking_Hotel", new[] { "Hotel_id" });
            DropColumn("dbo.Contacts", "Fax");
            DropTable("dbo.Image_Hotel");
            DropTable("dbo.Type_Hotel");
            DropTable("dbo.Cate_Hotel");
            DropTable("dbo.Hotels");
            DropTable("dbo.Booking_Hotel");
        }
    }
}

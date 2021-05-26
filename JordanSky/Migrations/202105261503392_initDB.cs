namespace JordanSky.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Booking_package",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Phone = c.String(),
                        No_Pepole = c.Int(nullable: false),
                        No_Child = c.Int(nullable: false),
                        Start_Id = c.Int(nullable: false),
                        Details = c.String(),
                        Status = c.Int(nullable: false),
                        Package_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Starting_locations", t => t.Start_Id, cascadeDelete: true)
                .ForeignKey("dbo.Internal_Package", t => t.Package_id, cascadeDelete: true)
                .Index(t => t.Start_Id)
                .Index(t => t.Package_id);
            
            CreateTable(
                "dbo.Starting_locations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Internal_Package",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name_Pakage = c.String(),
                        Type_id = c.Int(nullable: false),
                        Phone = c.String(),
                        Ref_No = c.String(),
                        Price = c.Double(nullable: false),
                        Picture = c.String(),
                        Child_Price = c.Double(nullable: false),
                        Note_price = c.String(),
                        Transportation = c.Int(nullable: false),
                        overnight_Stay = c.Int(nullable: false),
                        overnight_Stay_name = c.String(),
                        Food = c.Int(nullable: false),
                        name_restaurant = c.Int(nullable: false),
                        No_Day = c.String(),
                        StartDate = c.String(),
                        EndDate = c.String(),
                        Description = c.String(),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Type_packge", t => t.Type_id, cascadeDelete: true)
                .Index(t => t.Type_id);
            
            CreateTable(
                "dbo.Type_packge",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Image_Package",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ImagePath = c.String(),
                        Package_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Internal_Package", t => t.Package_id, cascadeDelete: true)
                .Index(t => t.Package_id);
            
            CreateTable(
                "dbo.Bookings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Phone = c.String(),
                        StartDate = c.String(),
                        EndDate = c.String(),
                        Details = c.String(),
                        Status = c.Int(nullable: false),
                        Mazra3a_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Mazr3a", t => t.Mazra3a_id, cascadeDelete: true)
                .Index(t => t.Mazra3a_id);
            
            CreateTable(
                "dbo.Mazr3a",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Name_owner = c.String(),
                        Phone_owner = c.String(),
                        Name_guard = c.String(),
                        Phone_guard = c.String(),
                        Price = c.Double(nullable: false),
                        Number = c.String(),
                        City_id = c.Int(nullable: false),
                        Max_people = c.String(),
                        No_bedroom = c.String(),
                        Space = c.String(),
                        No_bed = c.String(),
                        internet = c.String(),
                        pool = c.String(),
                        Zarb = c.String(),
                        grill = c.String(),
                        pool_heating = c.String(),
                        AC = c.String(),
                        Children = c.String(),
                        View = c.String(),
                        Parking = c.String(),
                        Playground = c.String(),
                        Type_Product_id = c.Int(),
                        Location = c.String(),
                        Ref_No = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cities", t => t.City_id, cascadeDelete: true)
                .ForeignKey("dbo.Type_Product", t => t.Type_Product_id)
                .Index(t => t.City_id)
                .Index(t => t.Type_Product_id);
            
            CreateTable(
                "dbo.Cate_Mazra3a",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Mazra3a_id = c.Int(nullable: false),
                        Category_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.Category_id, cascadeDelete: true)
                .ForeignKey("dbo.Mazr3a", t => t.Mazra3a_id, cascadeDelete: true)
                .Index(t => t.Mazra3a_id)
                .Index(t => t.Category_id);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Registers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Phone = c.String(),
                        City_id = c.Int(nullable: false),
                        Location = c.String(),
                        license = c.String(),
                        Picture = c.String(),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cities", t => t.City_id, cascadeDelete: true)
                .Index(t => t.City_id);
            
            CreateTable(
                "dbo.Images",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ImagePath = c.String(),
                        Mazr3a_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Mazr3a", t => t.Mazr3a_id, cascadeDelete: true)
                .Index(t => t.Mazr3a_id);
            
            CreateTable(
                "dbo.Type_Product",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Contacts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(),
                        Phone = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Subject = c.String(),
                        Phone = c.String(),
                        Msg = c.String(),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Type_user",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Type = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Username = c.String(),
                        Password = c.String(),
                        Type_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Type_user", t => t.Type_id, cascadeDelete: true)
                .Index(t => t.Type_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "Type_id", "dbo.Type_user");
            DropForeignKey("dbo.Bookings", "Mazra3a_id", "dbo.Mazr3a");
            DropForeignKey("dbo.Mazr3a", "Type_Product_id", "dbo.Type_Product");
            DropForeignKey("dbo.Images", "Mazr3a_id", "dbo.Mazr3a");
            DropForeignKey("dbo.Mazr3a", "City_id", "dbo.Cities");
            DropForeignKey("dbo.Registers", "City_id", "dbo.Cities");
            DropForeignKey("dbo.Cate_Mazra3a", "Mazra3a_id", "dbo.Mazr3a");
            DropForeignKey("dbo.Cate_Mazra3a", "Category_id", "dbo.Categories");
            DropForeignKey("dbo.Booking_package", "Package_id", "dbo.Internal_Package");
            DropForeignKey("dbo.Image_Package", "Package_id", "dbo.Internal_Package");
            DropForeignKey("dbo.Internal_Package", "Type_id", "dbo.Type_packge");
            DropForeignKey("dbo.Booking_package", "Start_Id", "dbo.Starting_locations");
            DropIndex("dbo.Users", new[] { "Type_id" });
            DropIndex("dbo.Images", new[] { "Mazr3a_id" });
            DropIndex("dbo.Registers", new[] { "City_id" });
            DropIndex("dbo.Cate_Mazra3a", new[] { "Category_id" });
            DropIndex("dbo.Cate_Mazra3a", new[] { "Mazra3a_id" });
            DropIndex("dbo.Mazr3a", new[] { "Type_Product_id" });
            DropIndex("dbo.Mazr3a", new[] { "City_id" });
            DropIndex("dbo.Bookings", new[] { "Mazra3a_id" });
            DropIndex("dbo.Image_Package", new[] { "Package_id" });
            DropIndex("dbo.Internal_Package", new[] { "Type_id" });
            DropIndex("dbo.Booking_package", new[] { "Package_id" });
            DropIndex("dbo.Booking_package", new[] { "Start_Id" });
            DropTable("dbo.Users");
            DropTable("dbo.Type_user");
            DropTable("dbo.Messages");
            DropTable("dbo.Contacts");
            DropTable("dbo.Type_Product");
            DropTable("dbo.Images");
            DropTable("dbo.Registers");
            DropTable("dbo.Cities");
            DropTable("dbo.Categories");
            DropTable("dbo.Cate_Mazra3a");
            DropTable("dbo.Mazr3a");
            DropTable("dbo.Bookings");
            DropTable("dbo.Image_Package");
            DropTable("dbo.Type_packge");
            DropTable("dbo.Internal_Package");
            DropTable("dbo.Starting_locations");
            DropTable("dbo.Booking_package");
        }
    }
}

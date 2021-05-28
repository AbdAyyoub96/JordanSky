namespace JordanSky.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editname : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Internal_Package", "name_restaurant", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Internal_Package", "name_restaurant", c => c.Int(nullable: false));
        }
    }
}

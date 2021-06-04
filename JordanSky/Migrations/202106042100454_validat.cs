namespace JordanSky.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class validat : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Registers", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Registers", "Phone", c => c.String(nullable: false));
            AlterColumn("dbo.Registers", "Location", c => c.String(nullable: false));
            AlterColumn("dbo.Registers", "license", c => c.String(nullable: false));
            AlterColumn("dbo.Registers", "Picture", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Registers", "Picture", c => c.String());
            AlterColumn("dbo.Registers", "license", c => c.String());
            AlterColumn("dbo.Registers", "Location", c => c.String());
            AlterColumn("dbo.Registers", "Phone", c => c.String());
            AlterColumn("dbo.Registers", "Name", c => c.String());
        }
    }
}

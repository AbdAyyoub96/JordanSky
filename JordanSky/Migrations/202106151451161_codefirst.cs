namespace JordanSky.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class codefirst : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Hotels", "Picture");
            DropColumn("dbo.Hotels", "Status");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Hotels", "Status", c => c.Int(nullable: false));
            AddColumn("dbo.Hotels", "Picture", c => c.String());
        }
    }
}

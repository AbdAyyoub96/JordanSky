namespace JordanSky.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateMazra3a : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Mazr3a", "Maps", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Mazr3a", "Maps");
        }
    }
}

﻿namespace JordanSky.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Name", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "Name");
        }
    }
}

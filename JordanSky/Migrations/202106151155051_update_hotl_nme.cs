namespace JordanSky.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_hotl_nme : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Cate_Hotel", name: "Mazra3a_id", newName: "Hotel_id");
            RenameIndex(table: "dbo.Cate_Hotel", name: "IX_Mazra3a_id", newName: "IX_Hotel_id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Cate_Hotel", name: "IX_Hotel_id", newName: "IX_Mazra3a_id");
            RenameColumn(table: "dbo.Cate_Hotel", name: "Hotel_id", newName: "Mazra3a_id");
        }
    }
}

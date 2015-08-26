namespace FurnitureProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ImagePath : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "ImagePath", c => c.String());
            DropColumn("dbo.AspNetUsers", "UserPicture");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "UserPicture", c => c.String());
            DropColumn("dbo.AspNetUsers", "ImagePath");
        }
    }
}

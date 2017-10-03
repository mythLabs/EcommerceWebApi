namespace EcommerceWebApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InsertcategoryDescription : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Categories", "CategoryDescription", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Categories", "CategoryDescription");
        }
    }
}

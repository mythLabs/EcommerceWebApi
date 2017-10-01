namespace EcommerceWebApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adduserv1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        profileImageName = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        PhoneNumber = c.String(),
                        City = c.String(),
                        Country = c.String(),
                        Username = c.String(),
                        Password = c.String(),
                        isDeleted = c.Boolean(),
                    })
                .PrimaryKey(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Users");
        }
    }
}

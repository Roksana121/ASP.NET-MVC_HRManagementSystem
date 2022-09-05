namespace Work_01.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ScriptsA2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AdministratorAddresses",
                c => new
                    {
                        AdministratorId = c.Int(nullable: false),
                        Address = c.String(nullable: false, maxLength: 150),
                        PostCode = c.String(nullable: false, maxLength: 4),
                    })
                .PrimaryKey(t => t.AdministratorId)
                .ForeignKey("dbo.Administrators", t => t.AdministratorId)
                .Index(t => t.AdministratorId);
            
            CreateTable(
                "dbo.Administrators",
                c => new
                    {
                        AdministratorId = c.Int(nullable: false, identity: true),
                        AdministratorName = c.String(nullable: false, maxLength: 40),
                    })
                .PrimaryKey(t => t.AdministratorId);
            
            CreateTable(
                "dbo.AdministratorPhotoes",
                c => new
                    {
                        AdministratorId = c.Int(nullable: false),
                        Image = c.String(),
                    })
                .PrimaryKey(t => t.AdministratorId)
                .ForeignKey("dbo.Administrators", t => t.AdministratorId)
                .Index(t => t.AdministratorId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AdministratorAddresses", "AdministratorId", "dbo.Administrators");
            DropForeignKey("dbo.AdministratorPhotoes", "AdministratorId", "dbo.Administrators");
            DropIndex("dbo.AdministratorPhotoes", new[] { "AdministratorId" });
            DropIndex("dbo.AdministratorAddresses", new[] { "AdministratorId" });
            DropTable("dbo.AdministratorPhotoes");
            DropTable("dbo.Administrators");
            DropTable("dbo.AdministratorAddresses");
        }
    }
}

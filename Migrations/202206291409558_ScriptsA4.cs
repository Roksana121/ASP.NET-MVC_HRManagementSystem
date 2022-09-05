namespace Work_01.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ScriptsA4 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AdministratorAddresses", "Address", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AdministratorAddresses", "Address", c => c.String(nullable: false, maxLength: 150));
        }
    }
}

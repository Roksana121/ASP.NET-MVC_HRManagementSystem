namespace Work_01.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ScriptsA5 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AdministratorAddresses", "Address", c => c.String(nullable: true, maxLength: 150));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AdministratorAddresses", "Address", c => c.String());
        }
    }
}

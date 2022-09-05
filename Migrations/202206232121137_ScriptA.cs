namespace Work_01.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ScriptA : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employees", "Address", c => c.String());
            AddColumn("dbo.Employees", "Email", c => c.String(nullable: false));
            AddColumn("dbo.Employees", "Phone", c => c.String(nullable: false));
            AddColumn("dbo.Employees", "SalaryAmmount", c => c.Decimal(nullable: false, storeType: "money"));
            AddColumn("dbo.Employees", "Image", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Employees", "Image");
            DropColumn("dbo.Employees", "SalaryAmmount");
            DropColumn("dbo.Employees", "Phone");
            DropColumn("dbo.Employees", "Email");
            DropColumn("dbo.Employees", "Address");
        }
    }
}

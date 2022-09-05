namespace Work_01.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ScriptsA : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        DepartmentId = c.Int(nullable: false, identity: true),
                        DepartmentName = c.String(nullable: false, maxLength: 40),
                    })
                .PrimaryKey(t => t.DepartmentId);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        EmployeeId = c.Int(nullable: false, identity: true),
                        EmployeeName = c.String(nullable: false, maxLength: 40),
                        Gender = c.String(nullable: false),
                        JoinDate = c.DateTime(nullable: false, storeType: "date"),
                        Designation = c.String(),
                        Status = c.String(nullable: false),
                        DepartmentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.EmployeeId)
                .ForeignKey("dbo.Departments", t => t.DepartmentId, cascadeDelete: true)
                .Index(t => t.DepartmentId);
            
            CreateTable(
                "dbo.EmployeeAddresses",
                c => new
                    {
                        EmployeeId = c.Int(nullable: false),
                        Address = c.String(),
                        Email = c.String(nullable: false),
                        Phone = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.EmployeeId)
                .ForeignKey("dbo.Employees", t => t.EmployeeId)
                .Index(t => t.EmployeeId);
            
            CreateTable(
                "dbo.EmployeePhotoes",
                c => new
                    {
                        EmployeeId = c.Int(nullable: false),
                        Image = c.String(),
                    })
                .PrimaryKey(t => t.EmployeeId)
                .ForeignKey("dbo.Employees", t => t.EmployeeId)
                .Index(t => t.EmployeeId);
            
            CreateTable(
                "dbo.EmployeeSalaries",
                c => new
                    {
                        EmployeeId = c.Int(nullable: false),
                        SalaryId = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SalaryAmmount = c.Decimal(nullable: false, storeType: "money"),
                    })
                .PrimaryKey(t => t.EmployeeId)
                .ForeignKey("dbo.Employees", t => t.EmployeeId)
                .Index(t => t.EmployeeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EmployeeSalaries", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.EmployeePhotoes", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.EmployeeAddresses", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.Employees", "DepartmentId", "dbo.Departments");
            DropIndex("dbo.EmployeeSalaries", new[] { "EmployeeId" });
            DropIndex("dbo.EmployeePhotoes", new[] { "EmployeeId" });
            DropIndex("dbo.EmployeeAddresses", new[] { "EmployeeId" });
            DropIndex("dbo.Employees", new[] { "DepartmentId" });
            DropTable("dbo.EmployeeSalaries");
            DropTable("dbo.EmployeePhotoes");
            DropTable("dbo.EmployeeAddresses");
            DropTable("dbo.Employees");
            DropTable("dbo.Departments");
        }
    }
}

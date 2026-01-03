namespace ANewReport.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class namechange : DbMigration
    {
        public override void Up()
        {
            Sql(
                "ALTER TABLE Employees " +
                "CHANGE HireDate hire_date DATETIME NOT NULL;"
            );
        }

        public override void Down()
        {
            Sql(
                "ALTER TABLE Employees " +
                "CHANGE hire_date HireDate DATETIME NOT NULL;"
            );
        }

    }
}

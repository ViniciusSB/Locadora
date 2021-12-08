namespace SistemaLocadora.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migration10 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Ators", "checkboxAnswer", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Ators", "checkboxAnswer");
        }
    }
}

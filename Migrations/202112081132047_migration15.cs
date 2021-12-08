namespace SistemaLocadora.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migration15 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Funcionarios", "nome", c => c.String(nullable: false));
            AlterColumn("dbo.Funcionarios", "email", c => c.String(nullable: false));
            AlterColumn("dbo.Funcionarios", "senha", c => c.String(nullable: false));
            AlterColumn("dbo.Funcionarios", "dataNascimento", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Funcionarios", "dataNascimento", c => c.String());
            AlterColumn("dbo.Funcionarios", "senha", c => c.String());
            AlterColumn("dbo.Funcionarios", "email", c => c.String());
            AlterColumn("dbo.Funcionarios", "nome", c => c.String());
        }
    }
}

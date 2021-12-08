namespace SistemaLocadora.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migration13 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Clientes",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        nome = c.String(),
                        email = c.String(),
                        cpf = c.String(),
                        dataNascimento = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Funcionarios",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        nome = c.String(),
                        email = c.String(),
                        senha = c.String(),
                        cpf = c.String(),
                        dataNascimento = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Pedidoes",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        idCliente = c.Int(nullable: false),
                        idProduto = c.Int(nullable: false),
                        quantidadeDias = c.Int(nullable: false),
                        vezesAlugadas = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            DropColumn("dbo.Ators", "checkboxAnswer");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Ators", "checkboxAnswer", c => c.Boolean(nullable: false));
            DropTable("dbo.Pedidoes");
            DropTable("dbo.Funcionarios");
            DropTable("dbo.Clientes");
        }
    }
}

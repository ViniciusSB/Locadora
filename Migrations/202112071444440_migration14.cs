namespace SistemaLocadora.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migration14 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Aluguels",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        idPedido = c.Int(nullable: false),
                        idFuncionario = c.Int(nullable: false),
                        valorTotal = c.Double(nullable: false),
                        status = c.String(),
                        dataEntrega = c.String(),
                        dataLimiteEntrega = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Aluguels");
        }
    }
}

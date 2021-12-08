namespace SistemaLocadora.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Filmes",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        plataforma = c.String(),
                        publicadora = c.String(),
                        nome = c.String(),
                        dataLancamento = c.String(),
                        classificacaoIndicativa = c.Int(nullable: false),
                        genero = c.String(),
                        sinopse = c.String(),
                        valor = c.Double(nullable: false),
                        quantidadeTotal = c.Int(nullable: false),
                        quantidadeDisponivel = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Filmes");
        }
    }
}

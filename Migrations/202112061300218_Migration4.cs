namespace SistemaLocadora.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration4 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Jogoes",
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
                        tipo = c.String(),
                        valor = c.Double(nullable: false),
                        quantidadeTotal = c.Int(nullable: false),
                        quantidadeDisponivel = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Series",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        idElenco = c.Int(nullable: false),
                        temporada = c.Int(nullable: false),
                        nome = c.String(),
                        dataLancamento = c.String(),
                        classificacaoIndicativa = c.Int(nullable: false),
                        genero = c.String(),
                        sinopse = c.String(),
                        tipo = c.String(),
                        valor = c.Double(nullable: false),
                        quantidadeTotal = c.Int(nullable: false),
                        quantidadeDisponivel = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            AddColumn("dbo.Filmes", "tipo", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Filmes", "tipo");
            DropTable("dbo.Series");
            DropTable("dbo.Jogoes");
        }
    }
}

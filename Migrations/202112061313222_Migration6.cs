namespace SistemaLocadora.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration6 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Filmes", newName: "Produtoes");
            AddColumn("dbo.Produtoes", "plataforma", c => c.String());
            AddColumn("dbo.Produtoes", "publicadora", c => c.String());
            AddColumn("dbo.Produtoes", "temporada", c => c.Int());
            AddColumn("dbo.Produtoes", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Produtoes", "duracao", c => c.Int());
            DropTable("dbo.Jogoes");
            DropTable("dbo.Series");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Series",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
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
            
            AlterColumn("dbo.Produtoes", "duracao", c => c.Int(nullable: false));
            DropColumn("dbo.Produtoes", "Discriminator");
            DropColumn("dbo.Produtoes", "temporada");
            DropColumn("dbo.Produtoes", "publicadora");
            DropColumn("dbo.Produtoes", "plataforma");
            RenameTable(name: "dbo.Produtoes", newName: "Filmes");
        }
    }
}

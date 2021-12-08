namespace SistemaLocadora.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Filmes", "idElenco", c => c.Int(nullable: false));
            AddColumn("dbo.Filmes", "duracao", c => c.Int(nullable: false));
            DropColumn("dbo.Filmes", "plataforma");
            DropColumn("dbo.Filmes", "publicadora");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Filmes", "publicadora", c => c.String());
            AddColumn("dbo.Filmes", "plataforma", c => c.String());
            DropColumn("dbo.Filmes", "duracao");
            DropColumn("dbo.Filmes", "idElenco");
        }
    }
}

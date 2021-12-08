namespace SistemaLocadora.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration5 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Filmes", "idElenco");
            DropColumn("dbo.Series", "idElenco");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Series", "idElenco", c => c.Int(nullable: false));
            AddColumn("dbo.Filmes", "idElenco", c => c.Int(nullable: false));
        }
    }
}

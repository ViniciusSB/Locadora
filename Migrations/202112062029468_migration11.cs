namespace SistemaLocadora.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migration11 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Filme_Ator",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        idFilme = c.Int(nullable: false),
                        idAtor = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Filme_Ator");
        }
    }
}

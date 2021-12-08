namespace SistemaLocadora.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migration12 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Serie_Ator",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        idSerie = c.Int(nullable: false),
                        idAtor = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Serie_Ator");
        }
    }
}

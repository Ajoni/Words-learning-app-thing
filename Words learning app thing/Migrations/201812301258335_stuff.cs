namespace Words_learning_app_thing.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class stuff : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Jezyks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nazwa = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Slowoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Zawartosc = c.String(),
                        Jezyk_Id = c.Int(),
                        Slowo_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Jezyks", t => t.Jezyk_Id)
                .ForeignKey("dbo.Slowoes", t => t.Slowo_Id)
                .Index(t => t.Jezyk_Id)
                .Index(t => t.Slowo_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Slowoes", "Slowo_Id", "dbo.Slowoes");
            DropForeignKey("dbo.Slowoes", "Jezyk_Id", "dbo.Jezyks");
            DropIndex("dbo.Slowoes", new[] { "Slowo_Id" });
            DropIndex("dbo.Slowoes", new[] { "Jezyk_Id" });
            DropTable("dbo.Slowoes");
            DropTable("dbo.Jezyks");
        }
    }
}

namespace Words_learning_app_thing.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixed_slowo_tlumaczenie : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Slowoes", "Slowo_Id", "dbo.Slowoes");
            DropIndex("dbo.Slowoes", new[] { "Slowo_Id" });
            CreateTable(
                "dbo.SlowoTlumaczenie",
                c => new
                    {
                        SlowoId = c.Int(nullable: false),
                        TlumaczenieId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.SlowoId, t.TlumaczenieId })
                .ForeignKey("dbo.Slowoes", t => t.SlowoId)
                .ForeignKey("dbo.Slowoes", t => t.TlumaczenieId)
                .Index(t => t.SlowoId)
                .Index(t => t.TlumaczenieId);
            
            DropColumn("dbo.Slowoes", "Slowo_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Slowoes", "Slowo_Id", c => c.Int());
            DropForeignKey("dbo.SlowoTlumaczenie", "TlumaczenieId", "dbo.Slowoes");
            DropForeignKey("dbo.SlowoTlumaczenie", "SlowoId", "dbo.Slowoes");
            DropIndex("dbo.SlowoTlumaczenie", new[] { "TlumaczenieId" });
            DropIndex("dbo.SlowoTlumaczenie", new[] { "SlowoId" });
            DropTable("dbo.SlowoTlumaczenie");
            CreateIndex("dbo.Slowoes", "Slowo_Id");
            AddForeignKey("dbo.Slowoes", "Slowo_Id", "dbo.Slowoes", "Id");
        }
    }
}

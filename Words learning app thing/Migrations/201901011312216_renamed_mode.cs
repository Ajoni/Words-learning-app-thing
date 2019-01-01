namespace Words_learning_app_thing.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class renamed_mode : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "PrefferedMode", c => c.Int(nullable: false));
            DropColumn("dbo.AspNetUsers", "preferowanyTryb");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "preferowanyTryb", c => c.Int(nullable: false));
            DropColumn("dbo.AspNetUsers", "PrefferedMode");
        }
    }
}

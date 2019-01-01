namespace Words_learning_app_thing.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_difficulty_choice_preference : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "preferowanyTryb", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "preferowanyTryb");
        }
    }
}

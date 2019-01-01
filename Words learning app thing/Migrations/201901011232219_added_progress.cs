namespace Words_learning_app_thing.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_progress : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Progress", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Progress");
        }
    }
}

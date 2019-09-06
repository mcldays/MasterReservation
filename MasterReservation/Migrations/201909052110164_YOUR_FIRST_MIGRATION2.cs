namespace MasterReservation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class YOUR_FIRST_MIGRATION2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ResidentModels", "IsAdmin", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ResidentModels", "IsAdmin");
        }
    }
}

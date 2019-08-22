namespace MasterReservation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class YOUR_FIRST_MIGRATION : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SalonModels", "Information", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.SalonModels", "Information");
        }
    }
}

namespace MasterReservation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class admin18 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.WorkingPlaceModels", "RateHalfMounth", c => c.Double(nullable: false));
            AddColumn("dbo.WorkingPlaceModels", "RateMounth", c => c.Double(nullable: false));
            DropColumn("dbo.WorkingPlaceModels", "Rate3h");
        }
        
        public override void Down()
        {
            AddColumn("dbo.WorkingPlaceModels", "Rate3h", c => c.Double(nullable: false));
            DropColumn("dbo.WorkingPlaceModels", "RateMounth");
            DropColumn("dbo.WorkingPlaceModels", "RateHalfMounth");
        }
    }
}

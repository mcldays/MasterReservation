namespace MasterReservation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class admin20 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SalonModels", "OperatingModeMon", c => c.String(nullable: false));
            AddColumn("dbo.SalonModels", "OperatingModeTue", c => c.String(nullable: false));
            AddColumn("dbo.SalonModels", "OperatingModeWed", c => c.String(nullable: false));
            AddColumn("dbo.SalonModels", "OperatingModeThu", c => c.String(nullable: false));
            AddColumn("dbo.SalonModels", "OperatingModeFri", c => c.String(nullable: false));
            DropColumn("dbo.SalonModels", "OperatingModeWeek");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SalonModels", "OperatingModeWeek", c => c.String(nullable: false));
            DropColumn("dbo.SalonModels", "OperatingModeFri");
            DropColumn("dbo.SalonModels", "OperatingModeThu");
            DropColumn("dbo.SalonModels", "OperatingModeWed");
            DropColumn("dbo.SalonModels", "OperatingModeTue");
            DropColumn("dbo.SalonModels", "OperatingModeMon");
        }
    }
}

namespace MasterReservation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class admin19 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SalonModels", "OperatingModeWeek", c => c.String(nullable: false));
            AddColumn("dbo.SalonModels", "OperatingModeSat", c => c.String(nullable: false));
            AlterColumn("dbo.SalonModels", "City", c => c.String(nullable: false));
            AlterColumn("dbo.SalonModels", "Adress", c => c.String(nullable: false));
            AlterColumn("dbo.SalonModels", "AdminPass", c => c.String(nullable: false));
            DropColumn("dbo.SalonModels", "OperatingMode");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SalonModels", "OperatingMode", c => c.String());
            AlterColumn("dbo.SalonModels", "AdminPass", c => c.String());
            AlterColumn("dbo.SalonModels", "Adress", c => c.String());
            AlterColumn("dbo.SalonModels", "City", c => c.String());
            DropColumn("dbo.SalonModels", "OperatingModeSat");
            DropColumn("dbo.SalonModels", "OperatingModeWeek");
        }
    }
}

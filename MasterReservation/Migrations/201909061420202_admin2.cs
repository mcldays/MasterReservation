namespace MasterReservation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class admin2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SalonModels", "Adress", c => c.String());
            AddColumn("dbo.SalonModels", "OperatingMode", c => c.String());
            AddColumn("dbo.SalonModels", "ReservationType", c => c.Boolean(nullable: false));
            DropColumn("dbo.SalonModels", "Password");
            DropColumn("dbo.SalonModels", "Information");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SalonModels", "Information", c => c.String());
            AddColumn("dbo.SalonModels", "Password", c => c.String());
            DropColumn("dbo.SalonModels", "ReservationType");
            DropColumn("dbo.SalonModels", "OperatingMode");
            DropColumn("dbo.SalonModels", "Adress");
        }
    }
}

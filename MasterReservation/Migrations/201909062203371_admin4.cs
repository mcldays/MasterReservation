namespace MasterReservation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class admin4 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.SalonModels", "ContactPerson", c => c.String(nullable: false));
            AlterColumn("dbo.SalonModels", "Phone", c => c.String(nullable: false));
            AlterColumn("dbo.SalonModels", "Email", c => c.String(nullable: false));
            AlterColumn("dbo.WorkingPlaceModels", "Offers", c => c.String(nullable: false));
            AlterColumn("dbo.WorkingPlaceModels", "PlaceType", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.WorkingPlaceModels", "PlaceType", c => c.String());
            AlterColumn("dbo.WorkingPlaceModels", "Offers", c => c.String());
            AlterColumn("dbo.SalonModels", "Email", c => c.String());
            AlterColumn("dbo.SalonModels", "Phone", c => c.String());
            AlterColumn("dbo.SalonModels", "ContactPerson", c => c.String());
        }
    }
}

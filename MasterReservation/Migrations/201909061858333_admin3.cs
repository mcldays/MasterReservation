namespace MasterReservation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class admin3 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.WorkingPlaceModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Offers = c.String(),
                        Rate1h = c.Double(nullable: false),
                        Rate3h = c.Double(nullable: false),
                        RateDay = c.Double(nullable: false),
                        PlaceType = c.String(),
                        AdditionalConditions = c.String(),
                        Description = c.String(),
                        SalonId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.WorkingPlaceModels");
        }
    }
}

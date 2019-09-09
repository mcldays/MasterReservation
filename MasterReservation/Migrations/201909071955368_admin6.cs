namespace MasterReservation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class admin6 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TimeSlotModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PlaceId = c.Int(nullable: false),
                        Time = c.String(nullable: false),
                        Booked = c.Boolean(nullable: false),
                        ResidentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.TimeSlotModels");
        }
    }
}

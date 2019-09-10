namespace MasterReservation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class admin101 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TimeSlotModels", "BookingId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TimeSlotModels", "BookingId");
        }
    }
}

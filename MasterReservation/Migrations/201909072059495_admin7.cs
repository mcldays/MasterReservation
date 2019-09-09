namespace MasterReservation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class admin7 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TimeSlotModels", "SalonId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TimeSlotModels", "SalonId");
        }
    }
}

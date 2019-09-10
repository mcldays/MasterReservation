namespace MasterReservation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class admin8 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TimeSlotModels", "Date", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TimeSlotModels", "Date");
        }
    }
}

namespace MasterReservation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class admin23 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BookingModels", "Phone", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.BookingModels", "Phone");
        }
    }
}

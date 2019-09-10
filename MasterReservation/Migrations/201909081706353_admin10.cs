namespace MasterReservation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class admin10 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BookingModels", "Sum", c => c.Double(nullable: false));
            AddColumn("dbo.BookingModels", "Confirmed", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.BookingModels", "Confirmed");
            DropColumn("dbo.BookingModels", "Sum");
        }
    }
}

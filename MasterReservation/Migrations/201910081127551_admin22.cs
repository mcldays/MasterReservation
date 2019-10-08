namespace MasterReservation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class admin22 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BookingModels", "Name", c => c.String());
            AddColumn("dbo.BookingModels", "Surname", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.BookingModels", "Surname");
            DropColumn("dbo.BookingModels", "Name");
        }
    }
}

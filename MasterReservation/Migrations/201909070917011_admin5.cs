namespace MasterReservation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class admin5 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SalonModels", "SalonTitle", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SalonModels", "SalonTitle");
        }
    }
}

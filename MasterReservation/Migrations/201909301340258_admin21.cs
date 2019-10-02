namespace MasterReservation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class admin21 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SalonModels", "OperatingModeSun", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SalonModels", "OperatingModeSun");
        }
    }
}

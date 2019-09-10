namespace MasterReservation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class admin13 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SalonModels", "AdminPass", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.SalonModels", "AdminPass");
        }
    }
}

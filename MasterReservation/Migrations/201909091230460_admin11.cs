namespace MasterReservation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class admin11 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ResidentModels", "Favorites", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ResidentModels", "Favorites");
        }
    }
}

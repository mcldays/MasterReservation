namespace MasterReservation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ShadowDieTwice : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.ResidentModels", "ComparePass");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ResidentModels", "ComparePass", c => c.String());
        }
    }
}

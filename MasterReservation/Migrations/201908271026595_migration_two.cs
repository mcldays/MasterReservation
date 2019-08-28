namespace MasterReservation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migration_two : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ResidentModels", "Password", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ResidentModels", "Password", c => c.String());
        }
    }
}

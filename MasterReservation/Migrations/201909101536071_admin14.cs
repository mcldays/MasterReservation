namespace MasterReservation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class admin14 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PictureModels",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Image = c.Binary(),
                    })
                .PrimaryKey(t => t.id);
            
            AddColumn("dbo.SalonModels", "AdminPass", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.SalonModels", "AdminPass");
            DropTable("dbo.PictureModels");
        }
    }
}

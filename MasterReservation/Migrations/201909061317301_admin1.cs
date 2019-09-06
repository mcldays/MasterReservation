namespace MasterReservation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class admin1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ResidentModels", "IsAdmin", c => c.Boolean(nullable: false));
            DropTable("dbo.PictureModels");
        }
        
        public override void Down()
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
            
            DropColumn("dbo.ResidentModels", "IsAdmin");
        }
    }
}

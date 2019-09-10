namespace MasterReservation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class admin15 : DbMigration
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
            
        }
        
        public override void Down()
        {
            DropTable("dbo.PictureModels");
        }
    }
}

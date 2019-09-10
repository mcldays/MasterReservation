namespace MasterReservation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class admin16 : DbMigration
    {
        public override void Up()
        {
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
            
        }
    }
}

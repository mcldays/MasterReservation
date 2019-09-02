namespace MasterReservation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class YooBeach : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DateModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.String(),
                        TimeFirst = c.String(),
                        TimeSecond = c.String(),
                        FullTime = c.Boolean(nullable: false),
                        Offers = c.String(),
                        City = c.String(),
                        PrivateSpace = c.Boolean(nullable: false),
                        OpenSpace = c.Boolean(nullable: false),
                        AddedOffers = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.DateModels");
        }
    }
}

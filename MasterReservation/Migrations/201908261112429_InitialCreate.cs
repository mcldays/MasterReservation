namespace MasterReservation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ResidentModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Surname = c.String(),
                        Name = c.String(),
                        Patronymic = c.String(),
                        Phone = c.String(),
                        Email = c.String(),
                        ServicesIds = c.String(),
                        StudyPlace = c.String(),
                        Experience = c.String(),
                        Awards = c.String(),
                        Password = c.String(),
                        City = c.String(),
                        Offers = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SalonModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ContactPerson = c.String(),
                        Phone = c.String(),
                        City = c.String(),
                        Email = c.String(),
                        Message = c.String(),
                        Password = c.String(),
                        Information = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.SalonModels");
            DropTable("dbo.ResidentModels");
        }
    }
}

namespace Mono.Service.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Mono : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.VehicleMakeEntities",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Abrv = c.String(),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.VehicleModelEntities",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Abrv = c.String(),
                        MakeId = c.Guid(nullable: false),
                        Name = c.String(),
                        VehicleMake_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.VehicleMakeEntities", t => t.VehicleMake_Id)
                .Index(t => t.VehicleMake_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.VehicleModelEntities", "VehicleMake_Id", "dbo.VehicleMakeEntities");
            DropIndex("dbo.VehicleModelEntities", new[] { "VehicleMake_Id" });
            DropTable("dbo.VehicleModelEntities");
            DropTable("dbo.VehicleMakeEntities");
        }
    }
}

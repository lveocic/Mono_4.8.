namespace Mono.Service.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Mono1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.VehicleModelEntities", "VehicleMake_Id", "dbo.VehicleMakeEntities");
            DropIndex("dbo.VehicleModelEntities", new[] { "VehicleMake_Id" });
            RenameColumn(table: "dbo.VehicleModelEntities", name: "VehicleMake_Id", newName: "VehicleMakeId");
            AlterColumn("dbo.VehicleModelEntities", "VehicleMakeId", c => c.Guid(nullable: false));
            CreateIndex("dbo.VehicleModelEntities", "VehicleMakeId");
            AddForeignKey("dbo.VehicleModelEntities", "VehicleMakeId", "dbo.VehicleMakeEntities", "Id", cascadeDelete: true);
            DropColumn("dbo.VehicleModelEntities", "MakeId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.VehicleModelEntities", "MakeId", c => c.Guid(nullable: false));
            DropForeignKey("dbo.VehicleModelEntities", "VehicleMakeId", "dbo.VehicleMakeEntities");
            DropIndex("dbo.VehicleModelEntities", new[] { "VehicleMakeId" });
            AlterColumn("dbo.VehicleModelEntities", "VehicleMakeId", c => c.Guid());
            RenameColumn(table: "dbo.VehicleModelEntities", name: "VehicleMakeId", newName: "VehicleMake_Id");
            CreateIndex("dbo.VehicleModelEntities", "VehicleMake_Id");
            AddForeignKey("dbo.VehicleModelEntities", "VehicleMake_Id", "dbo.VehicleMakeEntities", "Id");
        }
    }
}

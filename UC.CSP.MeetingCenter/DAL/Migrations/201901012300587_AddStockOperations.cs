namespace UC.CSP.MeetingCenter.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddStockOperations : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.StockOperations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DateTime = c.DateTime(nullable: false),
                        Count = c.Int(nullable: false),
                        AccessoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Accessories", t => t.AccessoryId, cascadeDelete: true)
                .Index(t => t.AccessoryId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StockOperations", "AccessoryId", "dbo.Accessories");
            DropIndex("dbo.StockOperations", new[] { "AccessoryId" });
            DropTable("dbo.StockOperations");
        }
    }
}

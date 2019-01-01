namespace UC.CSP.MeetingCenter.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAccessories : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Accessories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CategoryId = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 100),
                        StoredCount = c.Int(nullable: false),
                        RecommendedMinCount = c.Int(nullable: false),
                        DeletedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Accessories", "CategoryId", "dbo.Categories");
            DropIndex("dbo.Accessories", new[] { "CategoryId" });
            DropTable("dbo.Categories");
            DropTable("dbo.Accessories");
        }
    }
}

namespace UC.CSP.MeetingCenter.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCustomerToStockOperation : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.StockOperations", "CustomerName", c => c.String(nullable: false, maxLength: 100));
        }
        
        public override void Down()
        {
            DropColumn("dbo.StockOperations", "CustomerName");
        }
    }
}

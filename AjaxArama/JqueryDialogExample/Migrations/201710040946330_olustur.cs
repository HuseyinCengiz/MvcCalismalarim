namespace JqueryDialogExample.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class olustur : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Kullanici",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Adi = c.String(nullable: false, maxLength: 10, fixedLength: true),
                        Soyadi = c.String(nullable: false, maxLength: 10, fixedLength: true),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Kullanici");
        }
    }
}

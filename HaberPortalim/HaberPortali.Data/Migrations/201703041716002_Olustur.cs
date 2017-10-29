namespace HaberPortali.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Olustur : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Kullanici",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AdSoyad = c.String(maxLength: 150),
                        Email = c.String(),
                        Sifre = c.String(nullable: false, maxLength: 16),
                        KayitTarihi = c.DateTime(nullable: false),
                        Aktif = c.Boolean(nullable: false),
                        Rol_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Rol", t => t.Rol_Id)
                .Index(t => t.Rol_Id);
            
            CreateTable(
                "dbo.Rol",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RolAdi = c.String(maxLength: 150),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Kullanici", "Rol_Id", "dbo.Rol");
            DropIndex("dbo.Kullanici", new[] { "Rol_Id" });
            DropTable("dbo.Rol");
            DropTable("dbo.Kullanici");
        }
    }
}

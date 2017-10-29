namespace HaberPortali.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Olustur2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Kategori", "EklenmeTarihi", c => c.DateTime(nullable: false));
            AddColumn("dbo.Kullanici", "EklenmeTarihi", c => c.DateTime(nullable: false));
            AddColumn("dbo.Kullanici", "AktifMi", c => c.Boolean(nullable: false));
            AddColumn("dbo.Rol", "EklenmeTarihi", c => c.DateTime(nullable: false));
            AddColumn("dbo.Rol", "AktifMi", c => c.Boolean(nullable: false));
            AddColumn("dbo.Resim", "EklenmeTarihi", c => c.DateTime(nullable: false));
            AddColumn("dbo.Resim", "AktifMi", c => c.Boolean(nullable: false));
            DropColumn("dbo.Kullanici", "KayitTarihi");
            DropColumn("dbo.Kullanici", "Aktif");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Kullanici", "Aktif", c => c.Boolean(nullable: false));
            AddColumn("dbo.Kullanici", "KayitTarihi", c => c.DateTime(nullable: false));
            DropColumn("dbo.Resim", "AktifMi");
            DropColumn("dbo.Resim", "EklenmeTarihi");
            DropColumn("dbo.Rol", "AktifMi");
            DropColumn("dbo.Rol", "EklenmeTarihi");
            DropColumn("dbo.Kullanici", "AktifMi");
            DropColumn("dbo.Kullanici", "EklenmeTarihi");
            DropColumn("dbo.Kategori", "EklenmeTarihi");
        }
    }
}

namespace HaberPortali.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<HaberPortali.Data.DataContext.HaberContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }
        /*Kod k�sm�nda yapt���m�z de�i�iklikleri veritaban�na yans�tmaya Migration demekteyiz.*/
        /*Migrations olu�tururken ilk �nce Enable-Migration diyoruz ve bu klas�r� olu�turuyor
        Daha Sonra Add-Migration Olustur diyerek tablolar�m�zdaki veriler classa geliyor daha sonra Update-Databese diyerek guncelliyoruz.*/

        protected override void Seed(HaberPortali.Data.DataContext.HaberContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}

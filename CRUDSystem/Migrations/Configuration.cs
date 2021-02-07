namespace CRUDSystem.Migrations
{
    using CRUDSystem.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<CRUDSystem.Models.MyDBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(CRUDSystem.Models.MyDBContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.

            IList<Detail> details = new List<Detail>();

            details.Add(new Detail() {Fname="Harry",Lname="Secret",Address="Secret Address1",Age=20,DateOfBirth=DateTime.Now });
            details.Add(new Detail() {Fname="Flouflou",Lname="Secret",Address="Secret Address2",Age=38,DateOfBirth=DateTime.Now });
            details.Add(new Detail() {Fname="ClairClair",Lname="Secret",Address="Secret Address3",Age=22,DateOfBirth=DateTime.Now });

            foreach (Detail detail in details)
                context.Details.Add(detail);

            base.Seed(context);
        }
    }
}

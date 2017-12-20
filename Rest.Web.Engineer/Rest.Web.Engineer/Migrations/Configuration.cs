using Rest.Web.Engineer.Models;

namespace Rest.Web.Engineer.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Rest.Web.Engineer.Models.Entities>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "Rest.Web.Engineer.Models.Entities";
        }

        protected override void Seed(Rest.Web.Engineer.Models.Entities context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            var menus = new Menu[]
            {
                new Menu()
                {
                    Title = "Strona g³ówna",
                    Href = "/",
                    Icon = "glyphicon-home",
                    MenuId = 1
                },
                new Menu()
                {
                    Title = "Kategorie",
                    Href = "/categories",
                    Icon = "glyphicon-inbox",
                    MenuId = 2
                },
                new Menu()
                {
                    Title="Produkty",
                    Icon = "glyphicon-list-alt",
                    Href = "/products",
                    MenuId = 3
                },
                new Menu()
                {
                    MenuId = 4,
                    Title = "Menu",
                    Icon = "glyphicon-shopping-cart",
                    Href = "/cart"
                },

            };

            context.Menus.AddOrUpdate(menus);

        }
    }
}

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
                    Title = "Produkty",
                    Icon = "glyphicon-list-alt",
                    Href = "/products",
                    MenuId = 3
                },
                new Menu()
                {
                    MenuId = 4,
                    Title = "Menu",
                    Icon = "glyphicon-shopping-cart",
                    Href = "/carts"
                },
            };

            context.Menus.AddOrUpdate(menus);

            var categories = new Category[]
            {
                new Category()
                {
                    Products = null,
                    CategoryId = 1,
                    Description = "",
                    Name = "Jedzenie"
                },
                new Category()
                {
                    Products = null,
                    CategoryId = 1,
                    Description = "",
                    Name = "Komputery"
                },
                new Category()
                {
                    Products = null,
                    CategoryId = 1,
                    Description = "",
                    Name = "Odzie¿"
                },
                new Category()
                {
                    Products = null,
                    CategoryId = 1,
                    Description = "",
                    Name = "Motoryzacja"
                },

            };

            context.Categories.AddOrUpdate(categories);

            var @params = new Param[]
            {
                new Param()
                {
                    ParamId = 1,
                    Code = "SMTP_PORT",
                    Value = "2525",
                },
                new Param()
                {
                    ParamId = 2,
                    Code = "SMTP_HOST",
                    Value = "smtp.mailtrap.io",
                },
                new Param()
                {
                    ParamId = 3,
                    Code = "SMTP_USERNAME",
                    Value = "83db0c2b4b392d",
                },
                new Param()
                {
                    ParamId = 4,
                    Code = "SMTP_PASSWORD",
                    Value = "87a4701fcce281",
                },
                new Param()
                {
                    ParamId = 5,
                    Code = "OWNER_MAIL",
                    Value = "rafeq1241@gmail.com"
                },
                new Param()
                {
                    ParamId = 6,
                    Code="OWNER_ACCOUNT_NUMBER",
                    Value = "22 1240 0001 7546 0049 6227 1208"
                }, 
                new Param()
                {
                    ParamId = 7,
                    Code="OWNER_NAME_AND_SURNAME",
                    Value="Jan Kowalski"
                }, 
            };

            context.Params.AddOrUpdate(@params);
        }
    }
}
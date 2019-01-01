using System.Data.Entity.Migrations;
using System.Linq;
using UC.CSP.MeetingCenter.DAL.Entities;

namespace UC.CSP.MeetingCenter.DAL.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<AppDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"DAL\Migrations";
        }

        protected override void Seed(AppDbContext context)
        {
            if (!context.Categories.Any())
            {
                context.Categories.AddRange(new[]
                {
                    new Category()
                    {
                        Name = "Hot drinks"
                    },
                    new Category()
                    {
                        Name = "Cold drinks"
                    },
                    new Category()
                    {
                        Name = "Snacks"
                    },
                    new Category()
                    {
                        Name = "Office supplies"
                    },
                    new Category()
                    {
                        Name = "Marketing materials"
                    },
                });
            }
        }
    }
}
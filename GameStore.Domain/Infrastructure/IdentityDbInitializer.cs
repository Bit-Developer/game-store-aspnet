using GameStore.Domain.Helper;
using GameStore.Domain.Identity;
using GameStore.Domain.Model;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Domain.Infrastructure
{
    public class IdentityDbInitializer : DropCreateDatabaseIfModelChanges<GameStoreDBContext>
    {
        protected override void Seed(GameStoreDBContext context)
        {
            PerformInitialSetup(context);
            base.Seed(context);
        }

        public void PerformInitialSetup(GameStoreDBContext context)
        {
            GetRoles().ForEach(c => context.Roles.Add(c));
            GetCategories().ForEach(c => context.Categories.Add(c));
            GetProducts().ForEach(c => context.Products.Add(c));
            context.SaveChanges();
            GameStorePasswordHasher hasher = new GameStorePasswordHasher();
            var user = new AppUser { UserName = "admin", Email = "admin@gamestore.com", PasswordHash = hasher.HashPassword("admin"), Membership = "Admin" };
            var role = context.Roles.Where(r => r.Name == "Admin").First();
            user.Roles.Add(new IdentityUserRole { RoleId = role.Id, UserId = user.Id });
            context.Users.Add(user);
            user = new AppUser { UserName = "regular", Email = "regular@gamestore.com", PasswordHash = hasher.HashPassword("regular"), Membership = "Regular" };
            role = context.Roles.Where(r => r.Name == "Regular").First();
            user.Roles.Add(new IdentityUserRole { RoleId = role.Id, UserId = user.Id });
            context.Users.Add(user);
            user = new AppUser { UserName = "advanced", Email = "advanced@gamestore.com", PasswordHash = hasher.HashPassword("advanced"), Membership = "Advanced" };
            role = context.Roles.Where(r => r.Name == "Advanced").First();
            user.Roles.Add(new IdentityUserRole { RoleId = role.Id, UserId = user.Id });
            context.Users.Add(user);
            context.SaveChanges();
        }

        private static List<AppRole> GetRoles()
        {
            var roles = new List<AppRole> {
               new AppRole {Name="Admin", Description="Admin"},
               new AppRole {Name="Regular", Description="Regular"},
               new AppRole {Name="Advanced", Description="Advanced"}
            };

            return roles;
        }

        private static List<Category> GetCategories()
        {
            var categories = new List<Category> {
               new Category {CategoryId=1, CategoryName="Console"},
               new Category {CategoryId=2, CategoryName="Accessory"},
               new Category {CategoryId=3, CategoryName="Game"}
            };

            return categories;
        }

        private static List<Product> GetProducts()
        {
            var products = new List<Product> {
               new Product {ProductId=1, ProductName="XBox One", CategoryId = 1, Price = 399.00, Image="consoles/xbox1.jpg", Condition="New", Discount=10, UserId="Admin"},
               new Product {ProductId=2, ProductName="XBox 360", CategoryId = 1, Price = 299.00, Image="consoles/xbox360.jpg", Condition="New", Discount=10, UserId="Admin"},
               new Product {ProductId=3, ProductName="PS3", CategoryId = 1, Price = 219.00, Image="consoles/ps3-console.jpg", Condition="New", Discount=10, UserId="Admin"},
               new Product {ProductId=4, ProductName="PS4", CategoryId = 1, Price = 349.00, Image="consoles/PS4-console-bundle.jpg", Condition="New", Discount=10, UserId="Admin"},
               new Product {ProductId=5, ProductName="Wii", CategoryId = 1, Price = 269.00, Image="consoles/wii.jpg", Condition="New", Discount=10, UserId="Admin"},
               new Product {ProductId=6, ProductName="WiiU", CategoryId = 1, Price = 299.99, Image="consoles/wiiu.jpg", Condition="New", Discount=10, UserId="Admin"},
               new Product {ProductId=7, ProductName="Xbox Controller", CategoryId = 2, Price = 40.99, Image="accessories/XBOX controller.jpg", Condition="New", Discount=10, UserId="Admin"},
               new Product {ProductId=8, ProductName="Turtle Beach Headset", CategoryId = 2, Price = 50.00, Image="accessories/Turtle Beach Headset.jpg", Condition="New", Discount=10, UserId="Admin"},
               new Product {ProductId=9, ProductName="Speeding Wheel", CategoryId = 2, Price = 35.99, Image="accessories/XBOX360-SpeedWheel.jpg", Condition="New", Discount=10, UserId="Admin"},
               new Product {ProductId=10, ProductName="Wireless Adapter", CategoryId = 2, Price = 40.99, Image="accessories/xbox360_wa.png", Condition="New", Discount=10, UserId="Admin"},
               new Product {ProductId=11, ProductName="Wireless Controller", CategoryId = 2, Price = 19.99, Image="accessories/ps3_controller.jpg", Condition="New", Discount=10, UserId="Admin"},
               new Product {ProductId=12, ProductName="Disc Remote Control", CategoryId = 2, Price = 23.99, Image="accessories/ps3_diskcontroller.jpg", Condition="New", Discount=10, UserId="Admin"},
               new Product {ProductId=13, ProductName="Chartboost - Black", CategoryId = 2, Price = 18.99, Image="accessories/chartboost.jpg", Condition="New", Discount=10, UserId="Admin"},
               new Product {ProductId=14, ProductName="Dual Controller Charger", CategoryId = 2, Price = 25.99, Image="accessories/ps4_controllercharger.jpg", Condition="New", Discount=10, UserId="Admin"},
               new Product {ProductId=15, ProductName="Charging System - Black", CategoryId = 2, Price = 25.99, Image="accessories/wii_chargingsystem.jpg", Condition="New", Discount=10, UserId="Admin"},
               new Product {ProductId=16, ProductName="Wii Remote Plus", CategoryId = 2, Price = 39.99, Image="accessories/wii_remoteplus.jpg", Condition="New", Discount=10, UserId="Admin"},
               new Product {ProductId=17, ProductName="Fight Pad", CategoryId = 2, Price = 16.99, Image="accessories/wiiu_fightingpad.jpg", Condition="New", Discount=10, UserId="Admin"},
               new Product {ProductId=18, ProductName="GameCube Controller", CategoryId = 2, Price = 29.99, Image="accessories/wiiu_gamecube.jpg", Condition="New", Discount=10, UserId="Admin"},
               new Product {ProductId=19, ProductName="FIFA 2016", CategoryId = 3, Price = 59.99, Image="games/ea_fifa.jpg", Condition="New", Discount=10, UserId="Admin"},
               new Product {ProductId=20, ProductName="Need for Speed", CategoryId = 3, Price = 32.99, Image="games/ea_nfs.jpg", Condition="New", Discount=10, UserId="Admin"},
               new Product {ProductId=21, ProductName="Call Of Duty", CategoryId = 3, Price = 36.99, Image="games/activision_cod.jpg", Condition="New", Discount=10, UserId="Admin"},
               new Product {ProductId=22, ProductName="Evolve", CategoryId = 3, Price = 49.99, Image="games/tti_evolve.jpg", Condition="New", Discount=10, UserId="Admin"},
            };

            return products;
        }
    }
}

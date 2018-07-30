using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStore.Domain.Model;
using Microsoft.AspNet.Identity.EntityFramework;
using GameStore.Domain.Identity;

namespace GameStore.Domain.Infrastructure
{
    public partial class GameStoreDBContext : IdentityDbContext<AppUser>
    {
        public GameStoreDBContext()
            : base("name=GameStoreDBContext", throwIfV1Schema: false)
        {
            Database.SetInitializer<GameStoreDBContext>(new IdentityDbInitializer());
        }

        public static GameStoreDBContext Create()
        {
            return new GameStoreDBContext();
        }

        //public virtual DbSet<Role> Roles { get; set; }
        //public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Review> Reviews { get; set; }
        public virtual DbSet<OrderItem> OrderItems { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
    }
}

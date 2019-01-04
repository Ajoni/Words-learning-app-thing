using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Words_learning_app_thing.Models;

namespace Words_learning_app_thing.Data
{

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {

        public DbSet<Jezyk> Jezyki { get; set; }
        public DbSet<Slowo> Slowa { get; set; }

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Slowo>()
                        .HasMany<Slowo>(s => s.Tlumaczenia)
                        .WithMany()
                        .Map(cs =>
                        {
                            cs.MapLeftKey("SlowoId");
                            cs.MapRightKey("TlumaczenieId");
                            cs.ToTable("SlowoTlumaczenie");
                        });

        }
    }
}
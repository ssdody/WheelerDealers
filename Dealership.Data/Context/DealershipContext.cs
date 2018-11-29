using Dealership.Data.Context.Configurations;
using Dealership.Data.Models;
using Dealership.Data.Models.Contracts;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Dealership.Data.Context
{
    public class DealershipContext : IdentityDbContext<User>
    {
        public DealershipContext(DbContextOptions<DealershipContext> contextOptions) : base(contextOptions)
        {

        }
        public DbSet<Brand> Brands { get; set; }

        public DbSet<CarModel> CarModels { get; set; }

        public DbSet<Car> Cars { get; set; }

        public DbSet<CarsExtras> CarsExtras { get; set; }

        public DbSet<BodyType> BodyTypes { get; set; }

        public DbSet<Color> Colors { get; set; }

        public DbSet<Extra> Extras { get; set; }

        public DbSet<FuelType> FuelTypes { get; set; }

        public DbSet<Gearbox> Gearboxes { get; set; }

        public DbSet<GearType> GearTypes { get; set; }

        public DbSet<ColorType> ColorTypes { get; set; }

        public DbSet<UsersCars> UsersCars { get; set; }

        public DbSet<Image> Images { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Car>()
                .HasOne(c => c.Brand)
                .WithMany(b => b.Cars)
                .Metadata.DeleteBehavior = DeleteBehavior.Restrict;

            modelBuilder.ApplyConfiguration(new CarsExtrasConfiguration());
            //modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new UsersCarsConfiguration());

            SeedData(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            this.ApplyAuditInfoRules();
            return base.SaveChanges();
        }

        private void ApplyAuditInfoRules()
        {
            var newlyCreatedEntities = this.ChangeTracker.Entries()
                .Where(e => e.Entity is IEditable && ((e.State == EntityState.Added) || (e.State == EntityState.Modified)));

            foreach (var entry in newlyCreatedEntities)
            {
                var entity = (IEditable)entry.Entity;

                if (entry.State == EntityState.Added && entity.CreatedOn == null)
                {
                    entity.CreatedOn = DateTime.Now;
                }
                else
                {
                    entity.ModifiedOn = DateTime.Now;
                }
            }
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BodyType>().HasData(new BodyType { Id = 1, Name = "Sedan" });
            modelBuilder.Entity<BodyType>().HasData(new BodyType { Id = 2, Name = "Coupe" });
            modelBuilder.Entity<BodyType>().HasData(new BodyType { Id = 3, Name = "Cabrio" });
            modelBuilder.Entity<BodyType>().HasData(new BodyType { Id = 4, Name = "Touring" });
            modelBuilder.Entity<BodyType>().HasData(new BodyType { Id = 5, Name = "SUV" });
            modelBuilder.Entity<BodyType>().HasData(new BodyType { Id = 6, Name = "Hatchback" });

            modelBuilder.Entity<GearType>().HasData(new GearType { Id = 1, Name = "Automatic" });
            modelBuilder.Entity<GearType>().HasData(new GearType { Id = 2, Name = "Manual" });

            modelBuilder.Entity<Gearbox>().HasData(new Gearbox { Id = 1, GearTypeId = 1, NumberOfGears = 3 });
            modelBuilder.Entity<Gearbox>().HasData(new Gearbox { Id = 2, GearTypeId = 1, NumberOfGears = 4 });
            modelBuilder.Entity<Gearbox>().HasData(new Gearbox { Id = 3, GearTypeId = 1, NumberOfGears = 5 });
            modelBuilder.Entity<Gearbox>().HasData(new Gearbox { Id = 4, GearTypeId = 1, NumberOfGears = 6 });
            modelBuilder.Entity<Gearbox>().HasData(new Gearbox { Id = 5, GearTypeId = 1, NumberOfGears = 7 });
            modelBuilder.Entity<Gearbox>().HasData(new Gearbox { Id = 6, GearTypeId = 1, NumberOfGears = 8 });
            modelBuilder.Entity<Gearbox>().HasData(new Gearbox { Id = 7, GearTypeId = 2, NumberOfGears = 4 });
            modelBuilder.Entity<Gearbox>().HasData(new Gearbox { Id = 8, GearTypeId = 2, NumberOfGears = 5 });
            modelBuilder.Entity<Gearbox>().HasData(new Gearbox { Id = 9, GearTypeId = 2, NumberOfGears = 6 });

            modelBuilder.Entity<FuelType>().HasData(new FuelType { Id = 1, Name = "Diesel" });
            modelBuilder.Entity<FuelType>().HasData(new FuelType { Id = 2, Name = "Gasoline" });
            modelBuilder.Entity<FuelType>().HasData(new FuelType { Id = 3, Name = "LPG" });
            modelBuilder.Entity<FuelType>().HasData(new FuelType { Id = 4, Name = "Hybrid" });
            modelBuilder.Entity<FuelType>().HasData(new FuelType { Id = 5, Name = "Electic" });

            modelBuilder.Entity<ColorType>().HasData(new ColorType { Id = 1, Name = "Acrylic" });
            modelBuilder.Entity<ColorType>().HasData(new ColorType { Id = 2, Name = "Metalic" });
            modelBuilder.Entity<ColorType>().HasData(new ColorType { Id = 3, Name = "Pearlescent" });
            modelBuilder.Entity<ColorType>().HasData(new ColorType { Id = 4, Name = "Matte" });
            modelBuilder.Entity<ColorType>().HasData(new ColorType { Id = 5, Name = "Xirallic" });
        }
    }
}

using Dealership.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dealership.Data.Context.Configurations
{
    internal class CarsExtrasConfiguration : IEntityTypeConfiguration<CarsExtras>
    {
        public void Configure(EntityTypeBuilder<CarsExtras> builder)
        {
            builder.HasKey(ce => new { ce.CarId, ce.ExtraId });

            builder.HasOne(ce => ce.Car)
                .WithMany(c => c.CarsExtras)
                .HasForeignKey(ce => ce.CarId);

            builder.HasOne(ce => ce.Extra)
                .WithMany(e => e.CarsExtras)
                .HasForeignKey(ce => ce.ExtraId);
        }
    }
}

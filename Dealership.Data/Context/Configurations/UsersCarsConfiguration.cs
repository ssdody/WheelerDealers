using Dealership.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dealership.Data.Context.Configurations
{
    internal class UsersCarsConfiguration : IEntityTypeConfiguration<UsersCars>
    {
        public void Configure(EntityTypeBuilder<UsersCars> builder)
        {
            builder.HasKey(uc => new { uc.UserId, uc.CarId });

            builder.HasOne(uc => uc.Car)
                .WithMany(c => c.UsersCars)
                .HasForeignKey(uc => uc.CarId);

            builder.HasOne(uc => uc.User)
                .WithMany(u => u.UsersCars)
                .HasForeignKey(uc => uc.UserId);
        }
    }
}

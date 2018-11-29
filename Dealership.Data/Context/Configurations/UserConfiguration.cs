using Dealership.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Dealership.Data.Context.Configurations
{
    //internal class UserConfiguration : IEntityTypeConfiguration<User>
    //{
    //    public void Configure(EntityTypeBuilder<User> builder)
    //    {
    //        builder.HasIndex(u => u.Username)
    //                .IsUnique();

    //        builder.HasIndex(u => u.Email)
    //                .IsUnique();

    //        var converter = new EnumToStringConverter<UserType>();

    //        builder
    //            .Property(e => e.UserType)
    //            .HasConversion(converter);
    //    }
    //}
}

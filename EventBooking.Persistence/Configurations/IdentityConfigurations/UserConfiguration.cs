using EventBooking.Domain.Entities.IdentityEntities;
using EventBooking.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventBooking.Persistence.Configurations.IdentityConfigurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);

            builder.Property(u => u.FirstName)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(u => u.LastName)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(u => u.Address)
                .HasMaxLength(200)
                .IsRequired(false);

            builder.Property(u => u.PhoneNumber)
                .IsRequired();

            builder.Property(u => u.Email)
                .IsRequired();

            builder.Property(u => u.ImageUrl)
                .HasMaxLength(500)
                .IsRequired(false);

            builder.Property(u => u.DateOfBirth)
                .IsRequired(false);

            builder.Property(u => u.Gender)
                .HasMaxLength(6)
                .IsRequired()
                .HasConversion(u => u.ToString(),
                u => Enum.Parse<GenderType>(u));

            builder.ToTable("Users");
        }
    }
}

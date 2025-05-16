using EventBooking.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventBooking.Persistence.Configurations
{
    public class EventConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                  .ValueGeneratedOnAdd()
                  .HasDefaultValueSql("NEWID()");

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(e => e.Description)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(e => e.Category)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasIndex(e => e.Category, "IN_Event_Category")
                .IsUnique();

            builder.Property(e => e.Venue)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(e => e.Price)
                .IsRequired()
                .HasPrecision(7, 2);

            builder.Property(e => e.ImageUrl)
                .IsRequired(false)
                .HasMaxLength(500);

            builder.ToTable("Events");

        }
    }
}

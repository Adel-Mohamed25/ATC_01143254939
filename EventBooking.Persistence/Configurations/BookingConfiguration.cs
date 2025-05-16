using EventBooking.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventBooking.Persistence.Configurations
{
    public class BookingConfiguration : IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> builder)
        {
            builder.HasKey(b => b.Id);

            builder.Property(b => b.Id)
                  .ValueGeneratedOnAdd()
                  .HasDefaultValueSql("NEWID()");

            builder.Property(b => b.BookingDate)
                  .ValueGeneratedOnAdd()
                  .HasDefaultValueSql("GETDATE()");

            builder.HasOne(b => b.User)
                .WithMany(u => u.Bookings)
                .HasForeignKey(b => b.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(b => b.Event)
               .WithMany(e => e.Bookings)
               .HasForeignKey(b => b.EventId)
               .OnDelete(DeleteBehavior.Restrict);

            builder.ToTable("Bookings");
        }
    }
}

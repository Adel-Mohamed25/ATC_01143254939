using EventBooking.Domain.Entities.IdentityEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventBooking.Persistence.Configurations.IdentityConfigurations
{
    public class JwtTokenConfiguration : IEntityTypeConfiguration<JwtToken>
    {
        public void Configure(EntityTypeBuilder<JwtToken> builder)
        {
            builder.HasKey(jt => jt.Id);

            builder.Property(jt => jt.Id)
                  .ValueGeneratedOnAdd()
                  .HasDefaultValueSql("NEWID()");


            builder.HasOne(jt => jt.User)
                .WithMany(u => u.JwtTokens)
                .HasForeignKey(jt => jt.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.ToTable("JwtTokens");
        }
    }
}

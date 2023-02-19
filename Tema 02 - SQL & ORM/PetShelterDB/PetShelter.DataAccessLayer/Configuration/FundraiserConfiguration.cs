using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetShelter.DataAccessLayer.Models;

namespace PetShelter.DataAccessLayer.Configuration
{
    public class FundraiserConfiguration : IEntityTypeConfiguration<Fundraiser>
    {
        public void Configure(EntityTypeBuilder<Fundraiser> builder)
        {
            builder.HasKey(p => p.Id);

            
            builder.Property(p => p.Name).IsRequired().HasMaxLength(100);
            builder.Property(p => p.Target).IsRequired();


            builder.HasMany(p => p.Donations).WithOne(p => p.Fundraiser)
                .HasForeignKey(p => p.FundraiserId);

        }
    }
}

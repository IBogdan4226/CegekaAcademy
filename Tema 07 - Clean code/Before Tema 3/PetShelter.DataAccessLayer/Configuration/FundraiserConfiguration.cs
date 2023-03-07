using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PetShelter.DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class FundraiserConfiguration : IEntityTypeConfiguration<Fundraiser>
{
        public void Configure(EntityTypeBuilder<Fundraiser> builder)
        {
            builder.HasKey(p => p.Id);


            builder.Property(p => p.Name).IsRequired().HasMaxLength(100);
            builder.Property(p => p.Target).IsRequired();
            builder.Property(p => p.OwnerId).IsRequired();
            builder.Property(p=>p.DueDate).IsRequired();

            builder.HasMany(p => p.Donations).WithOne(p => p.Fundraiser)
                .HasForeignKey(p => p.FundraiserId);
            builder.HasOne(p => p.Owner).WithMany(p => p.StartedFundraisers).HasForeignKey(p => p.OwnerId).IsRequired();
        }
}

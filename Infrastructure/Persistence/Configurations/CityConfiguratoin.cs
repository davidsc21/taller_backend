using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using taller_backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;
 public class CityConfiguration : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.ToTable("cities");
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasOne(c => c.Region)
                .WithMany(r => r.Cities)
                .HasForeignKey(c => c.RegionId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(c => c.Companies)
                .WithOne(comp => comp.City)
                .HasForeignKey(comp => comp.CityId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(c => c.Branches)
                .WithOne(b => b.City)
                .HasForeignKey(b => b.CityId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
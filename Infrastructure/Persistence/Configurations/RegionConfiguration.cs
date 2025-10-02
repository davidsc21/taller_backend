using System;
using System.Collections.Generic;
using taller_backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace taller_backend.Domain.configurations;

public class RegionConfiguration : IEntityTypeConfiguration<Region>
    {
        public void Configure(EntityTypeBuilder<Region> builder)
        {
            builder.ToTable("regions");
            builder.HasKey(r => r.Id);
            
            builder.Property(r => r.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasOne(r => r.Country)
                .WithMany(c => c.Regions)
                .HasForeignKey(r => r.CountryId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(r => r.Cities)
                .WithOne(c => c.Region)
                .HasForeignKey(c => c.RegionId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
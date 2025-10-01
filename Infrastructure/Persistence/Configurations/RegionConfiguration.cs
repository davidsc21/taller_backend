using System;
using taller_backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace taller_backend.Domain.configurations;

public class RegionConfiguration : IEntityTypeConfiguration<Region>
{
    public void Configure(EntityTypeBuilder<Region> builder)
    {
        builder.ToTable("Regions");
        builder.HasKey(r => r.Id);
        builder.Property(r => r.Name)
               .IsRequired()
               .HasColumnType("varchar(50)");
        builder.HasOne(r => r.Country)
               .WithMany(c => c.Regions)
               .HasForeignKey(r => r.CountryId)
               .OnDelete(DeleteBehavior.SetNull);
        builder.HasMany(ci => ci.Cities)
               .WithOne(r => r.Region)
               .HasForeignKey(ci => ci.RegionId)
               .OnDelete(DeleteBehavior.SetNull);
    }
}

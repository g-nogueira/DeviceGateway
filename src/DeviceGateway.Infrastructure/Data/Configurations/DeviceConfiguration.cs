using DeviceGateway.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DeviceGateway.Infrastructure.Data.Configurations;

public class DeviceConfiguration : IEntityTypeConfiguration<Device>
{
    public void Configure(EntityTypeBuilder<Device> builder)
    {
        builder.ToTable("Devices");
        builder.HasIndex(d => d.BrandId);
        builder.HasIndex(d => d.State);
        builder.HasIndex(d => d.CreatedAt);
        builder.HasIndex(d => d.DeletedAt);

        builder.HasKey(d => d.Id);

        builder.Property(d => d.Name)
            .IsRequired()
            .HasMaxLength(100); // Assuming a reasonable max length for device names

        builder.Property(d => d.BrandId)
            .IsRequired();

        builder.Property(d => d.State)
            .HasConversion<int>()
            .IsRequired();

        builder.Property(d => d.CreatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP")
            .IsRequired();

        builder.Property(d => d.DeletedAt)
            .IsRequired(false);
    }
}
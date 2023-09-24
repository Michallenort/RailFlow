using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Railflow.Core.Entities;

namespace RailFlow.Infrastructure.DAL.Configurations;

internal sealed class StationConfiguration : IEntityTypeConfiguration<Station>
{
    public void Configure(EntityTypeBuilder<Station> builder)
    {
        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(50);

        builder.OwnsOne(x => x.Address, address =>
            {
                address.ToTable("Addresses");
                address.WithOwner().HasForeignKey("Id");
                address.Property(x => x.Country)
                    .HasColumnName("Country")
                    .IsRequired()
                    .HasMaxLength(25);
                address.Property(x => x.City)
                    .HasColumnName("City")
                    .IsRequired()
                    .HasMaxLength(25);
                address.Property(x => x.Street)
                    .HasColumnName("Street")
                    .IsRequired()
                    .HasMaxLength(50);
            }
        );
    }
}
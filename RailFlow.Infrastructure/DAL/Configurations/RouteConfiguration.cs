using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Railflow.Core.Entities;

namespace RailFlow.Infrastructure.DAL.Configurations;

internal sealed class RouteConfiguration : IEntityTypeConfiguration<Route>
{
    public void Configure(EntityTypeBuilder<Route> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(25);

        builder.Property(x => x.StartStationId)
            .IsRequired();
        
        builder.Property(x => x.EndStationId)
            .IsRequired();

        builder.Property(x => x.IsActive)
            .IsRequired()
            .HasDefaultValue(false);

        builder.HasMany(x => x.Stops)
            .WithOne(x => x.Route)
            .HasForeignKey(x => x.RouteId)
            .OnDelete(DeleteBehavior.Cascade);
        
    }
}
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
        
        builder.Property(x => x.IsActive)
            .IsRequired()
            .HasDefaultValue(false);
        
        builder.HasOne(x => x.StartStation)
            .WithMany()
            .HasForeignKey(x => x.StartStationId)
            .OnDelete(DeleteBehavior.SetNull);
        
        builder.HasOne(x => x.EndStation)
            .WithMany()
            .HasForeignKey(x => x.EndStationId)
            .OnDelete(DeleteBehavior.SetNull);
        
        builder.HasOne(x => x.Train)
            .WithOne(x => x.AssignedRoute)
            .HasForeignKey<Route>(x => x.TrainId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasMany(x => x.Stops)
            .WithOne(x => x.Route)
            .HasForeignKey(x => x.RouteId)
            .OnDelete(DeleteBehavior.Cascade);
        
    }
}
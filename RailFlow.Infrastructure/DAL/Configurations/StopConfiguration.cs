using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Railflow.Core.Entities;

namespace RailFlow.Infrastructure.DAL.Configurations;

internal sealed class StopConfiguration : IEntityTypeConfiguration<Stop>
{
    public void Configure(EntityTypeBuilder<Stop> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.ArrivalHour)
            .IsRequired();

        builder.Property(x => x.DepartureHour)
            .IsRequired();

        builder.Property(x => x.RouteId)
            .IsRequired();

        builder.Property(x => x.StationId)
            .IsRequired();
        
        builder.HasOne(x => x.Route)
            .WithMany(x => x.Stops)
            .HasForeignKey(x => x.RouteId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne(x => x.Station)
            .WithMany(x => x.Stops)
            .HasForeignKey(x => x.StationId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
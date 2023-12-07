using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Railflow.Core.Entities;

namespace RailFlow.Infrastructure.DAL.Configurations;

internal sealed class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
{
    public void Configure(EntityTypeBuilder<Reservation> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Date)
            .IsRequired();
        builder.Property(x => x.UserId)
            .IsRequired();
        builder.Property(x => x.FirstScheduleId);
        builder.Property(x => x.StartStopId)
            .IsRequired();
        builder.Property(x => x.EndStopId)
            .IsRequired();
        builder.Property(x => x.Price)
            .IsRequired();

        builder.HasOne(x => x.User)
            .WithMany(x => x.Reservations)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne(x => x.FirstSchedule)
            .WithMany()
            .HasForeignKey(x => x.FirstScheduleId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.SecondSchedule)
            .WithMany()
            .HasForeignKey(x => x.SecondScheduleId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne(x => x.StartStop)
            .WithMany()
            .HasForeignKey(x => x.StartStopId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.EndStop)
            .WithMany()
            .HasForeignKey(x => x.EndStopId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.TransferStop)
            .WithMany()
            .HasForeignKey(x => x.TransferStopId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
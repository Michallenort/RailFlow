using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Railflow.Core.Entities;

namespace RailFlow.Infrastructure.DAL.Configurations;

internal sealed class TrainConfiguration : IEntityTypeConfiguration<Train>
{
    public void Configure(EntityTypeBuilder<Train> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Number)
            .IsRequired();

        builder.Property(x => x.Capacity)
            .IsRequired();

        builder.HasOne(x => x.AssignedRoute)
            .WithOne(x => x.Train)
            .HasForeignKey<Route>(x => x.TrainId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
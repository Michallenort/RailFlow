using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Railflow.Core.Entities;

namespace RailFlow.Infrastructure.DAL.Configurations;

internal class EmployeeAssignmentConfiguration : IEntityTypeConfiguration<EmployeeAssignment>
{
    public void Configure(EntityTypeBuilder<EmployeeAssignment> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.UserId)
            .IsRequired();
        
        builder.Property(x => x.ScheduleId)
            .IsRequired();
        
        builder.Property(x => x.StartHour)
            .IsRequired();

        builder.Property(x => x.EndHour)
            .IsRequired();

        builder.HasOne(x => x.User)
            .WithMany(x => x.EmployeeAssignments)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Schedule)
            .WithMany(x => x.EmployeeAssignments)
            .HasForeignKey(x => x.ScheduleId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
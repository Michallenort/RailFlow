using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Railflow.Core.Entities;

namespace RailFlow.Infrastructure.DAL.Configurations;

internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.FirstName)
            .IsRequired()
            .HasMaxLength(25);
        builder.Property(x => x.LastName)
            .IsRequired()
            .HasMaxLength(25);
        builder.Property(x => x.Email)
            .IsRequired()
            .HasMaxLength(50);
        builder.Property(x => x.Nationality)
            .IsRequired();
        builder.Property(x => x.RoleId)
            .IsRequired();
    }
}
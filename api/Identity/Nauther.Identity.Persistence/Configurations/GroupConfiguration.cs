using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nauther.Identity.Domain.Entities;

namespace Nauther.Identity.Persistence.Configurations;

public class GroupConfiguration : IEntityTypeConfiguration<Group>
{
    public void Configure(EntityTypeBuilder<Group> entity)
    {
        entity.HasKey(pg => pg.Id);
        entity.Property(pg => pg.Name).HasMaxLength(100).IsRequired();

        entity.HasMany(pg => pg.UserGroups)
            .WithOne(ug => ug.Group)
            .HasForeignKey(ug => ug.GroupId)
            .OnDelete(DeleteBehavior.Cascade);
        
        entity.HasMany(pg => pg.GroupPermissions)
            .WithOne(pgp => pgp.Group)
            .HasForeignKey(pgp => pgp.GroupId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nauther.Identity.Domain.Entities;

namespace Nauther.Identity.Persistence.Configurations;

public class UserPermissionGroupConfiguration : IEntityTypeConfiguration<UserGroup>
{
    public void Configure(EntityTypeBuilder<UserGroup> entity)
    {
        entity.HasKey(upg => new
        {
            upg.UserId, PermissionGroupId = upg.GroupId
        });

        entity.HasOne(upg => upg.User)
            .WithMany(u => u.UserGroups)
            .HasForeignKey(upg => upg.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        
        entity.HasOne(upg => upg.Group)
            .WithMany(pg => pg.UserGroups)
            .HasForeignKey(upg => upg.GroupId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

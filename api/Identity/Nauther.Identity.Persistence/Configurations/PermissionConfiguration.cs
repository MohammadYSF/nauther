using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nauther.Identity.Domain.Entities;

namespace Nauther.Identity.Persistence.Configurations;

public class PermissionConfiguration : IEntityTypeConfiguration<Permission>
{
    public void Configure(EntityTypeBuilder<Permission> entity)
    {
        entity.HasKey(p => p.Id);
        entity.Property(p => p.Name).HasMaxLength(50).IsRequired();
        entity.Property(p => p.DisplayName).HasMaxLength(50).IsRequired();
        
        entity.HasMany(p => p.UserPermissions)
            .WithOne(p => p.Permission)
            .HasForeignKey(p => p.PermissionId)
            .OnDelete(DeleteBehavior.Cascade);
        
        entity.HasMany(p => p.RolePermissions)
            .WithOne(p => p.Permission)
            .HasForeignKey(p => p.PermissionId)
            .OnDelete(DeleteBehavior.Cascade);
        
        entity.HasMany(p => p.GroupPermissions)
            .WithOne(p => p.Permission)
            .HasForeignKey(p => p.PermissionId)
            .OnDelete(DeleteBehavior.Cascade);
        
    }
}
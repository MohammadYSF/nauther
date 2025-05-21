using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nauther.Identity.Domain.Entities;

namespace Nauther.Identity.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> entity)
    {
        entity.HasKey(u => u.Id);
        
        entity.HasOne(u => u.UserCredential)
            .WithOne(uc => uc.User)
            .HasForeignKey<UserCredential>(uc => uc.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        
        entity.HasMany(u => u.UserRoles)
            .WithOne(ur => ur.User)
            .HasForeignKey(ur => ur.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        entity.HasMany(u => u.UserPermissions)
            .WithOne(up => up.User)
            .HasForeignKey(up => up.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        
        entity.HasMany(u => u.UserGroups)
            .WithOne(upg => upg.User)
            .HasForeignKey(upg => upg.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
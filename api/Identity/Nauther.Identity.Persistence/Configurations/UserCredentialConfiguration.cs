using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nauther.Identity.Domain.Entities;

namespace Nauther.Identity.Persistence.Configurations;

public class UserCredentialConfiguration : IEntityTypeConfiguration<UserCredential>
{
    public void Configure(EntityTypeBuilder<UserCredential> entity)
    {
        entity.HasKey(uc => uc.UserId);

        entity.Property(uc => uc.PasswordHash).HasMaxLength(500).IsRequired();

        entity.HasOne<User>()
            .WithOne()
            .HasForeignKey<UserCredential>(a => a.UserId)
            .HasPrincipalKey<User>(a => a.Id)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
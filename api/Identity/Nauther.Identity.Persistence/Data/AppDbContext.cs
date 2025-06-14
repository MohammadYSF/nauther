using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Options;
using Nauther.Framework.Domain.Common;
using Nauther.Framework.Infrastructure.Authorization.JwtToken;
using Nauther.Identity.Domain.Entities;
using Nauther.Identity.Infrastructure.Utilities;
using Nauther.Identity.Infrastructure.Utilities.PasswordHash;

namespace Nauther.Identity.Persistence.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options,
    IOptions<DefaultSuperAdminConfiguration> adminOpitopns,
    IPasswordHasherService passwordHasherService) : DbContext(options)
{
    private readonly DefaultSuperAdminConfiguration _defaultSuperAdminConfiguration=adminOpitopns.Value;

    public DbSet<User> Users { get; set; }
    public DbSet<UserCredential> UserCredentials { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    public DbSet<RolePermission> RolePermissions { get; set; }
    public DbSet<Permission> Permissions { get; set; }
    public DbSet<UserPermission> UserPermissions { get; set; }
    public DbSet<Group> Groups { get; set; }
    public DbSet<GroupPermission> GroupPermissions { get; set; }
    public DbSet<UserGroup> UserGroups { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.EnableSensitiveDataLogging();
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        SeedDataExtension.Seed(modelBuilder,_defaultSuperAdminConfiguration,passwordHasherService);
    }
    
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        var authRepository = this.GetService<IAuthUserService>();
        foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedAt = DateTime.UtcNow;
                    entry.Entity.CreatedBy = (await authRepository.GetUserByTokenAsync())?.UserId ?? Guid.Empty;
                    break;
                case EntityState.Modified:
                    entry.Entity.UpdatedAt = DateTime.UtcNow;
                    entry.Entity.UpdatedBy = (await authRepository.GetUserByTokenAsync())?.UserId ?? Guid.Empty;
                    break;
            }
        }

        return await base.SaveChangesAsync(cancellationToken);
    }
}
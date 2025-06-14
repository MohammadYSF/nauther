using Microsoft.EntityFrameworkCore;
using Nauther.Identity.Application.Resources;
using Nauther.Identity.Domain.Entities;
using Nauther.Identity.Infrastructure.Utilities;
using Nauther.Identity.Infrastructure.Utilities.PasswordHash;

namespace Nauther.Identity.Persistence.Data;

public static class SeedDataExtension //TODO:AddPermission
{
    public static void Seed(ModelBuilder modelBuilder,DefaultSuperAdminConfiguration  defaultSuperAdminConfiguration,
        IPasswordHasherService passwordHasherService)
    {
        var user = new User()
        {
            Id = defaultSuperAdminConfiguration.Id
        };
        var userCredential = new UserCredential()
        {
            PasswordHash = defaultSuperAdminConfiguration.PasswordHash,
            UserId = defaultSuperAdminConfiguration.Id
        };
        // user.UserCredential = userCredential;
        modelBuilder.Entity<User>().HasData([user]);
        modelBuilder.Entity<UserCredential>().HasData([userCredential]);
      
 List<Permission> permissions = new List<Permission>()
        {
            new Permission()
            {
                Id = Guid.Parse("093d581c-28bb-4700-83a2-429d21765ee6"),
                DisplayName = Messages.VIEW + " " + Messages.PERMISSION,
                Name = "ViewPermission"
            },
            new Permission()
            {
                Id = Guid.Parse("a40cda85-a303-46d7-8350-cbed507efb12"),
                DisplayName = Messages.CREATE + " " + Messages.PERMISSION,
                Name = "AddPermission"
            },
            new Permission()
            {
                Id = Guid.Parse("7c52899e-c7b1-4074-95fc-4257120b5b29"),
                DisplayName = Messages.EDIT + " " + Messages.PERMISSION,
                Name = "EditPermission"
            },
            new Permission()
            {
                Id = Guid.Parse("be05bebe-21e7-4d7f-964f-ce4030373ac6"),
                DisplayName = Messages.DELETE + " " + Messages.PERMISSION,
                Name = "DeletePermission"
            },
            new Permission()
            {
                Id = Guid.Parse("9a755e5d-4380-49ab-bae2-06480b8e9476"),
                DisplayName = Messages.VIEW + " " + Messages.ROLE,
                Name = "ViewRole"
            },
            new Permission()
            {
                Id = Guid.Parse("62ec4f35-44ca-4c63-82b9-689d2bfde0c6"),
                DisplayName = Messages.EDIT + " " + Messages.ROLE,
                Name = "EditRole"
            },
            new Permission()
            {
                Id = Guid.Parse("0f5c4756-d479-4029-a1c5-165f7385accb"),
                DisplayName = Messages.CREATE + " " + Messages.ROLE,
                Name = "AddRole"
            },
            new Permission()
            {
                Id = Guid.Parse("0010928a-3381-46aa-b9b7-c2db83dd0f82"),
                DisplayName = Messages.DELETE + " " + Messages.ROLE,
                Name = "DeleteRole"
            },
            new Permission()
            {
                Id = Guid.Parse("9a5a8465-cb5b-44eb-bbe5-16a463a03c37"),
                DisplayName = Messages.VIEW + " " + Messages.ADMIN,
                Name = "ViewAdmin"
            },
            new Permission()
            {
                Id = Guid.Parse("3180f21d-0785-4293-a8fe-281a533b768c"),
                DisplayName = Messages.DELETE + " " + Messages.ADMIN,
                Name = "DeleteAdmin"
            },
            new Permission()
            {
                Id = Guid.Parse("c1a243db-a5d5-4d5c-bae4-02a8ccf5fd33"),
                DisplayName = Messages.CREATE + " " + Messages.ADMIN,
                Name = "AddAdmin"
            },
            new Permission()
            {
                Id = Guid.Parse("8ca6c12d-a066-4859-a3a0-f786de32821c"),
                DisplayName = Messages.EDIT + " " + Messages.ADMIN,
                Name = "EditAdmin"
            },
        };
        modelBuilder.Entity<Permission>().HasData(permissions
        );
        var superAdminRole = new Role()
        {
            Id = Guid.Parse("081e66fc-527d-47a7-941f-7af16e95e738"),
            DisplayName = Messages.ROLE + " " + Messages.ADMIN,
            Name = "Admin"
        };
        modelBuilder.Entity<Role>().HasData([superAdminRole]);
        List<RolePermission> rolePermissions = [];
        foreach (var item in permissions)
        {
            var rolePermission = new RolePermission()
            {
                RoleId = superAdminRole.Id,
                PermissionId = item.Id
            };
            rolePermissions.Add(rolePermission);
        }

        var userRole = new UserRole()
        {
            RoleId = superAdminRole.Id,
            UserId = user.Id
        };
        modelBuilder.Entity<UserRole>().HasData(userRole);
        modelBuilder.Entity<RolePermission>().HasData(rolePermissions);
    }
}
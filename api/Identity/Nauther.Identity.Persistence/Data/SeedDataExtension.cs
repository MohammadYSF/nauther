using Microsoft.EntityFrameworkCore;
using Nauther.Identity.Application.Resources;
using Nauther.Identity.Domain.Entities;

namespace Nauther.Identity.Persistence.Data;

public static class SeedDataExtension //TODO:AddPermission
{
    public static void Seed(ModelBuilder modelBuilder)
    {
        SeedPermissions(modelBuilder);
        SeedRoles(modelBuilder);
        SeedRolePermissions(modelBuilder);
    }

    private static void SeedPermissions(ModelBuilder modelBuilder)
    {
        var permissions = new List<(Guid Id, string Name, string DisplayName)>
        {
            //(Guid.Parse("541CC258-A23E-42A4-B681-3240BD44565B"), "GetAllGroups",),
            //(Guid.Parse("4EBC2C2D-B57A-4714-A267-337C00CFBF0C"), "GetGroupByName"),
            //(Guid.Parse("D7B02687-990F-4D0D-8464-3A3AD2F70F93"), "GetGroupById"),
            //(Guid.Parse("BACA0F7B-7837-4B33-9052-3FC64532B294"), "CreateGroup"),
            //(Guid.Parse("C1E5F8F3-37C1-495D-9968-4BDAB20B6467"), "CreateGroupPermission"),
            //(Guid.Parse("F91B2819-EB5A-4845-99ED-4D281DCE5414"), "GetAllPermissions"),
            (Guid.Parse("67B1E085-D1FF-496A-9ADB-5C57A2C0BB3D"), "ViewPermission",Messages.VIEW_PERMISSIONS),
            (Guid.Parse("2379B9D4-CA76-467B-BEA7-6BE08BCEF55C"), "CreatePermission",Messages.CREATE_PERMISSION),
            (Guid.Parse("2479B9D4-CA76-467B-BEA7-6BE08BCEF55C"), "EditPermission",Messages.EDIT_PERMISSION),
            (Guid.Parse("2579B9D4-CA76-467B-BEA7-6BE08BCEF55C"), "DeletePermission",Messages.DELETE_PERMISSION)
            //(Guid.Parse("AB7E9CAD-3089-4C83-AA7D-7C4CC35BD462"), "GetAllRoles"),
            //(Guid.Parse("DC9A9190-FB09-4ECF-8205-80F18AC14906"), "GetRoleByName"),
            //(Guid.Parse("97FAFD05-5297-4D28-B3BF-8CFE3CB6E5E1"), "GetRoleById"),
            //(Guid.Parse("AAF3762F-2271-4CF4-96F5-8D5EAF390A72"), "CreateRole"),
            //(Guid.Parse("4FF76E7F-7080-4B45-B2BC-9331559BB5FA"), "CreateRolePermission"),
            //(Guid.Parse("2EC312E7-4ED7-4EAC-A853-9398F8BFE87C"), "GetAllUsers"),
            //(Guid.Parse("D875B7C9-3EDA-4007-A9F5-956450A2EE02"), "GetUserDetail"),
            //(Guid.Parse("C37C06D8-6139-4E7C-9A9C-9AEE1F2F1E42"), "CreateUserGroup"),
            //(Guid.Parse("43DA1A86-2C5D-414B-878F-A2DA33E06922"), "CreateUserPermission"),
            //(Guid.Parse("73FA46DB-649F-463A-9380-A98F880B7D5E"), "CreateUserRole")
        };

        modelBuilder.Entity<Permission>().HasData(
            permissions.ConvertAll(p => new Permission
            {
                Id = p.Id,
                Name = p.Name,
                DisplayName = p.DisplayName
            })
        );
    }

    private static void SeedRoles(ModelBuilder modelBuilder)
    {
        var roles = new List<(Guid Id, string Name, string DisplayName)>
        {
            (Guid.Parse("BD7A5B0B-3059-4FAB-8C36-B385B8BAA9C9"), "Admin",Messages.ADMIN),
            (Guid.Parse("717D7A4A-D864-4774-8AE0-5010E745D87E"), "User",Messages.USER)
        };

        modelBuilder.Entity<Role>().HasData(
            roles.ConvertAll(r => new Role
            {
                Id = r.Id,
                Name = r.Name,
                DisplayName = r.DisplayName
            })
        );
    }

    private static void SeedRolePermissions(ModelBuilder modelBuilder)
    {
        var rolePermissions = new List<RolePermission>
        {

        };

        modelBuilder.Entity<RolePermission>().HasData(rolePermissions);
    }
}
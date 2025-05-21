using Microsoft.EntityFrameworkCore;
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
        var permissions = new List<(Guid Id, string Name)>
        {
            (Guid.Parse("541CC258-A23E-42A4-B681-3240BD44565B"), "GetAllGroups"),
            (Guid.Parse("4EBC2C2D-B57A-4714-A267-337C00CFBF0C"), "GetGroupByName"),
            (Guid.Parse("D7B02687-990F-4D0D-8464-3A3AD2F70F93"), "GetGroupById"),
            (Guid.Parse("BACA0F7B-7837-4B33-9052-3FC64532B294"), "CreateGroup"),
            (Guid.Parse("C1E5F8F3-37C1-495D-9968-4BDAB20B6467"), "CreateGroupPermission"),
            (Guid.Parse("F91B2819-EB5A-4845-99ED-4D281DCE5414"), "GetAllPermissions"),
            (Guid.Parse("88468F14-C320-42F1-A95D-58EDF5F3BB01"), "GetPermissionByName"),
            (Guid.Parse("67B1E085-D1FF-496A-9ADB-5C57A2C0BB3D"), "GetPermissionById"),
            (Guid.Parse("2379B9D4-CA76-467B-BEA7-6BE08BCEF55C"), "CreatePermission"),
            (Guid.Parse("AB7E9CAD-3089-4C83-AA7D-7C4CC35BD462"), "GetAllRoles"),
            (Guid.Parse("DC9A9190-FB09-4ECF-8205-80F18AC14906"), "GetRoleByName"),
            (Guid.Parse("97FAFD05-5297-4D28-B3BF-8CFE3CB6E5E1"), "GetRoleById"),
            (Guid.Parse("AAF3762F-2271-4CF4-96F5-8D5EAF390A72"), "CreateRole"),
            (Guid.Parse("4FF76E7F-7080-4B45-B2BC-9331559BB5FA"), "CreateRolePermission"),
            (Guid.Parse("2EC312E7-4ED7-4EAC-A853-9398F8BFE87C"), "GetAllUsers"),
            (Guid.Parse("D875B7C9-3EDA-4007-A9F5-956450A2EE02"), "GetUserDetail"),
            (Guid.Parse("C37C06D8-6139-4E7C-9A9C-9AEE1F2F1E42"), "CreateUserGroup"),
            (Guid.Parse("43DA1A86-2C5D-414B-878F-A2DA33E06922"), "CreateUserPermission"),
            (Guid.Parse("73FA46DB-649F-463A-9380-A98F880B7D5E"), "CreateUserRole")
        };

        modelBuilder.Entity<Permission>().HasData(
            permissions.ConvertAll(p => new Permission
            {
                Id = p.Id,
                Name = p.Name
            })
        );
    }

    private static void SeedRoles(ModelBuilder modelBuilder)
    {
        var roles = new List<(Guid Id, string Name)>
        {
            (Guid.Parse("BD7A5B0B-3059-4FAB-8C36-B385B8BAA9C9"), "Admin"),
            (Guid.Parse("717D7A4A-D864-4774-8AE0-5010E745D87E"), "User")
        };

        modelBuilder.Entity<Role>().HasData(
            roles.ConvertAll(r => new Role
            {
                Id = r.Id,
                Name = r.Name
            })
        );
    }

    private static void SeedRolePermissions(ModelBuilder modelBuilder)
    {
        var rolePermissions = new List<RolePermission>
        {
            new()
            {
                Id = Guid.Parse("5148F413-281C-489D-8DB6-E3401251A69C"),
                RoleId = Guid.Parse("BD7A5B0B-3059-4FAB-8C36-B385B8BAA9C9"),
                PermissionId = Guid.Parse("6FCAC8C9-C0E0-45E8-BEDF-07A758A9D86A"),
                CreatedBy = Guid.Empty,
                CreatedAt = DateTime.Parse("2025-04-19T15:00:56.0363387")
            },
            new()
            {
                Id = Guid.Parse("A9D36113-C8DA-4701-8E03-8B554CCD8669"),
                RoleId = Guid.Parse("BD7A5B0B-3059-4FAB-8C36-B385B8BAA9C9"),
                PermissionId = Guid.Parse("A6C055F2-9C85-44FB-BEC1-0EC1ABD552C6"),
                CreatedBy = Guid.Empty,
                CreatedAt = DateTime.Parse("2025-04-19T15:00:56.0363387")
            },
            new()
            {
                Id = Guid.Parse("5BDBC803-D4CC-4F5D-8148-BD15910A287B"),
                RoleId = Guid.Parse("BD7A5B0B-3059-4FAB-8C36-B385B8BAA9C9"),
                PermissionId = Guid.Parse("CC3BEF25-A9A5-461B-9F54-2D882570456F"),
                CreatedBy = Guid.Empty,
                CreatedAt = DateTime.Parse("2025-04-19T15:00:56.0363387")
            },
            new()
            {
                Id = Guid.Parse("16EAB8FE-7434-4222-ADF2-4DAA926D3B17"),
                RoleId = Guid.Parse("BD7A5B0B-3059-4FAB-8C36-B385B8BAA9C9"),
                PermissionId = Guid.Parse("541CC258-A23E-42A4-B681-3240BD44565B"),
                CreatedBy = Guid.Empty,
                CreatedAt = DateTime.Parse("2025-04-19T15:00:56.0363387")
            },
            new()
            {
                Id = Guid.Parse("9ECA2F8C-4D40-479E-AC09-FE1415BB27FE"),
                RoleId = Guid.Parse("BD7A5B0B-3059-4FAB-8C36-B385B8BAA9C9"),
                PermissionId = Guid.Parse("4EBC2C2D-B57A-4714-A267-337C00CFBF0C"),
                CreatedBy = Guid.Empty,
                CreatedAt = DateTime.Parse("2025-04-19T15:00:56.0363387")
            },
            new()
            {
                Id = Guid.Parse("F9710CA7-4154-463E-9B32-6DB35F3351BF"),
                RoleId = Guid.Parse("BD7A5B0B-3059-4FAB-8C36-B385B8BAA9C9"),
                PermissionId = Guid.Parse("D7B02687-990F-4D0D-8464-3A3AD2F70F93"),
                CreatedBy = Guid.Empty,
                CreatedAt = DateTime.Parse("2025-04-19T15:00:56.0363387")
            },
            new()
            {
                Id = Guid.Parse("C912CADA-F765-44EC-BBE4-BDA8632F480F"),
                RoleId = Guid.Parse("BD7A5B0B-3059-4FAB-8C36-B385B8BAA9C9"),
                PermissionId = Guid.Parse("BACA0F7B-7837-4B33-9052-3FC64532B294"),
                CreatedBy = Guid.Empty,
                CreatedAt = DateTime.Parse("2025-04-19T15:00:56.0363387")
            },
            new()
            {
                Id = Guid.Parse("F70CC5E7-B2CE-4354-BABB-41A053FA6F45"),
                RoleId = Guid.Parse("BD7A5B0B-3059-4FAB-8C36-B385B8BAA9C9"),
                PermissionId = Guid.Parse("C1E5F8F3-37C1-495D-9968-4BDAB20B6467"),
                CreatedBy = Guid.Empty,
                CreatedAt = DateTime.Parse("2025-04-19T15:00:56.0363387")
            },
            new()
            {
                Id = Guid.Parse("36972925-5D8C-4EB1-BCE8-9AC1DE8A7CF1"),
                RoleId = Guid.Parse("BD7A5B0B-3059-4FAB-8C36-B385B8BAA9C9"),
                PermissionId = Guid.Parse("F91B2819-EB5A-4845-99ED-4D281DCE5414"),
                CreatedBy = Guid.Empty,
                CreatedAt = DateTime.Parse("2025-04-19T15:00:56.0363387")
            },
            new()
            {
                Id = Guid.Parse("19875C30-6CE3-4D4B-9D21-115B3CA4E4E7"),
                RoleId = Guid.Parse("BD7A5B0B-3059-4FAB-8C36-B385B8BAA9C9"),
                PermissionId = Guid.Parse("88468F14-C320-42F1-A95D-58EDF5F3BB01"),
                CreatedBy = Guid.Empty,
                CreatedAt = DateTime.Parse("2025-04-19T15:00:56.0363387")
            },
            new()
            {
                Id = Guid.Parse("095F218A-0348-459D-B2E0-75237A887DF7"),
                RoleId = Guid.Parse("BD7A5B0B-3059-4FAB-8C36-B385B8BAA9C9"),
                PermissionId = Guid.Parse("67B1E085-D1FF-496A-9ADB-5C57A2C0BB3D"),
                CreatedBy = Guid.Empty,
                CreatedAt = DateTime.Parse("2025-04-19T15:00:56.0363387")
            },
            new()
            {
                Id = Guid.Parse("16E7A924-13D5-401D-A016-D2A2E1044C6F"),
                RoleId = Guid.Parse("BD7A5B0B-3059-4FAB-8C36-B385B8BAA9C9"),
                PermissionId = Guid.Parse("2379B9D4-CA76-467B-BEA7-6BE08BCEF55C"),
                CreatedBy = Guid.Empty,
                CreatedAt = DateTime.Parse("2025-04-19T15:00:56.0363387")
            },
            new()
            {
                Id = Guid.Parse("76EA686A-1153-437F-8E32-A3CD6C1AFBDC"),
                RoleId = Guid.Parse("BD7A5B0B-3059-4FAB-8C36-B385B8BAA9C9"),
                PermissionId = Guid.Parse("AB7E9CAD-3089-4C83-AA7D-7C4CC35BD462"),
                CreatedBy = Guid.Empty,
                CreatedAt = DateTime.Parse("2025-04-19T15:00:56.0363387")
            },
            new()
            {
                Id = Guid.Parse("ADF36BA8-4940-4E00-9109-C924BEE1FFBC"),
                RoleId = Guid.Parse("BD7A5B0B-3059-4FAB-8C36-B385B8BAA9C9"),
                PermissionId = Guid.Parse("DC9A9190-FB09-4ECF-8205-80F18AC14906"),
                CreatedBy = Guid.Empty,
                CreatedAt = DateTime.Parse("2025-04-19T15:00:56.0363387")
            },
            new()
            {
                Id = Guid.Parse("872C21A9-A168-4453-A5E2-06D8BCFD1556"),
                RoleId = Guid.Parse("BD7A5B0B-3059-4FAB-8C36-B385B8BAA9C9"),
                PermissionId = Guid.Parse("97FAFD05-5297-4D28-B3BF-8CFE3CB6E5E1"),
                CreatedBy = Guid.Empty,
                CreatedAt = DateTime.Parse("2025-04-19T15:00:56.0363387")
            },
            new()
            {
                Id = Guid.Parse("2AA5C80D-A8B8-4246-B39B-8878B696D62C"),
                RoleId = Guid.Parse("BD7A5B0B-3059-4FAB-8C36-B385B8BAA9C9"),
                PermissionId = Guid.Parse("AAF3762F-2271-4CF4-96F5-8D5EAF390A72"),
                CreatedBy = Guid.Empty,
                CreatedAt = DateTime.Parse("2025-04-19T15:00:56.0363387")
            },
            new()
            {
                Id = Guid.Parse("633F425C-E6C5-4DBB-A99F-DD15D4857D39"),
                RoleId = Guid.Parse("BD7A5B0B-3059-4FAB-8C36-B385B8BAA9C9"),
                PermissionId = Guid.Parse("4FF76E7F-7080-4B45-B2BC-9331559BB5FA"),
                CreatedBy = Guid.Empty,
                CreatedAt = DateTime.Parse("2025-04-19T15:00:56.0363387")
            },
            new()
            {
                Id = Guid.Parse("4ED2A8F7-A77B-488A-B7C8-EC5CEC3C3E40"),
                RoleId = Guid.Parse("BD7A5B0B-3059-4FAB-8C36-B385B8BAA9C9"),
                PermissionId = Guid.Parse("2EC312E7-4ED7-4EAC-A853-9398F8BFE87C"),
                CreatedBy = Guid.Empty,
                CreatedAt = DateTime.Parse("2025-04-19T15:00:56.0363387")
            },
            new()
            {
                Id = Guid.Parse("7A468D5C-31FE-4131-BE61-8CE11E714D53"),
                RoleId = Guid.Parse("BD7A5B0B-3059-4FAB-8C36-B385B8BAA9C9"),
                PermissionId = Guid.Parse("D875B7C9-3EDA-4007-A9F5-956450A2EE02"),
                CreatedBy = Guid.Empty,
                CreatedAt = DateTime.Parse("2025-04-19T15:00:56.0363387")
            },
            new()
            {
                Id = Guid.Parse("F8CF6C6D-5DDD-4147-8244-E3A075D3C91F"),
                RoleId = Guid.Parse("BD7A5B0B-3059-4FAB-8C36-B385B8BAA9C9"),
                PermissionId = Guid.Parse("C37C06D8-6139-4E7C-9A9C-9AEE1F2F1E42"),
                CreatedBy = Guid.Empty,
                CreatedAt = DateTime.Parse("2025-04-19T15:00:56.0363387")
            },
            new()
            {
                Id = Guid.Parse("F3E5F1F9-7772-4C8B-BA4D-B5FC4634734A"),
                RoleId = Guid.Parse("BD7A5B0B-3059-4FAB-8C36-B385B8BAA9C9"),
                PermissionId = Guid.Parse("43DA1A86-2C5D-414B-878F-A2DA33E06922"),
                CreatedBy = Guid.Empty,
                CreatedAt = DateTime.Parse("2025-04-19T15:00:56.0363387")
            },
            new()
            {
                Id = Guid.Parse("540813C8-38D4-4B74-B174-A2153C97ABF9"),
                RoleId = Guid.Parse("BD7A5B0B-3059-4FAB-8C36-B385B8BAA9C9"),
                PermissionId = Guid.Parse("73FA46DB-649F-463A-9380-A98F880B7D5E"),
                CreatedBy = Guid.Empty,
                CreatedAt = DateTime.Parse("2025-04-19T15:00:56.0363387")
            }           
        };

        modelBuilder.Entity<RolePermission>().HasData(rolePermissions);
    }
}
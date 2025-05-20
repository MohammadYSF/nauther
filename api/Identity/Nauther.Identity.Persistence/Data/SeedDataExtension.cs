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
            (Guid.Parse("73FA46DB-649F-463A-9380-A98F880B7D5E"), "CreateUserRole"),
            (Guid.Parse("F87811D3-958B-49FE-AB4C-AC911AD37447"), "AddActivityLicense"),
            (Guid.Parse("CBED817D-891C-481D-85AD-B250A50EBBA1"), "AddCertificate"),
            (Guid.Parse("BEA47F9A-45A8-4C64-B5D5-B86227968BB7"), "RegisterCompanyBaseInfo"),
            (Guid.Parse("663B4A3F-5256-4CB3-82C3-BB232FFCA641"), "UpdateCompanyBaseInfo"),
            (Guid.Parse("C2F0E1F7-F514-4702-AA58-C1FDDFBB0E28"), "AddCompanyHumanResource"),
            (Guid.Parse("85613123-A871-42CB-B580-C599954445C1"), "AddFieldOfActivity"),
            (Guid.Parse("FADCE983-829A-4900-A77C-C8D68CC22923"), "AddPPE"),
            (Guid.Parse("F085C7A7-4802-4F42-8220-CDE9B2F52C7A"), "AddRnD"),
            (Guid.Parse("6E8DB7F5-61FA-49FF-AC60-D5290B0091FA"), "AddWorkExperience"),
            (Guid.Parse("1FD525DD-6D5E-4F60-99A0-EA96126F3560"), "GetAllCompanies"),
            (Guid.Parse("B3084E6B-245B-4CF5-9393-F48964EE651E"), "GetCompanyById"),
            (Guid.Parse("7B3B02B6-3521-4B5B-86C2-002D72241140"), "GetFieldOfActivitiesByCompanyId"),
            (Guid.Parse("72FA6089-A915-4730-A1EA-B6207F50CA38"), "GetActivityLicensesByCompanyId"),
            (Guid.Parse("BAD5EDE7-497E-45A2-A73E-BCA401D9F31A"), "GetCertificateByCompanyId"),
            (Guid.Parse("E8EC273A-E412-4739-8209-3C34AE136D60"), "GetCompanyHumanResourceByCompanyId"),
            (Guid.Parse("87392D3B-27C5-46A8-8651-8BFCB7410189"), "GetPPEByCompanyId"),
            (Guid.Parse("49B8B2E8-403C-4D13-9B36-77265C27DA67"), "GetRnDByCompanyId"),
            (Guid.Parse("2875FC32-4198-4DB3-BCB5-9218A7482487"), "GetWorkExperienceByCompanyId"),
            (Guid.Parse("D41B0FFA-319C-4641-AC2D-2CC6FEA9A12C"), "UpdateFieldOfActivity"),
            (Guid.Parse("59E3036E-2F1B-4888-8C88-4DFD7B4274FE"), "UpdateActivityLicense")
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
            },
            new()
            {
                Id = Guid.Parse("66E0821E-14AA-40B3-B9B3-C9A7A82BF5E0"),
                RoleId = Guid.Parse("BD7A5B0B-3059-4FAB-8C36-B385B8BAA9C9"),
                PermissionId = Guid.Parse("F87811D3-958B-49FE-AB4C-AC911AD37447"),
                CreatedBy = Guid.Empty,
                CreatedAt = DateTime.Parse("2025-04-19T15:00:56.0363387")
            },
            new()
            {
                Id = Guid.Parse("42F6CD33-F65A-48A0-A28E-D12A3D755CB6"),
                RoleId = Guid.Parse("BD7A5B0B-3059-4FAB-8C36-B385B8BAA9C9"),
                PermissionId = Guid.Parse("CBED817D-891C-481D-85AD-B250A50EBBA1"),
                CreatedBy = Guid.Empty,
                CreatedAt = DateTime.Parse("2025-04-19T15:00:56.0363387")
            },
            new()
            {
                Id = Guid.Parse("E5E5F452-21D4-452A-A168-9DA6E5CCD208"),
                RoleId = Guid.Parse("BD7A5B0B-3059-4FAB-8C36-B385B8BAA9C9"),
                PermissionId = Guid.Parse("BEA47F9A-45A8-4C64-B5D5-B86227968BB7"),
                CreatedBy = Guid.Empty,
                CreatedAt = DateTime.Parse("2025-04-19T15:00:56.0363387")
            },
            new()
            {
                Id = Guid.Parse("7A67E2EA-7A3D-4E7F-A9F8-FC98039915E4"),
                RoleId = Guid.Parse("BD7A5B0B-3059-4FAB-8C36-B385B8BAA9C9"),
                PermissionId = Guid.Parse("663B4A3F-5256-4CB3-82C3-BB232FFCA641"),
                CreatedBy = Guid.Empty,
                CreatedAt = DateTime.Parse("2025-04-19T15:00:56.0363387")
            },
            new()
            {
                Id = Guid.Parse("CF73C899-D7C1-4A43-96FA-CCD4AA0E9858"),
                RoleId = Guid.Parse("BD7A5B0B-3059-4FAB-8C36-B385B8BAA9C9"),
                PermissionId = Guid.Parse("C2F0E1F7-F514-4702-AA58-C1FDDFBB0E28"),
                CreatedBy = Guid.Empty,
                CreatedAt = DateTime.Parse("2025-04-19T15:00:56.0363387")
            },
            new()
            {
                Id = Guid.Parse("E9AA9EEF-D00A-4861-9821-BC9DE0B16FC7"),
                RoleId = Guid.Parse("BD7A5B0B-3059-4FAB-8C36-B385B8BAA9C9"),
                PermissionId = Guid.Parse("85613123-A871-42CB-B580-C599954445C1"),
                CreatedBy = Guid.Empty,
                CreatedAt = DateTime.Parse("2025-04-19T15:00:56.0363387")
            },
            new()
            {
                Id = Guid.Parse("5B985DFD-4FF0-4EE5-98FA-32636F31D44A"),
                RoleId = Guid.Parse("BD7A5B0B-3059-4FAB-8C36-B385B8BAA9C9"),
                PermissionId = Guid.Parse("FADCE983-829A-4900-A77C-C8D68CC22923"),
                CreatedBy = Guid.Empty,
                CreatedAt = DateTime.Parse("2025-04-19T15:00:56.0363387")
            },
            new()
            {
                Id = Guid.Parse("28FDD323-17A4-4776-BCED-E724922B62B4"),
                RoleId = Guid.Parse("BD7A5B0B-3059-4FAB-8C36-B385B8BAA9C9"),
                PermissionId = Guid.Parse("F085C7A7-4802-4F42-8220-CDE9B2F52C7A"),
                CreatedBy = Guid.Empty,
                CreatedAt = DateTime.Parse("2025-04-19T15:00:56.0363387")
            },
            new()
            {
                Id = Guid.Parse("B1BC7D34-0521-41C8-8032-B4312BAB5F0C"),
                RoleId = Guid.Parse("BD7A5B0B-3059-4FAB-8C36-B385B8BAA9C9"),
                PermissionId = Guid.Parse("6E8DB7F5-61FA-49FF-AC60-D5290B0091FA"),
                CreatedBy = Guid.Empty,
                CreatedAt = DateTime.Parse("2025-04-19T15:00:56.0363387")
            },
            new()
            {
                Id = Guid.Parse("61084070-1420-4C38-9C7D-3C560AD7EAD0"),
                RoleId = Guid.Parse("BD7A5B0B-3059-4FAB-8C36-B385B8BAA9C9"),
                PermissionId = Guid.Parse("1FD525DD-6D5E-4F60-99A0-EA96126F3560"),
                CreatedBy = Guid.Empty,
                CreatedAt = DateTime.Parse("2025-04-19T15:00:56.0363387")
            },
            new()
            {
                Id = Guid.Parse("C9C8AA97-9746-47BF-8E2A-23A8530BB425"),
                RoleId = Guid.Parse("BD7A5B0B-3059-4FAB-8C36-B385B8BAA9C9"),
                PermissionId = Guid.Parse("B3084E6B-245B-4CF5-9393-F48964EE651E"),
                CreatedBy = Guid.Empty,
                CreatedAt = DateTime.Parse("2025-04-19T15:00:56.0363387")
            },
            new()
            {
                Id = Guid.Parse("A25939CC-4989-4073-8C32-D02E42DC15A9"),
                RoleId = Guid.Parse("BD7A5B0B-3059-4FAB-8C36-B385B8BAA9C9"),
                PermissionId = Guid.Parse("7B3B02B6-3521-4B5B-86C2-002D72241140"),
                CreatedBy = Guid.Empty,
                CreatedAt = DateTime.Parse("2025-04-19T15:00:56.0363387")
            },
            new()
            {
                Id = Guid.Parse("854042BE-CF8A-4284-B3B1-9F486435306B"),
                RoleId = Guid.Parse("BD7A5B0B-3059-4FAB-8C36-B385B8BAA9C9"),
                PermissionId = Guid.Parse("72FA6089-A915-4730-A1EA-B6207F50CA38"),
                CreatedBy = Guid.Empty,
                CreatedAt = DateTime.Parse("2025-04-19T15:00:56.0363387")
            },
            new()
            {
                Id = Guid.Parse("C1F483C4-F61A-48D7-AC41-BA78F6B1305B"),
                RoleId = Guid.Parse("BD7A5B0B-3059-4FAB-8C36-B385B8BAA9C9"),
                PermissionId = Guid.Parse("2875FC32-4198-4DB3-BCB5-9218A7482487"),
                CreatedBy = Guid.Empty,
                CreatedAt = DateTime.Parse("2025-04-19T15:00:56.0363387")
            },
            new()
            {
                Id = Guid.Parse("61F91C35-030E-44AE-8456-A1128EDC8DCF"),
                RoleId = Guid.Parse("BD7A5B0B-3059-4FAB-8C36-B385B8BAA9C9"),
                PermissionId = Guid.Parse("49B8B2E8-403C-4D13-9B36-77265C27DA67"),
                CreatedBy = Guid.Empty,
                CreatedAt = DateTime.Parse("2025-04-19T15:00:56.0363387")
            },
            new()
            {
                Id = Guid.Parse("900379DF-31D4-46E3-B97F-92DA77D8ADDB"),
                RoleId = Guid.Parse("BD7A5B0B-3059-4FAB-8C36-B385B8BAA9C9"),
                PermissionId = Guid.Parse("87392D3B-27C5-46A8-8651-8BFCB7410189"),
                CreatedBy = Guid.Empty,
                CreatedAt = DateTime.Parse("2025-04-19T15:00:56.0363387")
            },
            new()
            {
                Id = Guid.Parse("65904DD7-5CDD-4F4E-8ADA-C27656C565AB"),
                RoleId = Guid.Parse("BD7A5B0B-3059-4FAB-8C36-B385B8BAA9C9"),
                PermissionId = Guid.Parse("E8EC273A-E412-4739-8209-3C34AE136D60"),
                CreatedBy = Guid.Empty,
                CreatedAt = DateTime.Parse("2025-04-19T15:00:56.0363387")
            },
            new()
            {
                Id = Guid.Parse("1969C968-167E-4CC5-8835-E045E49E67D7"),
                RoleId = Guid.Parse("BD7A5B0B-3059-4FAB-8C36-B385B8BAA9C9"),
                PermissionId = Guid.Parse("BAD5EDE7-497E-45A2-A73E-BCA401D9F31A"),
                CreatedBy = Guid.Empty,
                CreatedAt = DateTime.Parse("2025-04-19T15:00:56.0363387")
            },
            new()
            {
                Id = Guid.Parse("B0D10C47-552C-4CBE-8FE8-3D81BF6839FB"),
                RoleId = Guid.Parse("BD7A5B0B-3059-4FAB-8C36-B385B8BAA9C9"),
                PermissionId = Guid.Parse("59E3036E-2F1B-4888-8C88-4DFD7B4274FE"),
                CreatedBy = Guid.Empty,
                CreatedAt = DateTime.Parse("2025-04-19T15:00:56.0363387")
            },
            new()
            {
                Id = Guid.Parse("04FEF867-7E00-4648-BD04-D368FF35124C"),
                RoleId = Guid.Parse("BD7A5B0B-3059-4FAB-8C36-B385B8BAA9C9"),
                PermissionId = Guid.Parse("D41B0FFA-319C-4641-AC2D-2CC6FEA9A12C"),
                CreatedBy = Guid.Empty,
                CreatedAt = DateTime.Parse("2025-04-19T15:00:56.0363387")
            },
            //
            new()
            {
                Id = Guid.Parse("A6A7A5C0-9E9F-4E2E-AE0E-12FBD87C73E1"),
                RoleId = Guid.Parse("717D7A4A-D864-4774-8AE0-5010E745D87E"),
                PermissionId = Guid.Parse("2EC312E7-4ED7-4EAC-A853-9398F8BFE87C"),
                CreatedBy = Guid.Empty,
                CreatedAt = DateTime.Parse("2025-04-19T15:00:56.0363387")
            },
            new()
            {
                Id = Guid.Parse("BF6C67F1-9273-45A5-B292-871C81B1C9F4"),
                RoleId = Guid.Parse("717D7A4A-D864-4774-8AE0-5010E745D87E"),
                PermissionId = Guid.Parse("D875B7C9-3EDA-4007-A9F5-956450A2EE02"),
                CreatedBy = Guid.Empty,
                CreatedAt = DateTime.Parse("2025-04-19T15:00:56.0363387")
            },
            new()
            {
                Id = Guid.Parse("8B5D28A7-32AE-41D3-9F03-51E7B2545C7B"),
                RoleId = Guid.Parse("717D7A4A-D864-4774-8AE0-5010E745D87E"),
                PermissionId = Guid.Parse("F87811D3-958B-49FE-AB4C-AC911AD37447"),
                CreatedBy = Guid.Empty,
                CreatedAt = DateTime.Parse("2025-04-19T15:00:56.0363387")
            },
            new()
            {
                Id = Guid.Parse("C4D79018-E154-4C25-9188-98ECA1D60E9E"),
                RoleId = Guid.Parse("717D7A4A-D864-4774-8AE0-5010E745D87E"),
                PermissionId = Guid.Parse("CBED817D-891C-481D-85AD-B250A50EBBA1"),
                CreatedBy = Guid.Empty,
                CreatedAt = DateTime.Parse("2025-04-19T15:00:56.0363387")
            },
            new()
            {
                Id = Guid.Parse("B5071C99-C0E2-41E8-B556-70D9D0E8F770"),
                RoleId = Guid.Parse("717D7A4A-D864-4774-8AE0-5010E745D87E"),
                PermissionId = Guid.Parse("BEA47F9A-45A8-4C64-B5D5-B86227968BB7"),
                CreatedBy = Guid.Empty,
                CreatedAt = DateTime.Parse("2025-04-19T15:00:56.0363387")
            },
            new()
            {
                Id = Guid.Parse("D5E5B72C-6A9D-4706-A2F1-D3AE7675A125"),
                RoleId = Guid.Parse("717D7A4A-D864-4774-8AE0-5010E745D87E"),
                PermissionId = Guid.Parse("663B4A3F-5256-4CB3-82C3-BB232FFCA641"),
                CreatedBy = Guid.Empty,
                CreatedAt = DateTime.Parse("2025-04-19T15:00:56.0363387")
            },
            new()
            {
                Id = Guid.Parse("F2A46B51-0212-4C7C-B7D7-B29D3C96A059"),
                RoleId = Guid.Parse("717D7A4A-D864-4774-8AE0-5010E745D87E"),
                PermissionId = Guid.Parse("C2F0E1F7-F514-4702-AA58-C1FDDFBB0E28"),
                CreatedBy = Guid.Empty,
                CreatedAt = DateTime.Parse("2025-04-19T15:00:56.0363387")
            },
            new()
            {
                Id = Guid.Parse("F38364FC-5F5A-463F-A72E-12E79B469A04"),
                RoleId = Guid.Parse("717D7A4A-D864-4774-8AE0-5010E745D87E"),
                PermissionId = Guid.Parse("85613123-A871-42CB-B580-C599954445C1"),
                CreatedBy = Guid.Empty,
                CreatedAt = DateTime.Parse("2025-04-19T15:00:56.0363387")
            },
            new()
            {
                Id = Guid.Parse("EE289C0F-20A9-4B9F-9E2A-3795700D8A6E"),
                RoleId = Guid.Parse("717D7A4A-D864-4774-8AE0-5010E745D87E"),
                PermissionId = Guid.Parse("FADCE983-829A-4900-A77C-C8D68CC22923"),
                CreatedBy = Guid.Empty,
                CreatedAt = DateTime.Parse("2025-04-19T15:00:56.0363387")
            },
            new()
            {
                Id = Guid.Parse("67E8C87A-32B4-4D35-A1A7-FC4D58F44D2F"),
                RoleId = Guid.Parse("717D7A4A-D864-4774-8AE0-5010E745D87E"),
                PermissionId = Guid.Parse("F085C7A7-4802-4F42-8220-CDE9B2F52C7A"),
                CreatedBy = Guid.Empty,
                CreatedAt = DateTime.Parse("2025-04-19T15:00:56.0363387")
            },
            new()
            {
                Id = Guid.Parse("9A5608C5-73F9-48BB-90C5-C1DB19F5B727"),
                RoleId = Guid.Parse("717D7A4A-D864-4774-8AE0-5010E745D87E"),
                PermissionId = Guid.Parse("6E8DB7F5-61FA-49FF-AC60-D5290B0091FA"),
                CreatedBy = Guid.Empty,
                CreatedAt = DateTime.Parse("2025-04-19T15:00:56.0363387")
            },
            new()
            {
                Id = Guid.Parse("5A5F53F1-A745-430F-8E78-93CE9D46ACB0"),
                RoleId = Guid.Parse("717D7A4A-D864-4774-8AE0-5010E745D87E"),
                PermissionId = Guid.Parse("1FD525DD-6D5E-4F60-99A0-EA96126F3560"),
                CreatedBy = Guid.Empty,
                CreatedAt = DateTime.Parse("2025-04-19T15:00:56.0363387")
            },
            new()
            {
                Id = Guid.Parse("DE44ED72-6A82-441B-A8A1-AF5F3D37275C"),
                RoleId = Guid.Parse("717D7A4A-D864-4774-8AE0-5010E745D87E"),
                PermissionId = Guid.Parse("B3084E6B-245B-4CF5-9393-F48964EE651E"),
                CreatedBy = Guid.Empty,
                CreatedAt = DateTime.Parse("2025-04-19T15:00:56.0363387")
            },
            new()
            {
                Id = Guid.Parse("54D2D948-6BC3-43C4-9A3E-B52723A592FF"),
                RoleId = Guid.Parse("717D7A4A-D864-4774-8AE0-5010E745D87E"),
                PermissionId = Guid.Parse("7B3B02B6-3521-4B5B-86C2-002D72241140"),
                CreatedBy = Guid.Empty,
                CreatedAt = DateTime.Parse("2025-04-19T15:00:56.0363387")
            },
            new()
            {
                Id = Guid.Parse("22B74C51-EC3F-42E2-8C52-83DA4046E59A"),
                RoleId = Guid.Parse("717D7A4A-D864-4774-8AE0-5010E745D87E"),
                PermissionId = Guid.Parse("72FA6089-A915-4730-A1EA-B6207F50CA38"),
                CreatedBy = Guid.Empty,
                CreatedAt = DateTime.Parse("2025-04-19T15:00:56.0363387")
            },
            new()
            {
                Id = Guid.Parse("F1E9E4C7-CA13-46B1-9A28-69EE625E424F"),
                RoleId = Guid.Parse("717D7A4A-D864-4774-8AE0-5010E745D87E"),
                PermissionId = Guid.Parse("BAD5EDE7-497E-45A2-A73E-BCA401D9F31A"),
                CreatedBy = Guid.Empty,
                CreatedAt = DateTime.Parse("2025-04-19T15:00:56.0363387")
            },
            new()
            {
                Id = Guid.Parse("1738995E-4A7A-4164-B2A9-FF3A4DA7D1F0"),
                RoleId = Guid.Parse("717D7A4A-D864-4774-8AE0-5010E745D87E"),
                PermissionId = Guid.Parse("E8EC273A-E412-4739-8209-3C34AE136D60"),
                CreatedBy = Guid.Empty,
                CreatedAt = DateTime.Parse("2025-04-19T15:00:56.0363387")
            },
            new()
            {
                Id = Guid.Parse("33B1F640-5E8F-4A4A-8D63-2DAF34823067"),
                RoleId = Guid.Parse("717D7A4A-D864-4774-8AE0-5010E745D87E"),
                PermissionId = Guid.Parse("87392D3B-27C5-46A8-8651-8BFCB7410189"),
                CreatedBy = Guid.Empty,
                CreatedAt = DateTime.Parse("2025-04-19T15:00:56.0363387")
            },
            new()
            {
                Id = Guid.Parse("70FB4427-AE1F-4AE9-A8FE-5D4F7B6602A6"),
                RoleId = Guid.Parse("717D7A4A-D864-4774-8AE0-5010E745D87E"),
                PermissionId = Guid.Parse("49B8B2E8-403C-4D13-9B36-77265C27DA67"),
                CreatedBy = Guid.Empty,
                CreatedAt = DateTime.Parse("2025-04-19T15:00:56.0363387")
            },
            new()
            {
                Id = Guid.Parse("4FE4BC7E-42F1-4D83-8529-1394DB0D595D"),
                RoleId = Guid.Parse("717D7A4A-D864-4774-8AE0-5010E745D87E"),
                PermissionId = Guid.Parse("2875FC32-4198-4DB3-BCB5-9218A7482487"),
                CreatedBy = Guid.Empty,
                CreatedAt = DateTime.Parse("2025-04-19T15:00:56.0363387")
            },
            new()
            {
                Id = Guid.Parse("AEE4F144-6AA3-4186-9A68-A381FE05DCA9"),
                RoleId = Guid.Parse("717D7A4A-D864-4774-8AE0-5010E745D87E"),
                PermissionId = Guid.Parse("D41B0FFA-319C-4641-AC2D-2CC6FEA9A12C"),
                CreatedBy = Guid.Empty,
                CreatedAt = DateTime.Parse("2025-04-19T15:00:56.0363387")
            },
            new()
            {
                Id = Guid.Parse("9F9E75DC-1A1C-4973-80F4-98B0D5BB6A47"),
                RoleId = Guid.Parse("717D7A4A-D864-4774-8AE0-5010E745D87E"),
                PermissionId = Guid.Parse("59E3036E-2F1B-4888-8C88-4DFD7B4274FE"),
                CreatedBy = Guid.Empty,
                CreatedAt = DateTime.Parse("2025-04-19T15:00:56.0363387")
            }
        };

        modelBuilder.Entity<RolePermission>().HasData(rolePermissions);
    }
}
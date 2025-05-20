using AutoMapper;
using Nauther.Identity.Application.Features.RolePermission.Commands;
using Nauther.Identity.Application.Features.RolePermission.Commands.CreateRolePermissions;
using Nauther.Identity.Application.Features.UserRole.Commands.CreateUserRole;
using Nauther.Identity.Domain.Entities;

namespace Nauther.Identity.Application.Profiles.RolePermissions;

public partial class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<CreateRolePermissionDto, RolePermission>();
        CreateMap<RolePermission, CreateRolePermissionCommandResponse>()
            .ForMember(desc => desc.RolePermissionId, opt 
                => opt.MapFrom(src => src.Id));
    }
}
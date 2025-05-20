using AutoMapper;
using Nauther.Identity.Application.Features.Permission.Commands.CreatePermission;
using Nauther.Identity.Application.Features.Permission.Queries;
using Nauther.Identity.Application.Features.Role.Commands.CreateRole;
using Nauther.Identity.Application.Features.Role.Queries;
using Nauther.Identity.Domain.Entities;

namespace Nauther.Identity.Application.Profiles.Permissions;

public partial class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<CreatePermissionCommand, Permission>();

        CreateMap<Permission, GetPermissionsQueryResponse>();
        CreateMap<Permission, CreatePermissionCommandResponse>()
            .ForMember(des => des.PermissionId, opt 
                => opt.MapFrom(src => src.Id));
    }
}
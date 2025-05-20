using AutoMapper;
using Nauther.Identity.Application.Features.GroupPermission;
using Nauther.Identity.Application.Features.GroupPermission.Commands.CreateGroupPermissions;
using Nauther.Identity.Application.Features.RolePermission.Commands;
using Nauther.Identity.Application.Features.RolePermission.Commands.CreateRolePermissions;
using Nauther.Identity.Domain.Entities;

namespace Nauther.Identity.Application.Profiles.GroupPermissions;

public partial class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<CreateGroupPermissionDto, GroupPermission>();
        CreateMap<GroupPermission, CreateGroupPermissionCommandResponse>()
            .ForMember(desc => desc.GroupPermissionId, opt 
                => opt.MapFrom(src => src.Id));
    }
}
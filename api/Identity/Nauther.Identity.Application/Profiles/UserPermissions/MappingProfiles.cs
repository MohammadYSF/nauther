using AutoMapper;
using Nauther.Identity.Application.Features.RolePermission.Commands;
using Nauther.Identity.Application.Features.RolePermission.Commands.CreateRolePermissions;
using Nauther.Identity.Application.Features.UserPermission.Commands;
using Nauther.Identity.Application.Features.UserPermission.Commands.CreateUserPermissions;
using Nauther.Identity.Domain.Entities;

namespace Nauther.Identity.Application.Profiles.UserPermissions;

public partial class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<CreateUserPermissionDto, UserPermission>();
        CreateMap<UserPermission, CreateUserPermissionCommandResponse>();
    }
}
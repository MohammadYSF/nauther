using AutoMapper;
using Nauther.Identity.Application.Features.Role.Commands.CreateRole;
using Nauther.Identity.Application.Features.Role.Queries;
using Nauther.Identity.Domain.Entities;

namespace Nauther.Identity.Application.Profiles.Roles;

public partial class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<CreateRoleCommand, Role>();

        CreateMap<Role, GetRolesQueryResponse>();
        CreateMap<Role, CreateRoleCommandResponse>()
            .ForMember(des => des.RoleId, opt 
                => opt.MapFrom(src => src.Id));
    }
}
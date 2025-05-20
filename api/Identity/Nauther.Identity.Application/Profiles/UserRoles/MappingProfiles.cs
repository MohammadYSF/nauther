using AutoMapper;
using Nauther.Identity.Application.Features.UserRole.Commands;
using Nauther.Identity.Application.Features.UserRole.Commands.CreateUserRole;
using Nauther.Identity.Domain.Entities;

namespace Nauther.Identity.Application.Profiles.UserRoles;

public partial class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<CreateUserRoleDto, UserRole>();
        CreateMap<UserRole, CreateUserRoleCommandResponse>()
            .ForMember(desc => desc.UserRoleId, opt 
                => opt.MapFrom(src => src.Id));
    }
}
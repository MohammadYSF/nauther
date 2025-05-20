using AutoMapper;
using Nauther.Identity.Application.Features.UserGroup;
using Nauther.Identity.Application.Features.UserGroup.Commands.CreateUserRole;
using Nauther.Identity.Domain.Entities;

namespace Nauther.Identity.Application.Profiles.UserGroups;

public partial class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<CreateUserGroupDto, UserGroup>();
        CreateMap<UserGroup, CreateUserGroupCommandResponse>()
            .ForMember(desc => desc.UserGroupId, opt 
                => opt.MapFrom(src => src.Id));
    }
}
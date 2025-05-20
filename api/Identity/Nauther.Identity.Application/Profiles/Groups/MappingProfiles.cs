using AutoMapper;
using Nauther.Identity.Application.Features.Group.Commands.CreateGroup;
using Nauther.Identity.Application.Features.Group.Queries;
using Nauther.Identity.Application.Features.Role.Commands.CreateRole;
using Nauther.Identity.Application.Features.Role.Queries;
using Nauther.Identity.Domain.Entities;

namespace Nauther.Identity.Application.Profiles.Groups;

public partial class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<CreateGroupCommand, Group>();

        CreateMap<Group, GetGroupsQueryResponse>();
        CreateMap<Group, CreateGroupCommandResponse>()
            .ForMember(des => des.GroupId, opt 
                => opt.MapFrom(src => src.Id));
    }
}
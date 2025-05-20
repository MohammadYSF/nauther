using AutoMapper;
using Nauther.Framework.Shared.Responses;
using Nauther.Identity.Application.Features.Auth.Commands.LoginWithPassword;
using Nauther.Identity.Application.Features.Auth.Commands.Register;
using Nauther.Identity.Application.Features.Auth.Commands.VerifyNationalCode;
using Nauther.Identity.Application.Features.Auth.Commands.VerifyOtp;
using Nauther.Identity.Application.Features.User.Queries.GetUserDetail;
using Nauther.Identity.Application.Features.User.Queries.GetUsersList;
using Nauther.Identity.Domain.Entities;

namespace Nauther.Identity.Application.Profiles.Users;

public partial class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<User, GetUsersListQueryResponse>();
        CreateMap<User, GetUserDetailQueryResponse>();

        CreateMap<RegisterUserCommand, User>();
        CreateMap<User, RegisterUserCommandResponse>()
            .ForMember(dest => dest.UserId,
                opt
                    => opt.MapFrom(src => src.Id));

        CreateMap<User, VerifyNationalCodeCommandResponse>()
            .ForMember(dest => dest.UserId,
                opt
                    => opt.MapFrom(src => src.Id));

        CreateMap<string, LoginWithPasswordCommandResponse>()
            .ForMember(dest => dest.Access_Token,
                opt 
                    => opt.MapFrom(src => src));
        
        CreateMap<string, VerifyOtpCommandResponse>()
            .ForMember(dest => dest.Access_Token,
                opt 
                    => opt.MapFrom(src => src));
    }
}
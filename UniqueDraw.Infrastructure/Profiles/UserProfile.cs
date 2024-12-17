using AutoMapper;
using UniqueDraw.Domain.Entities.UniqueDraw;
using UniqueDraw.Domain.Models.Create;
using UniqueDraw.Domain.Models.Response;

namespace UniqueDraw.Infrastructure.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<UserCreateDTO, User>();
        CreateMap<User, UserResponseDTO>();
    }
}

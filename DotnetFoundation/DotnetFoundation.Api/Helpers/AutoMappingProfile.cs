using AutoMapper;
using DotnetFoundation.Application.Models.DTOs.UserDTO;
using DotnetFoundation.Domain.Entities;

namespace DotnetFoundation.Api.Helpers;

public class AutoMappingProfile : Profile
{
    public AutoMappingProfile()
    {
        CreateMap<User, UserResponse>();
    }
}

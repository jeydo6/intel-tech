using AutoMapper;
using IntelTech.Users.Application.Commands;
using IntelTech.Users.Presentation.Contracts;

namespace IntelTech.Users.Presentation.Mappings
{
    internal sealed class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CreateUserRequest, CreateUserCommand>();
        }
    }
}

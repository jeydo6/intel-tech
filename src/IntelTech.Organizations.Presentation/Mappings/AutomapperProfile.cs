using AutoMapper;
using IntelTech.Organizations.Application.Commands;
using IntelTech.Organizations.Application.Queries;
using IntelTech.Organizations.Presentation.Contracts;

namespace IntelTech.Organizations.Presentation.Mappings
{
    internal sealed class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<AddOrganizationUserRequest, AddOrganizationUserCommand>();
            CreateMap<GetOrganizationUsersRequest, GetOrganizationUsersQuery>();

            CreateMap<Application.Models.PaginationInfo, Domain.Models.PaginationInfo>()
                .ForMember(d => d.Offset, o => o.MapFrom(s => (s.PageNumber - 1) * s.PageSize))
                .ForMember(d => d.Limit, o => o.MapFrom(s => s.PageSize));

            CreateMap<Domain.Entities.User, Application.Models.User>();
        }
    }
}
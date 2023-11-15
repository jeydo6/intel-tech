using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using IntelTech.Organizations.Application.Commands;
using IntelTech.Organizations.Application.Queries;
using IntelTech.Organizations.Presentation.Contracts;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace IntelTech.Organizations.Presentation.Controllers;

[ApiController]
[Route("[controller]")]
public sealed class OrganizationsController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public OrganizationsController(
        IMediator mediator,
        IMapper mapper
    )
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost(nameof(AddUser))]
    public async Task AddUser([FromBody] AddOrganizationUserRequest request, CancellationToken cancellationToken)
    {
        var command = _mapper.Map<AddOrganizationUserCommand>(request);
        await _mediator.Send(command, cancellationToken);
    }

    [HttpPost(nameof(GetUsers))]
    public async Task<GetOrganizationUsersResponse> GetUsers([FromBody] GetOrganizationUsersRequest request, CancellationToken cancellationToken)
    {
        var query = _mapper.Map<GetOrganizationUsersQuery>(request);
        var result = await _mediator.Send(query, cancellationToken);

        return new GetOrganizationUsersResponse
        {
            Users = result
        };
    }
}

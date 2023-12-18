using System.Threading;
using System.Threading.Tasks;
using IntelTech.Organizations.Presentation.Contracts;
using IntelTech.Organizations.Presentation.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace IntelTech.Organizations.Presentation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public sealed class OrganizationsController
    {
        private readonly IMediator _mediator;

        public OrganizationsController(IMediator mediator)
            => _mediator = mediator;

        [HttpPost(nameof(AddUser))]
        public Task AddUser([FromBody] AddOrganizationUserRequest request, CancellationToken cancellationToken)
            => _mediator.Send(request.Map(), cancellationToken);

        [HttpPost(nameof(GetUsers))]
        public async Task<GetOrganizationUsersResponse> GetUsers([FromBody] GetOrganizationUsersRequest request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request.Map(), cancellationToken);
            return new GetOrganizationUsersResponse
            {
                Users = result
            };
        }
    }
}

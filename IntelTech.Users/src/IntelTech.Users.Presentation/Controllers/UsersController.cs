using System.Threading;
using System.Threading.Tasks;
using IntelTech.Users.Presentation.Contracts;
using IntelTech.Users.Presentation.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace IntelTech.Users.Presentation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public sealed class UsersController
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
            => _mediator = mediator;

        [HttpPost(nameof(CreateUser))]
        public Task CreateUser([FromBody] CreateUserRequest request, CancellationToken cancellationToken)
            => _mediator.Send(request.Map(), cancellationToken);
    }
}

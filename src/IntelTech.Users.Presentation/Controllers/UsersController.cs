using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using IntelTech.Users.Application.Commands;
using IntelTech.Users.Presentation.Contracts;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace IntelTech.Users.Presentation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public sealed class UsersController
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public UsersController(
            IMediator mediator,
            IMapper mapper
        )
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost(nameof(CreateUser))]
        public Task CreateUser([FromBody] CreateUserRequest request, CancellationToken cancellationToken)
        {
            var command = _mapper.Map<CreateUserCommand>(request);
            return _mediator.Send(command, cancellationToken);
        }
    }
}

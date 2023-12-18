using System.Threading.Tasks;
using FluentAssertions;
using FluentValidation;
using IntelTech.Common.Testing;
using IntelTech.Organizations.Application.Commands;
using IntelTech.Organizations.Infrastructure.DbContexts;
using IntelTech.Organizations.Presentation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace IntelTech.Organizations.UnitTests.Commands;

[Collection(TestApplicationCollection.Collection)]
public sealed class AddOrganizationUserTests : TestsBase
{
    public AddOrganizationUserTests(TestApplicationFixture<Program> fixture) : base(fixture)
    {
    }

    [Fact]
    public async Task WhenAddOrganizationUser_WithInvalidParameters_ThenException()
    {
        // Arrange
        var command = new AddOrganizationUserCommand();

        await using var host = CreateHost();
        using var scope = host.Services.CreateScope();

        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

        // Act / Assert
        await FluentActions
            .Awaiting(() => mediator.Send(command))
            .Should()
            .ThrowAsync<ValidationException>();
    }

    [Fact]
    public async Task WhenAddOrganizationUser_WithValidParameters_ThenSuccess()
    {
        // Arrange
        var organization = CreateOrganization();
        var user = CreateUser();

        await using var host = CreateHost();
        using var scope = host.Services.CreateScope();

        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        await dbContext.Organizations.AddAsync(organization);
        await dbContext.Users.AddAsync(user);
        await dbContext.SaveChangesAsync();

        var command = new AddOrganizationUserCommand
        {
            UserId = user.Id,
            OrganizationId = organization.Id
        };

        // Act
        await mediator.Send(command);

        // Assert
        var actualUser = await dbContext.Users
            .Include(u => u.Organization)
            .FirstOrDefaultAsync(u => u.Id == command.UserId);
        actualUser.Should().NotBeNull();
        actualUser!.OrganizationId.Should().Be(command.OrganizationId);
        actualUser.Organization.Should().NotBeNull();
    }
}

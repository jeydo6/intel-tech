using System.Net.Http.Json;
using System.Threading.Tasks;
using FluentAssertions;
using FluentValidation;
using IntelTech.Organizations.Application.Models;
using IntelTech.Organizations.Infrastructure.DbContexts;
using IntelTech.Organizations.Presentation.Contracts;
using IntelTech.Organizations.Presentation.Controllers;
using IntelTech.Organizations.UnitTests.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace IntelTech.Organizations.UnitTests.Controllers.Organizations;

[Collection(TestApplicationCollection.Collection)]
public sealed class AddOrganizationUserTests : TestBase
{
    private static readonly string _addUserBaseUrl = $"/{GetRoute<OrganizationsController>()}/{nameof(OrganizationsController.AddUser)}";
    private static readonly string _getUsersBaseUrl = $"/{GetRoute<OrganizationsController>()}/{nameof(OrganizationsController.GetUsers)}";

    public AddOrganizationUserTests(TestApplicationFixture fixture) : base(fixture)
    {
    }

    [Fact]
    public async Task WhenAddOrganizationUser_WithInvalidParameters_ThenException()
    {
        // Arrange
        var request = new AddOrganizationUserRequest();

        using var host = CreateHost();
        using var scope = host.Services.CreateScope();

        var client = host.CreateDefaultClient();

        // Act / Assert
        await FluentActions
            .Awaiting(() => client.PostAsync(_addUserBaseUrl, request))
            .Should()
            .ThrowAsync<ValidationException>();
    }

    [Fact]
    public async Task WhenAddOrganizationUser_WithValidParameters_ThenSuccess()
    {
        // Arrange
        var organization = CreateOrganization();
        var user = CreateUser();

        using var host = CreateHost();
        using var scope = host.Services.CreateScope();

        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        await dbContext.Organizations.AddAsync(organization);
        await dbContext.Users.AddAsync(user);
        await dbContext.SaveChangesAsync();

        var addUserRequest = new AddOrganizationUserRequest
        {
            UserId = user.Id,
            OrganizationId = organization.Id
        };
        var getUsersRequest = new GetOrganizationUsersRequest
        {
            OrganizationId = organization.Id,
            PaginationInfo = new PaginationInfo
            {
                PageNumber = 1,
                PageSize = 1
            }
        };

        var client = host.CreateDefaultClient();

        // Act
        await client.PostAsync(_addUserBaseUrl, addUserRequest);
        var getUsersResponse = await client.PostAsync(_getUsersBaseUrl, getUsersRequest);
        var getUsersResult = await getUsersResponse.Content.ReadFromJsonAsync<GetOrganizationUsersResponse>();

        // Assert
        getUsersResult.Should().NotBeNull();
        getUsersResult!.Users.Should().NotBeNull();
        getUsersResult!.Users.Should().HaveCount(1);
        getUsersResult!.Users.Should().AllSatisfy(actualUser => actualUser.OrganizationId.Should().Be(organization.Id));
    }
}

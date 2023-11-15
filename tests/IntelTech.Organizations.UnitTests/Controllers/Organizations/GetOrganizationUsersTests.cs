using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;
using AutoMapper;
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
public sealed class GetOrganizationUsersTests : TestBase
{
    private static readonly string _baseUrl = $"/{GetRoute<OrganizationsController>()}/{nameof(OrganizationsController.GetUsers)}";

    public GetOrganizationUsersTests(TestApplicationFixture fixture) : base(fixture)
    {
    }

    [Fact]
    public async Task WhenGetOrganizationUsers_WithInvalidParameters_ThenException()
    {
        // Arrange
        var request = new GetOrganizationUsersRequest
        {
            PaginationInfo = new PaginationInfo()
        };

        using var host = CreateHost();
        using var scope = host.Services.CreateScope();

        var client = host.CreateDefaultClient();

        // Act / Assert
        await FluentActions
            .Awaiting(() => client.PostAsync(_baseUrl, request))
            .Should()
            .ThrowAsync<ValidationException>();
    }

    [Fact]
    public async Task WhenGetOrganizationUsers_WithOrganizationId_ThenSuccess()
    {
        const int pageSize = 2;
        const int pageNumbersCount = 5;

        var organization = CreateOrganization();
        var users = CreateUsers(usersCount: pageSize * pageNumbersCount);

        foreach (var user in users)
        {
            organization.Users.Add(user);
        }

        var paginationInfos = Enumerable
            .Range(0, pageNumbersCount)
            .Select(i => new PaginationInfo
            {
                PageNumber = i + 1,
                PageSize = pageSize
            });

        using var host = CreateHost();
        using var scope = host.Services.CreateScope();

        var mapper = scope.ServiceProvider.GetRequiredService<IMapper>();
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        await dbContext.Organizations.AddAsync(organization);
        await dbContext.Users.AddRangeAsync(users);
        await dbContext.SaveChangesAsync();

        var requests = paginationInfos
            .Select(pi => new GetOrganizationUsersRequest
            {
                OrganizationId = organization.Id,
                PaginationInfo = pi
            });

        var client = host.CreateDefaultClient();

        // Act
        var results = new List<User[]>();
        foreach (var request in requests)
        {
            var response = await client.PostAsync(_baseUrl, request);
            var result = await response.Content.ReadFromJsonAsync<GetOrganizationUsersResponse>();
            if (result != null)
            {
                results.Add(result.Users);
            }
        }

        // Assert
        results.Should().HaveCount(pageNumbersCount);
        for (var i = 0; i < pageNumbersCount; i++)
        {
            var actualUsers = results[i];
            var expectedUsers = mapper.Map<User[]>(users[(i * pageSize)..((i + 1) * pageSize)]);
            actualUsers.Should().BeEquivalentTo(expectedUsers);
        }
    }

    [Fact]
    public async Task WhenGetOrganizationUsers_WithoutOrganizationId_ThenSuccess()
    {
        const int pageSize = 2;
        const int pageNumbersCount = 5;

        var users = CreateUsers(usersCount: pageSize * pageNumbersCount);

        var paginationInfos = Enumerable
            .Range(0, pageNumbersCount)
            .Select(i => new PaginationInfo
            {
                PageNumber = i + 1,
                PageSize = pageSize
            });

        using var host = CreateHost();
        using var scope = host.Services.CreateScope();

        var mapper = scope.ServiceProvider.GetRequiredService<IMapper>();
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        await dbContext.Users.AddRangeAsync(users);
        await dbContext.SaveChangesAsync();

        var requests = paginationInfos
            .Select(pi => new GetOrganizationUsersRequest
            {
                OrganizationId = default,
                PaginationInfo = pi
            });

        var client = host.CreateDefaultClient();

        // Act
        var results = new List<User[]>();
        foreach (var request in requests)
        {
            var response = await client.PostAsync(_baseUrl, request);
            var result = await response.Content.ReadFromJsonAsync<GetOrganizationUsersResponse>();
            if (result != null)
            {
                results.Add(result.Users);
            }
        }

        // Assert
        results.Should().NotBeEmpty();
    }

    [Fact]
    public async Task WhenGetOrganizationUsers_WithGreaterPageNumber_ThenSuccess()
    {
        const int pageSize = 2;
        const int pageNumbersCount = 5;

        var organization = CreateOrganization();
        var users = CreateUsers(usersCount: pageSize * pageNumbersCount);

        var paginationInfo = new PaginationInfo
        {
            PageNumber = pageNumbersCount + 1,
            PageSize = pageSize
        };

        using var host = CreateHost();
        using var scope = host.Services.CreateScope();

        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        await dbContext.Organizations.AddAsync(organization);
        await dbContext.Users.AddRangeAsync(users);
        await dbContext.SaveChangesAsync();

        var request = new GetOrganizationUsersRequest
        {
            OrganizationId = organization.Id,
            PaginationInfo = paginationInfo
        };

        var client = host.CreateDefaultClient();

        // Act
        var response = await client.PostAsync(_baseUrl, request);
        var result = await response.Content.ReadFromJsonAsync<GetOrganizationUsersResponse>();

        // Assert
        result.Should().NotBeNull();
        result!.Users.Should().BeEmpty();
    }
}

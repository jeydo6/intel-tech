using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using FluentValidation;
using IntelTech.Common.Testing;
using IntelTech.Organizations.Application.Extensions;
using IntelTech.Organizations.Application.Models;
using IntelTech.Organizations.Application.Queries;
using IntelTech.Organizations.Infrastructure.DbContexts;
using IntelTech.Organizations.Presentation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace IntelTech.Organizations.UnitTests.Commands;

[Collection(TestApplicationCollection.Collection)]
public sealed class GetOrganizationUsersTests : TestsBase
{
    public GetOrganizationUsersTests(TestApplicationFixture<Program> fixture) : base(fixture)
    {
    }

    [Fact]
    public async Task WhenGetOrganizationUsers_WithInvalidParameters_ThenException()
    {
        // Arrange
        var command = new GetOrganizationUsersQuery
        {
            PaginationInfo = new PaginationInfo()
        };

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

        await using var host = CreateHost();
        using var scope = host.Services.CreateScope();

        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        await dbContext.Organizations.AddAsync(organization);
        await dbContext.Users.AddRangeAsync(users);
        await dbContext.SaveChangesAsync();

        var queries = paginationInfos
            .Select(pi => new GetOrganizationUsersQuery
            {
                OrganizationId = organization.Id,
                PaginationInfo = pi
            });

        // Act
        var results = new List<User[]>();
        foreach (var query in queries)
        {
            var result = await mediator.Send(query);
            results.Add(result);
        }

        // Assert
        results.Should().HaveCount(pageNumbersCount);
        for (var i = 0; i < pageNumbersCount; i++)
        {
            var actualUsers = results[i];
            var expectedUsers = users[(i * pageSize)..((i + 1) * pageSize)]
                .Select(MappingExtension.Map)
                .ToArray();
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

        await using var host = CreateHost();
        using var scope = host.Services.CreateScope();

        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        await dbContext.Users.AddRangeAsync(users);
        await dbContext.SaveChangesAsync();

        var queries = paginationInfos
            .Select(pi => new GetOrganizationUsersQuery
            {
                OrganizationId = default,
                PaginationInfo = pi
            });

        // Act
        var results = new List<User[]>();
        foreach (var query in queries)
        {
            var result = await mediator.Send(query);
            results.Add(result);
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

        await using var host = CreateHost();
        using var scope = host.Services.CreateScope();

        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        await dbContext.Organizations.AddAsync(organization);
        await dbContext.Users.AddRangeAsync(users);
        await dbContext.SaveChangesAsync();

        var query = new GetOrganizationUsersQuery
        {
            OrganizationId = organization.Id,
            PaginationInfo = paginationInfo
        };

        // Act
        var result = await mediator.Send(query);

        // Assert
        result.Should().BeEmpty();
    }
}

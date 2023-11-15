using System;
using System.Linq;
using Bogus;
using Bogus.DataSets;
using IntelTech.Organizations.Domain.Entities;
using IntelTech.Organizations.Presentation;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace IntelTech.Organizations.UnitTests;

public abstract class BaseTests : IDisposable
{
    protected readonly Faker Faker = new Faker("ru");

    private readonly TestApplicationFixture _fixture;

    public BaseTests(TestApplicationFixture fixture)
        => _fixture = fixture;

    protected WebApplicationFactory<Program> CreateHost() => CreateHost(_ => { });

    protected WebApplicationFactory<Program> CreateHost(Action<IServiceCollection> dependencyOverrides)
        => _fixture.CreateHost(dependencyOverrides);

    public void Dispose() => _fixture.Dispose();

    protected Organization CreateOrganization()
        => new Organization
        {
            Id = default,
            Name = Faker.Company.CompanyName()
        };

    protected User CreateUser(int? organizationId = default)
    {
        var gender = Faker.PickRandom<Name.Gender>();
        var firstName = Faker.Name.FirstName(gender);
        var lastName = Faker.Name.LastName(gender);
        return new User
        {
            Id = default,
            FirstName = firstName,
            LastName = lastName,
            PhoneNumber = Faker.Phone.PhoneNumber(),
            Email = Faker.Internet.Email(firstName, lastName),
            OrganizationId = organizationId
        };
    }

    protected User[] CreateUsers(int? organizationId = default, int usersCount = 10)
        => Enumerable.Range(0, usersCount)
            .Select(_ => CreateUser(organizationId))
            .ToArray();
}

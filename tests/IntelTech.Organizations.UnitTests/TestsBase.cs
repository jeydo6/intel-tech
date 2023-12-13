using System.Linq;
using Bogus;
using Bogus.DataSets;
using IntelTech.Organizations.Domain.Entities;
using IntelTech.Organizations.Presentation;

namespace IntelTech.Organizations.UnitTests;

public abstract class TestsBase : Common.Testing.TestsBase<Program>
{
    public TestsBase(Common.Testing.TestApplicationFixture<Program> fixture) : base(fixture)
    {
    }

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

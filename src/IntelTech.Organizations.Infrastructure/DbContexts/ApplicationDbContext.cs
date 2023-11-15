using System.IO;
using System.Linq;
using Bogus;
using Bogus.DataSets;
using IntelTech.Organizations.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace IntelTech.Organizations.Infrastructure.DbContexts;

internal sealed class ApplicationDbContext : DbContext
{
    private readonly IConfiguration _configuration;

    public ApplicationDbContext(IConfiguration configuration)
        => _configuration = configuration;

    public DbSet<User> Users { get; set; }
    public DbSet<Organization> Organizations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasKey(u => u.Id);
        modelBuilder.Entity<Organization>()
            .HasKey(o => o.Id);

        modelBuilder.Entity<User>()
            .HasOne<Organization>(u => u.Organization)
            .WithMany(o => o.Users)
            .HasForeignKey(u => u.OrganizationId);

        var faker = new Faker("ru");

        var organizationsCount = 5;
        modelBuilder.Entity<Organization>()
            .HasData(faker.CreateOrganizations(organizationsCount));

        var usersCount = 10;
        modelBuilder.Entity<User>()
            .HasData(faker.CreateUsers(organizationsCount, usersCount));
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseNpgsql(_configuration.GetConnectionString("DefaultConnection"));
}

internal class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

        return new ApplicationDbContext(configuration);
    }
}

internal static class FakerExtension
{
    public static Organization[] CreateOrganizations(this Faker faker, int organizationsCount = 5)
        => Enumerable
            .Range(0, organizationsCount)
            .Select(i => new Organization
            {
                Id = i + 1,
                Name = faker.Company.CompanyName()
            })
            .ToArray();

    public static User[] CreateUsers(this Faker faker, int organizationsCount, int usersCount = 10)
        => Enumerable
            .Range(0, usersCount)
            .Select(i =>
            {
                var gender = faker.PickRandom<Name.Gender>();
                var firstName = faker.Name.FirstName(gender);
                var lastName = faker.Name.LastName(gender);
                return new User
                {
                    Id = i + 1,
                    FirstName = firstName,
                    LastName = lastName,
                    PhoneNumber = faker.Phone.PhoneNumber(),
                    Email = faker.Internet.Email(firstName, lastName),
                    OrganizationId = faker.Random.Int(1, organizationsCount)
                };
            })
            .ToArray();
}

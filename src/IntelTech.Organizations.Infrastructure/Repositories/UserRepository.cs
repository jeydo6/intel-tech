using System.Threading;
using System.Threading.Tasks;
using IntelTech.Organizations.Domain.Entities;
using IntelTech.Organizations.Domain.Repositories;
using IntelTech.Organizations.Infrastructure.DbContexts;

namespace IntelTech.Organizations.Infrastructure.Repositories
{
    public sealed class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public UserRepository(ApplicationDbContext dbContext)
            => _dbContext = dbContext;

        public async Task<int> Create(User entity, CancellationToken cancellationToken = default)
        {
            await _dbContext.Users.AddAsync(entity, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}

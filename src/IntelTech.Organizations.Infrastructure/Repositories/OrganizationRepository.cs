using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using IntelTech.Organizations.Domain.Entities;
using IntelTech.Organizations.Domain.Models;
using IntelTech.Organizations.Domain.Repositories;
using IntelTech.Organizations.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace IntelTech.Organizations.Infrastructure.Repositories
{
    public sealed class OrganizationRepository : IOrganizationRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<OrganizationRepository> _logger;

        public OrganizationRepository(
            ApplicationDbContext dbContext,
            ILogger<OrganizationRepository> logger
        )
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task AddUser(int organizationId, int userId, CancellationToken cancellationToken = default)
        {
            try
            {
                var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId, cancellationToken: cancellationToken) ??
                    throw new ApplicationException($"Не удалось найти пользователя с идентификатором '{organizationId}'");

                user.OrganizationId = organizationId;
                await _dbContext.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "Не удалось добавить пользователя с идентификатором '{UserId}' к организации с идентификатором '{OrganizationId}'",
                    userId, organizationId
                );
            }
        }

        public Task<User[]> GetUsers(int? organizationId, PaginationInfo paginationInfo, CancellationToken cancellationToken = default)
            => _dbContext.Users
                .Where(u => u.OrganizationId == organizationId)
                .OrderBy(u => u.Id)
                .Skip(paginationInfo.Offset)
                .Take(paginationInfo.Limit)
                .AsNoTracking()
                .ToArrayAsync(cancellationToken);
    }
}

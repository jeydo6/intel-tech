using System.Threading;
using System.Threading.Tasks;
using IntelTech.Organizations.Domain.Entities;
using IntelTech.Organizations.Domain.Models;

namespace IntelTech.Organizations.Domain.Repositories;

public interface IOrganizationRepository
{
    Task AddUser(int organizationId, int userId, CancellationToken cancellationToken = default);
    Task<User[]> GetUsers(int? organizationId, PaginationInfo paginationInfo, CancellationToken cancellationToken = default);
}

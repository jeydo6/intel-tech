using System.Threading;
using System.Threading.Tasks;
using IntelTech.Organizations.Domain.Entities;

namespace IntelTech.Organizations.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<int> Create(User entity, CancellationToken cancellationToken = default);
    }
}

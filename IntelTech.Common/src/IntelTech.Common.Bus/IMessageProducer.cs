using System.Threading;
using System.Threading.Tasks;

namespace IntelTech.Common.Bus;

public interface IMessageProducer
{
    Task Produce<TMessage>(TMessage message, CancellationToken cancellationToken = default) where TMessage : notnull;
}

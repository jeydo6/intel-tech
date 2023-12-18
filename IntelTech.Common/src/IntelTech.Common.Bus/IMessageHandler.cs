using System.Threading;
using System.Threading.Tasks;

namespace IntelTech.Common.Bus;

public interface IMessageHandler<in TMessage> : IMessageHandler
    where TMessage: notnull
{
    Task Handle(TMessage message, CancellationToken cancellationToken = default);
}

public interface IMessageHandler;
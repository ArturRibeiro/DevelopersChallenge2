using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Nibo.Prova.Asp.Web.Presentation.Application.EventHandlers.Transactions.Events;

namespace Nibo.Prova.Asp.Web.Presentation.Application.EventHandlers.Transactions
{
    public class TransactionEventHandler
        : INotificationHandler<DeleteAllFilesEvent>
    {
        public async Task Handle(DeleteAllFilesEvent message, CancellationToken cancellationToken)
        {
            //Delete all files
        }
    }
}
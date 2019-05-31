using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Nibo.Prova.Asp.Web.Presentation.Application.CommandHandlers.Transactions.Commands;
using Nibo.Prova.Asp.Web.Presentation.Application.EventHandlers.Transactions.Events;
using Nibo.Prova.Asp.Web.Presentation.Application.Helpers;
using Nibo.Prova.Asp.Web.Presentation.Application.Notifications;
using Nibo.Prova.Domain.Models.Transactions;
using Nibo.Prova.Domain.Models.Transactions.Repository;

namespace Nibo.Prova.Asp.Web.Presentation.Application.CommandHandlers.Transactions
{
    public class TransactionCommandHandler
        : IRequestHandler<PrecessFileOFXCommand, bool>
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IMediator _mediator;
        private readonly DomainNotificationHandler _notificationHandler;

        public TransactionCommandHandler(ITransactionRepository transactionRepository, IMediator mediator, INotificationHandler<DomainNotification> notificationHandler)
        {
            _transactionRepository = transactionRepository;
            _mediator = mediator;
            _notificationHandler = (DomainNotificationHandler)notificationHandler;
        }

        /// <summary>
        /// Process all client transactions
        /// </summary>
        /// <param name="message">Message assigned to the command handler</param>
        /// <param name="cancellationToken">Spreads the notification that transactions should be canceled.</param>
        /// <returns></returns>
        public async Task<bool> Handle(PrecessFileOFXCommand message, CancellationToken cancellationToken)
        {
            if ( await ProcessFiles(message.Files, cancellationToken))
                await _mediator.Publish(DeleteAllFilesEvent.Factory.Create(message.Files));

            return !_notificationHandler.HasNotifications;
        }

        #region Private Method's
        /// <summary>
        /// Process the Files
        /// </summary>
        /// <param name="files">Files ofx</param>
        /// <param name="cancellationToken">Spreads the notification that transactions should be canceled.</param>
        /// <returns></returns>
        private async Task<bool> ProcessFiles(string[] files, CancellationToken cancellationToken)
        {
            try
            {
                var transactions = GetTransactions(files);

                _transactionRepository.Add(transactions);

                await _transactionRepository.UnitOfWork
                    .SaveEntitiesAsync(cancellationToken);

                await _mediator.Publish(DomainNotification.Factory.Create("There was an error persisting the files"));
            }
            catch(Exception ex)
            {
                await _mediator.Publish(DomainNotification.Factory.Create("There was an error persisting the files"));
            }

            return !_notificationHandler.HasNotifications;
        }

        /// <summary>
        /// Parse TransactionDto to class Transaction
        /// </summary>
        /// <param name="files">List the Files ofx</param>
        /// <returns></returns>
        private static IEnumerable<Transaction> GetTransactions(IEnumerable<string> files)
            => from file in files
               select new OfxDocument(file)
                    into document
               from t in document.Transactions
               select Transaction.Factory.Create(datePosted: t.DatePosted
               , transAmount: t.TransAmount
               , memo: t.Memo
               , transactionType: (TransactionType)t.TransType); 
        #endregion
    }
}

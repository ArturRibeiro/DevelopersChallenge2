using System;
using System.Collections.Generic;
using System.Linq;
using Nibo.Prova.Domain.Models.Transactions;
using Nibo.Prova.Domain.Models.Transactions.Repository;
using Nibo.Prova.Domain.SeedWork;

namespace Nibo.Prova.Infrastructure.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly NiboDbContext _context;
        public IUnitOfWork UnitOfWork => _context;

        public TransactionRepository(NiboDbContext context)
            => _context = context ?? throw new ArgumentNullException(nameof(context));

        public void Add(IEnumerable<Transaction> transactions)
            => transactions
                    .ToList()
                    .ForEach(transaction => _context.Transactions.Add(transaction));
    }
}

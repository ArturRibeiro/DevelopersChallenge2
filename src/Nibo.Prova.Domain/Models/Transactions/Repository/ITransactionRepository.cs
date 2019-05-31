using System.Collections.Generic;
using Nibo.Prova.Domain.SeedWork;

namespace Nibo.Prova.Domain.Models.Transactions.Repository
{
    public interface ITransactionRepository : IRepository<Transaction>
    {
        void Add(IEnumerable<Transaction> transactions);
    }
}

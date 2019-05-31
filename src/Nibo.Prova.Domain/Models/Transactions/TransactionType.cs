using Nibo.Prova.Domain.SeedWork;
using System.Collections.Generic;

namespace Nibo.Prova.Domain.Models.Transactions
{
    public enum TransactionType : short
    {
        Undefined = 0,
        Debit = 1,
        Credit = 2
    }
}

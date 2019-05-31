using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nibo.Prova.Asp.Web.Presentation.Application.Queries.Transactions
{
    public interface ITransactionQueries
    {
        /// <summary>
        /// Gets the history of transactions
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<HistoricalTransactionViewModel>> GetTransactionHistory();
    }
}

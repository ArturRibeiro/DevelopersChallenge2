using System.Collections.Generic;
using Dapper;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Nibo.Prova.Asp.Web.Presentation.Application.Queries.Transactions
{
    public class TransactionQueries : ITransactionQueries
    {
        private readonly IConnectionStringProvider _connectionStringProvider;

        public TransactionQueries(IConnectionStringProvider connectionStringProvider)
            => _connectionStringProvider = connectionStringProvider;

        /// <summary>
        /// Gets the history of transactions
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<HistoricalTransactionViewModel>> GetTransactionHistory()
        {
            using (var connection = new SqlConnection(_connectionStringProvider.ConnectionString))
            {
                connection.Open();
                return await connection.QueryAsync<HistoricalTransactionViewModel>
                (
                    sql: @"SELECT DISTINCT
                                  Id
                                  , DatePosted
                                  , Amount
                                  , Memo
                                  , [Type]
                           FROM [dbo].[transactions] Order By 1"
                );
            }
        }
    }
}

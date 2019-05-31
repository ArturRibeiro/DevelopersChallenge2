using Nibo.Prova.Domain.Models.Transactions;

namespace Nibo.Prova.Asp.Web.Presentation.Application.Queries.Transactions
{
    public class HistoricalTransactionViewModel
    {
        public int Id { get; set; }

        public string DatePosted { get; set; }

        public string Amount { get; set; }

        public string Memo { get; set; }

        public short Type { get; set; }

        public string TypeDescription => ((TransactionType)Type).ToString();
    
    }
}

using Nibo.Prova.Domain.Models.Transactions;

namespace Nibo.Prova.Asp.Web.Presentation.Application.Queries.Transactions
{
    public class HistoricalTransactionViewModel
    {
        public string DatePosted { get; set; }

        public string TransAmount { get; set; }

        public string Memo { get; set; }

        public short Type { get; set; }

        public string TypeDescription => ((TransactionType)Type).ToString();
    
    }
}

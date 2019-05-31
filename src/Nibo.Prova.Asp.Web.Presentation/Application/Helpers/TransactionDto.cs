using System;

namespace Nibo.Prova.Asp.Web.Presentation.Application.Helpers
{
    /// <summary>
    /// Transaction is a struct that holds a single transaction
    /// </summary>
    public struct TransactionDto
    {
        public short TransType;
        public DateTime DatePosted;
        public string TransAmount;
        public string Memo;
    }
}

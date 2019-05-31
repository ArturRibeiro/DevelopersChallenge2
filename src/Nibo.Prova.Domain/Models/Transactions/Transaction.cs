using Nibo.Prova.Domain.SeedWork;
using System;
using System.Collections.Generic;

namespace Nibo.Prova.Domain.Models.Transactions
{
    public class Transaction : Entity<int>, IAggregateRoot
    {
        #region Constructors

        //TODO: Constructor necessary for EF 
        public Transaction()
        {

        }

        internal Transaction(DateTime datePosted, string transAmount, string memo, TransactionType transactionType)
        {
            if (datePosted == DateTime.MinValue) throw new ArgumentNullException(nameof(datePosted));
            if (datePosted == DateTime.MaxValue) throw new ArgumentNullException(nameof(datePosted));
            if (transAmount == null) throw new ArgumentNullException(nameof(transAmount));
            if (memo == null) throw new ArgumentNullException(nameof(memo));
            //if (transactionType == TransactionType.Undefined) throw new ArgumentNullException(nameof(transactionType));

            this.Id = 0;
            this.DatePosted = datePosted;
            this.TransAmount = transAmount;
            this.Memo = memo;
            this.TransactionType = transactionType;
        } 
        #endregion

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        public DateTime DatePosted { get; private set; }


        /// <summary>
        /// 
        /// </summary>
        public string TransAmount { get; private set; }


        /// <summary>
        /// 
        /// </summary>
        public string Memo { get; private set; }

        public TransactionType TransactionType { get; private set; }
        #endregion

        #region Factory
        public static class Factory
        {
            public static Transaction Create(DateTime datePosted, string transAmount, string memo, TransactionType transactionType)
                => new Transaction(datePosted, transAmount, memo, transactionType);
        }
        #endregion

        public void UpdateMemo(string memo)
        {
            this.Memo = memo;
        }
    }
}

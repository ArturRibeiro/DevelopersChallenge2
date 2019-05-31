using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace Nibo.Prova.Asp.Web.Presentation.Application.Helpers
{
    /// <summary>
    /// OfxDocument Holds the current document pulled in from a stream in the constructor
    /// </summary>
    public class OfxDocument
    {
        private const RegexOptions _regexOptions = RegexOptions.Multiline | RegexOptions.IgnoreCase;

        public List<TransactionDto> Transactions;

        public OfxDocument(string fileSource)
        {
            using (FileStream stream = File.OpenRead(fileSource))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    Transactions = new List<TransactionDto>();
                    bool inHeader = true;
                    while (!reader.EndOfStream)
                    {
                        string temp = reader.ReadLine();

                        inHeader = temp.ToLower().Contains("<ofx>");

                        if (!inHeader)
                        {
                            var restOfFile = temp + reader.ReadToEnd();
                            restOfFile = Regex.Replace(restOfFile, Environment.NewLine, string.Empty);
                            restOfFile = Regex.Replace(restOfFile, "\n", string.Empty);
                            restOfFile = Regex.Replace(restOfFile, "\t", string.Empty);

                            var banktranlist = Regex.Match(restOfFile, @"(?<=<banktranlist>).+(?=<\/banktranlist>)", _regexOptions).Value;
                            var matches = Regex.Matches(banktranlist, @"<stmttrn>.+?<\/stmttrn>", _regexOptions);

                            foreach (Match match in matches)
                            {
                                foreach (Capture capture in match.Captures)
                                {
                                    Transactions.Add(new TransactionDto()
                                    {
                                        TransType = CreateTransactionType(capture),
                                        DatePosted = CreateDatePostedTransaction(capture),
                                        TransAmount = CreateTransactionAmount(capture),
                                        Memo = CreateMemoTransaction(capture)
                                    });
                                }
                            }
                        }
                    }
                }
            }
        }

        #region Privates Methods

        private static short CreateTransactionType(Capture capture)
        {
            var transactionTypeCredit = Regex.Match(capture.Value, @"(?<=<trntype>).+?(?=<)", _regexOptions).Value
                .ToLower();

            var transactionTypeDebit = Regex.Match(capture.Value, @"(?<=<trntype>).+?(?=<)", _regexOptions).Value
                .ToLower();

            if (transactionTypeCredit.Equals("debit")) return 1;
            if (transactionTypeCredit.Equals("credit")) return 2;

            return 0;
        }

        private static DateTime CreateDatePostedTransaction(Capture capture)
        {
            var date = Regex.Match(capture.Value, @"(?<=<dtposted>).+?(?=<)", _regexOptions).Value;

            return date.Replace("[-03:EST]", string.Empty).ToDateTime("yyyyMMddHHmmss");
        }

        private static string CreateTransactionAmount(Capture capture)
            => Regex.Match(capture.Value, @"(?<=<trnamt>).+?(?=<)", _regexOptions).Value;

        private static string CreateMemoTransaction(Capture capture)
            => Regex.Match(capture.Value, @"(?<=<memo>).+?(?=<)", _regexOptions).Value;
        #endregion
    }
}

using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nibo.Prova.Asp.Web.Presentation.Application.Queries.Transactions;

namespace Nibo.Prova.Asp.Web.Presentation.Controllers
{
    public class TransactionsController : Controller
    {
        private readonly ITransactionQueries _transactionQueries;

        public TransactionsController(ITransactionQueries transactionQueries)
        {
            _transactionQueries = transactionQueries;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _transactionQueries.GetTransactionHistory());
        }
    }
}
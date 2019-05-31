using MediatR;

namespace Nibo.Prova.Asp.Web.Presentation.Application.CommandHandlers.Transactions.Commands
{
    public class PrecessFileOfxCommand : IRequest<bool>
    {
        /// <summary>
        /// Files for processing
        /// </summary>
        public string[] Files { get; private set; }

        #region Factory
        public static class Factory
        {
            public static PrecessFileOfxCommand Create(string[] files)
                => new PrecessFileOfxCommand()
                {
                    Files = files
                };
        }
        #endregion
    }
}
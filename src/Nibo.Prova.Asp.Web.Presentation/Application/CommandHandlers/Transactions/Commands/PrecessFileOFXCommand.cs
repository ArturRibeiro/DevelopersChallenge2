using MediatR;

namespace Nibo.Prova.Asp.Web.Presentation.Application.CommandHandlers.Transactions.Commands
{
    public class PrecessFileOFXCommand : IRequest<bool>
    {
        /// <summary>
        /// Files for processing
        /// </summary>
        public string[] Files { get; private set; }

        #region Factory
        public static class Factory
        {
            public static PrecessFileOFXCommand Create(string[] files)
                => new PrecessFileOFXCommand()
                {
                    Files = files
                };
        }
        #endregion
    }
}
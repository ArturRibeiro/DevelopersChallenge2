using MediatR;

namespace Nibo.Prova.Asp.Web.Presentation.Application.EventHandlers.Transactions.Events
{
    public class DeleteAllFilesEvent : IRequest, INotification
    {
        /// <summary>
        /// Files for processing
        /// </summary>
        public string[] Files { get; private set; }

        #region Factory
        public static class Factory
        {
            public static DeleteAllFilesEvent Create(string[] files)
                => new DeleteAllFilesEvent()
                {
                    Files = files
                };
        }
        #endregion
    }
}

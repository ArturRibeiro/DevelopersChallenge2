using MediatR;

namespace Nibo.Prova.Asp.Web.Presentation.Application.Notifications
{
    public class DomainNotification : INotification
    {
        public string Message { get; private set; }

        #region Factory

        public static class Factory
        {
            public static DomainNotification Create(string message)
                => new DomainNotification()
                {
                    Message = message
                };
        }
        
        #endregion
    }
}

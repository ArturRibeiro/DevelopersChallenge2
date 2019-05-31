namespace Nibo.Prova.Asp.Web.Presentation.Application.Queries
{
    public interface IConnectionStringProvider
    {
        /// <summary>
        /// Connecting the database access provider
        /// </summary>
        string ConnectionString { get; }
    }
}

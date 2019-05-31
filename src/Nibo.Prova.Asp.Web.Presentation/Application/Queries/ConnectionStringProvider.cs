namespace Nibo.Prova.Asp.Web.Presentation.Application.Queries
{
    public class ConnectionStringProvider : IConnectionStringProvider
    {
        private readonly string _connectionString;

        /// <summary>
        /// Connecting the database access provider
        /// </summary>
        public string ConnectionString => _connectionString;

        public ConnectionStringProvider(string connectionString)
            => _connectionString = connectionString;
    }
}

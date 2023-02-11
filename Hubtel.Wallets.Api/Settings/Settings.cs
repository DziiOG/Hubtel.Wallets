namespace Hubtel.Wallets.Api.Settings
{
    /// <summary>
    ///  Class contains mongo db settings
    /// </summary>
    public class MongoDbSettings
    {
        /// <summary>
        ///  gets and sets the host
        /// </summary>
        public string Host { get; set; } = string.Empty;

        /// <summary>
        ///  gets and sets the port
        /// </summary>
        public string Port { get; set; } = string.Empty;

        /// <summary>
        ///  gets and sets the database Name
        /// </summary>
        public string DatabaseName { get; set; } = string.Empty;

        /// <summary>
        ///  Get: Field generates a string from the Host and Port as a connection string
        /// </summary>
        public string ConnectionString
        {
            get { return $"mongodb://{Host}:{Port}"; }
        }
    }
}

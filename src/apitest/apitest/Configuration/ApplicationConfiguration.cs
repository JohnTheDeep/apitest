using apitest.Interfaces;

namespace apitest.Configuration
{
    public class ApplicationConfiguration : IApplicationConfiguration
    {
        public string DEFAULT_CONNECTION_STRING { get; set; }
    }
}

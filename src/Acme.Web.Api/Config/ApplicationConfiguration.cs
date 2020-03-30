namespace Acme.Web.Api.Config
{
    public class ApplicationConfiguration : IApplicationConfiguration
    {
        public string ReadOnlyString { get; set; }
        public string ReadWriteConnectionString { get; set; }
    }
}
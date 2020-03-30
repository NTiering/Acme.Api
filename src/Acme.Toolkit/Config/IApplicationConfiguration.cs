namespace Acme.Web.Api.Config
{
    public interface IApplicationConfiguration
    {
        string ReadOnlyString { get; }
        string ReadWriteConnectionString { get; }
    }
}
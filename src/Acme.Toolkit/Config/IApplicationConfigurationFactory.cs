namespace Acme.Web.Api.Config
{
    public interface IApplicationConfigurationFactory
    {
        ApplicationConfiguration Config { get; }
    }
}
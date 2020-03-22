using System.Security.Principal;

namespace Acme.Toolkit.Extensions
{
    public static class IIdentityExt
    {
        private const string UnknownUser = "Unknown User";

        public static string LogDescriptorText(this IIdentity identity)
        {
            var name = identity == null ? UnknownUser : string.IsNullOrEmpty(identity.Name) ? UnknownUser : identity.Name;
            return name;
        }
    }
}
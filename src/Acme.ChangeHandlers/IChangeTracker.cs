using System.Security.Principal;
using System.Threading.Tasks;

namespace Acme.ChangeHandlers
{
    public interface IChangeTracker
    {
        Task<int> BroadcastAdd(object newState, IIdentity identity);

        Task<int> BroadcastDelete(object oldState, IIdentity identity);

        Task<int> BroadcastModify(object oldState, object newState, IIdentity identity);
    }
}
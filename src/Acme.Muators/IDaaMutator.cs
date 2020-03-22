using System.Security.Principal;
using System.Threading.Tasks;

namespace Acme.Muators
{
    public interface IDataMutator
    {
        Task BeforeAdd(object newState, IIdentity identity);

        Task BeforeModify(object oldState, object newState, IIdentity identity);

        Task BeforeRemove(object oldState, IIdentity identity);
    }
}
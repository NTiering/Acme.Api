using System;
using System.Security.Principal;

namespace Acme.ChangeHandlers
{
    public interface IChangeEventHandler
    {
        bool CanHandle(Type type);

        void OnChange(object oldState, object newState, IIdentity identity);
    }
}
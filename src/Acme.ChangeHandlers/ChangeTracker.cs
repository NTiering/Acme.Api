using Acme.Toolkit.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;

namespace Acme.ChangeHandlers
{
    public class ChangeTracker : IChangeTracker
    {
        private readonly IEnumerable<IChangeEventHandler> _eventHandlers;

        public ChangeTracker(IEnumerable<IChangeEventHandler> eventHandlers)
        {
            eventHandlers.ThrowIfNull(nameof(eventHandlers));

            _eventHandlers = eventHandlers;
        }

        public async Task<int> BroadcastAdd(object newState, IIdentity identity)
        {
            newState.ThrowIfNull(nameof(newState));

            var handlers = GetHandlers(newState.GetType());
            await ExecuteHandlers(null, newState, identity, handlers);
            return handlers.Count();
        }

        public async Task<int> BroadcastDelete(object oldState, IIdentity identity)
        {
            oldState.ThrowIfNull(nameof(oldState));

            var handlers = GetHandlers(oldState.GetType());
            await ExecuteHandlers(oldState, null, identity, handlers);
            return handlers.Count();
        }

        public async Task<int> BroadcastModify(object oldState, object newState, IIdentity identity)
        {
            oldState.ThrowIfNull(nameof(oldState));
            newState.ThrowIfNull(nameof(newState));

            var handlers = GetHandlers(oldState.GetType());
            await ExecuteHandlers(oldState, newState, identity, handlers);
            return handlers.Count();
        }

        private async Task ExecuteHandlers<T>(T oldState, T newState, IIdentity identity, IEnumerable<IChangeEventHandler> handlers)
        where T : class
        {
            var tasks = handlers.Select(handler => Task.Run(() =>
            {
                handler.OnChange(oldState, newState, identity);
            })).ToArray();
            await Task.WhenAll(tasks);
        }

        private IEnumerable<IChangeEventHandler> GetHandlers(System.Type type)
        {
            return _eventHandlers.Where(x => x.CanHandle(type));
        }
    }
}
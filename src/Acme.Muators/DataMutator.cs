using Acme.Toolkit.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;

namespace Acme.Muators
{
    public class DataMutator : IDataMutator
    {
        private readonly IEnumerable<IDataMutatorHandler> _dataMutators;

        public DataMutator(IEnumerable<IDataMutatorHandler> dataMutators)
        {
            dataMutators.ThrowIfNull(nameof(dataMutators));
            _dataMutators = dataMutators;
        }

        public async Task BeforeAdd(object newState, IIdentity identity)
        {
            newState.ThrowIfNull(nameof(newState));

            var context = new DataMutatorContext { Action = DataActions.Add, DataSubjectType = newState.GetType(), Identity = identity, RequestedOn = DateTime.Now };
            var handlers = GetHandlers(context);

            var tasks = handlers.Select(handler => Task.Run(() =>
            {
                handler.BeforeAdd(newState, context);
            })).ToArray();
            await Task.WhenAll(tasks);
        }

        public async Task BeforeModify(object oldState, object newState, IIdentity identity)
        {
            oldState.ThrowIfNull(nameof(oldState));
            newState.ThrowIfNull(nameof(newState));

            var context = new DataMutatorContext { Action = DataActions.Modify, DataSubjectType = newState.GetType(), Identity = identity, RequestedOn = DateTime.Now };
            var handlers = GetHandlers(context);

            var tasks = handlers.Select(handler => Task.Run(() =>
            {
                handler.BeforeModify(oldState, newState, context);
            })).ToArray();
            await Task.WhenAll(tasks);
        }

        public async Task BeforeRemove(object oldState, IIdentity identity)
        {
            oldState.ThrowIfNull(nameof(oldState));

            var context = new DataMutatorContext { Action = DataActions.Modify, DataSubjectType = oldState.GetType(), Identity = identity, RequestedOn = DateTime.Now };
            var handlers = GetHandlers(context);

            var tasks = handlers.Select(handler => Task.Run(() =>
            {
                handler.BeforeRemove(oldState, context);
            })).ToArray();
            await Task.WhenAll(tasks);
        }

        private IEnumerable<IDataMutatorHandler> GetHandlers(IDataMutatorContext ctx)
        {
            return _dataMutators.Where(x => x.CanHandle(ctx));
        }
    }
}
using Acme.Data.DataModels.Contracts;
using Acme.Muators;
using Acme.Toolkit.Extensions;

namespace Acme.Data.Mutators
{
    public class OwnedByDataMutator : IDataMutatorHandler
    {
        public void BeforeAdd(object newState, IDataMutatorContext ctx)
        {
            newState.ThrowIfNull(nameof(newState));
            ctx.ThrowIfNull(nameof(ctx));

            var s = (IOwned)newState;
            s.OwnedBy = ctx.Identity.Name;
        }

        public void BeforeModify(object oldState, object newState, IDataMutatorContext ctx)
        {
            newState.ThrowIfNull(nameof(newState));
            ctx.ThrowIfNull(nameof(ctx));

            var s = (IOwned)newState;
            s.UpdatedBy = ctx.Identity.Name;
            s.UpdatedOn = ctx.RequestedOn;
        }

        public void BeforeRemove(object oldState, IDataMutatorContext ctx)
        {
            oldState.ThrowIfNull(nameof(oldState));
            ctx.ThrowIfNull(nameof(ctx));

            var s = (IOwned)oldState;
            s.RemovedBy = ctx.Identity.Name;
            s.RemovedOn = ctx.RequestedOn;
        }

        public bool CanHandle(IDataMutatorContext ctx)
        {
            if (ctx.Action != DataActions.Add) return false;
            if (ctx.DataSubjectType != typeof(IOwned)) return false;
            return true;
        }
    }
}
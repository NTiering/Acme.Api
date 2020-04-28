using Acme.Data.DataModels;
using Acme.Data.DataModels.Contracts;
using Acme.Muators;
using Acme.Toolkit.Extensions;
using System;
using System.Linq;

namespace Acme.Data.Mutators
{
    public class AddIdDataMutator : IDataMutatorHandler
    {
        public void BeforeAdd(object subject, IDataMutatorContext context)
        {
            subject.ThrowIfNull(nameof(subject));
            context.ThrowIfNull(nameof(context));

            if ((subject is BaseDataModel s) && (s.Id == Guid.Empty))
            {
                s.Id = Guid.NewGuid();
            }
        }

        public void BeforeModify(object oldState, object newState, IDataMutatorContext context)
        {
        }

        public void BeforeRemove(object oldState, IDataMutatorContext context)
        {
        }

        public bool CanHandle(IDataMutatorContext ctx)
        {
            if (ctx.Action != DataActions.Add) return false;
            if (ctx.DataSubjectType.GetInterfaces().Contains(typeof(ICreatedOn)) == false) return false;
            return true;
        }
    }
}
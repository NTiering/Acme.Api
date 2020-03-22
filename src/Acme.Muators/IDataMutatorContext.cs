using System;
using System.Security.Principal;

namespace Acme.Muators
{
    public interface IDataMutatorContext
    {
        DataActions Action { get; }
        Type DataSubjectType { get; }
        IIdentity Identity { get; }
        DateTime RequestedOn { get; }
    }
}
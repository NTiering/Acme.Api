using System;
using System.Security.Principal;

namespace Acme.Muators
{
    public class DataMutatorContext : IDataMutatorContext
    {
        public DataActions Action { get; set; }
        public Type DataSubjectType { get; set; }
        public IIdentity Identity { get; set; }
        public DateTime RequestedOn { get; set; }
    }
}
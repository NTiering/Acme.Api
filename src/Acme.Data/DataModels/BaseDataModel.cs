using Acme.Data.DataModels.Contracts;
using System;

namespace Acme.Data.DataModels
{
    public abstract class BaseDataModel : IDataModel
    {
        public DateTime CreatedOn { get; set; }
        public DateTime? DeletedOn { get; set; }
        public Guid Id { get; set; }
        public virtual string LogDescriptorText => GetType().Name;
    }
}
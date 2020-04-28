using Acme.Data.DataModels.Contracts;
using System;
using System.ComponentModel.DataAnnotations;

namespace Acme.Data.DataModels
{
    public abstract class BaseDataModel : IDataModel
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? DeletedOn { get; set; }
        public virtual string LogDescriptorText => GetType().Name;
    }
}
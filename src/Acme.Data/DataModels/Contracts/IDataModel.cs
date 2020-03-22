using System;

namespace Acme.Data.DataModels.Contracts
{
    public interface IDataModel
    {
        DateTime? DeletedOn { get; set; }
        Guid Id { get; set; }
    }
}
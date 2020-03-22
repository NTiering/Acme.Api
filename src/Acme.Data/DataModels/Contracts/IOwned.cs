using System;

namespace Acme.Data.DataModels.Contracts
{
    public interface IOwned
    {
        string OwnedBy { get; set; }
        string RemovedBy { get; set; }
        DateTime RemovedOn { get; set; }
        string UpdatedBy { get; set; }
        DateTime UpdatedOn { get; set; }
    }
}
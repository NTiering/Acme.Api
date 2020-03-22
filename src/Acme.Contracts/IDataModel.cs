using System;

namespace Acme.Contracts
{
    public interface IDataModel
    {
        Guid Id { get; set; }
        DateTime DeletedOn { get; set; }
        DateTime CreatedOn { get; set; }
        string LogDescriptorText { get; }
    }
}

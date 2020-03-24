using Acme.Data.DataModels.Contracts;
using System;
using System.Security.Principal;
using System.Threading.Tasks;

namespace Acme.Data.Context
{
    public interface IDataContext
    {
        Task Add<T>(T model, IIdentity identity) where T : class, IDataModel;

        Task<T> GetModel<T>(Guid id) where T : class, IDataModel;

        Task<bool> Modify<T>(T model, IIdentity identity) where T : class, IDataModel;

        Task<bool> Remove<T>(Guid id, IIdentity identity) where T : class, IDataModel;
    }
}
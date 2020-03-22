using Acme.Data.DataModels.Contracts;
using System;
using System.Threading.Tasks;

namespace Acme.Data.Context
{
    public interface ICrudDataTools
    {
        Task AddModel<T>(T model) where T : class, IDataModel;

        Task<T> GetModel<T>(Guid id) where T : class, IDataModel;

        Task ModifyModel<T>(T original, T model) where T : class, IDataModel;

        Task RemoveModel<T>(Guid id) where T : class, IDataModel;
    }
}
using Acme.ChangeHandlers;
using Acme.Data.DataModels.Contracts;
using Acme.Muators;
using Acme.Toolkit.Extensions;
using System;
using System.Security.Principal;
using System.Threading.Tasks;

namespace Acme.Data.Context
{
    public class DataContext : IDataContext
    {
        private readonly IChangeTracker _changeTracker;
        private readonly ICrudDataTools _crudDataTools;
        private readonly IDataMutator _dataMutator;

        public DataContext(IChangeTracker changeTracker, IDataMutator dataMutator, ICrudDataTools crudDataTools)
        {
            _changeTracker = changeTracker;
            _dataMutator = dataMutator;
            _crudDataTools = crudDataTools;
        }

        public async Task Add<T>(T model, IIdentity identity)
            where T : class, IDataModel
        {
            model.ThrowIfNull(nameof(model));
            identity.ThrowIfNull(nameof(identity));

            await _dataMutator.BeforeAdd(model, identity);
            await AddRecord(model);
            await _changeTracker.BroadcastAdd(model, identity);
        }

        public async Task<T> Get<T>(Guid id)
                    where T : class, IDataModel
        {
            var rtn = await _crudDataTools.GetModel<T>(id);
            return rtn;
        }

        public async Task<bool> Modify<T>(T model, IIdentity identity)
                    where T : class, IDataModel
        {
            model.ThrowIfNull(nameof(model));
            identity.ThrowIfNull(nameof(identity));

            var original = await Get<T>(model.Id);
            if (original == null) return false;

            await _dataMutator.BeforeModify(original, model, identity);
            await ModifyRecord(original, model);
            await _changeTracker.BroadcastModify(original, model, identity);

            return true;
        }

        public async Task<bool> Remove<T>(Guid id, IIdentity identity)
            where T : class, IDataModel
        {
            identity.ThrowIfNull(nameof(identity));

            var original = await Get<T>(id);

            await _dataMutator.BeforeRemove(original, identity);
            await RemoveRecord<T>(id);
            await _changeTracker.BroadcastDelete(original, identity);

            return true;
        }

        private async Task AddRecord<T>(T model)
            where T : class, IDataModel
        {
            model.Id = Guid.NewGuid();
            await _crudDataTools.AddModel(model);
        }

        private async Task ModifyRecord<T>(T original, T model)
                    where T : class, IDataModel
        {
            await _crudDataTools.ModifyModel(original, model);
        }

        private async Task RemoveRecord<T>(Guid id)
                                            where T : class, IDataModel
        {
            await _crudDataTools.RemoveModel<T>(id);
        }
    }
}
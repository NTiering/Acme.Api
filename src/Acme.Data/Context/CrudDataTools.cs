using Acme.Data.DataModels.Contracts;
using Acme.Web.Api.Config;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Acme.Data.Context
{
    public class CrudDataTools : ICrudDataTools
    {
        private readonly IApplicationConfigurationFactory _configurationFactory;

        public CrudDataTools(IApplicationConfigurationFactory configurationFactory)
        {
            _configurationFactory = configurationFactory;
        }

        private string _connectionstring => _configurationFactory.Config.ReadWriteConnectionString;

        public async Task AddModel<T>(T model) where T : class, IDataModel
        {
            using var ctx = new EntityFrameworkDataTools(_connectionstring);
            await ctx.AddAsync(model);
            await ctx.SaveChangesAsync();
        }

        public async Task<T> GetModel<T>(Guid id) where T : class, IDataModel
        {
            using var ctx = new EntityFrameworkDataTools(_connectionstring);
            var rtn = await ctx.FindAsync<T>(id);
            return rtn;
        }

        public async Task ModifyModel<T>(T original, T model) where T : class, IDataModel
        {
            using var ctx = new EntityFrameworkDataTools(_connectionstring);
            ctx.Attach(model);
            ctx.Entry(model).State = EntityState.Modified;
            await ctx.SaveChangesAsync();
        }

        public async Task RemoveModel<T>(Guid id) where T : class, IDataModel
        {
            using var ctx = new EntityFrameworkDataTools(_connectionstring);
            var model = await ctx.FindAsync<T>(id);
            ctx.Remove(model);
            await ctx.SaveChangesAsync();
        }
    }
}
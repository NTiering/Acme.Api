using Acme.Data.DataModels.Contracts;
using Acme.Web.Api.Config;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Acme.Data.Context
{
    public class CrudDataTools : ICrudDataTools
    {
        private readonly IApplicationConfiguration _configuration;

        public CrudDataTools(IApplicationConfiguration configuration)
        {
            _configuration = configuration;
        }
    
        public async Task AddModel<T>(T model) where T : class, IDataModel
        {
            using var ctx = new EntityFrameworkDataTools(_configuration.ReadWriteConnectionString);
            await ctx.AddAsync(model);
            await ctx.SaveChangesAsync();
        }

        public async Task<T> GetModel<T>(Guid id) where T : class, IDataModel
        {
            using var ctx = new EntityFrameworkDataTools(_configuration.ReadWriteConnectionString);
            var rtn = await ctx.FindAsync<T>(id);
            return rtn;
        }

        public async Task ModifyModel<T>(T original, T model) where T : class, IDataModel
        {
            using var ctx = new EntityFrameworkDataTools(_configuration.ReadWriteConnectionString);
            ctx.Attach(model);
            ctx.Entry(model).State = EntityState.Modified;
            await ctx.SaveChangesAsync();
        }

        public async Task RemoveModel<T>(Guid id) where T : class, IDataModel
        {
            using var ctx = new EntityFrameworkDataTools(_configuration.ReadWriteConnectionString);
            var model = await ctx.FindAsync<T>(id);
            ctx.Remove(model);
            await ctx.SaveChangesAsync();
        }
    }
}
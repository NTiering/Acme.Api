using Acme.Data.DataModels;
using Acme.Web.Api.Config;
using Microsoft.EntityFrameworkCore;

namespace Acme.Data.Context
{
    public class SearchContext : ISearchContext
    {
        private readonly EntityFrameworkDataTools _dataTools;

        public SearchContext(IApplicationConfigurationFactory configurationFactory)
        {
            _dataTools = new EntityFrameworkDataTools(configurationFactory.Config.ReadOnlyString);
        }

        public DbSet<ProductCategoryDataModel> ProductCategories => _dataTools.ProductCategories;
        public DbSet<ProductDataModel> Products => _dataTools.Products;
    }
}
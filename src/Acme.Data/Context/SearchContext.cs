using Acme.Data.DataModels;
using Acme.Web.Api.Config;
using Microsoft.EntityFrameworkCore;

namespace Acme.Data.Context
{
    public class SearchContext : ISearchContext
    {
        private readonly EntityFrameworkDataTools _dataTools;

        public SearchContext(IApplicationConfiguration configuration)
        {
            _dataTools = new EntityFrameworkDataTools(configuration.ReadOnlyString);
        }

        public DbSet<ProductCategoryDataModel> ProductCategories => _dataTools.ProductCategories;
        public DbSet<ProductReviewDataModel> ProductReviews => _dataTools.ProductReviews;
        public DbSet<ProductDataModel> Products => _dataTools.Products;
    }
}
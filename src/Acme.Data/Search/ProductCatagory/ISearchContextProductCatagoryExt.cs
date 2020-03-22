using Acme.Data.Context;
using Acme.Data.DataModels;
using Acme.Toolkit.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Acme.Data.Search.ProductCatagory
{
    public static class ISearchContextProductCategoryExt
    {
        public static PaginatedResult<ProductCategorySearchResult> GetCategories(this ISearchContext search, int pageCount, int pageSize)
        {
            var timer = new SearchTimer();

            var results = search.ProductCategories
                .ElligibleProductCategories()
                .OrderBy(x => x.Name);

            var rtn = ToPaginatedResult(search, pageCount, pageSize, timer.Duration, results);
            return rtn;
        }

        private static IQueryable<ProductCategoryDataModel> ElligibleProductCategories(this DbSet<ProductCategoryDataModel> products)
        {
            return products.Where(x => x.DeletedOn == null || x.DeletedOn > DateTime.Now);
        }

        private static PaginatedResult<ProductCategorySearchResult> ToPaginatedResult(ISearchContext search, int pageCount, int pageSize, long duration, IOrderedQueryable<ProductCategoryDataModel> results)
        {
            var rtn = new PaginatedResult<ProductCategorySearchResult>
            (
                pageSize: pageSize,
                pageCount: pageCount,
                searchDuration: duration,
                totalResults: results.Count(),
                items: results.Paginate(pageCount, pageSize).Select(x => ToSearchResult(search, x))
            );

            return rtn;
        }

        private static ProductCategorySearchResult ToSearchResult(ISearchContext search, ProductCategoryDataModel prd)
        {
            var rtn = new ProductCategorySearchResult
            {
                ProductCategoryId = prd.Id,
                ProductCategoryName = prd.Name
            };

            return rtn;
        }
    }
}
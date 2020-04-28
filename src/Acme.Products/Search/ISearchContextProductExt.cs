using Acme.Data.Context;
using Acme.Data.DataModels;
using Acme.Data.Search;
using Acme.Toolkit.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Acme.Products.Search
{
    public static class ISearchContextProductExt
    {
        public static ProductSearchResult Get(this ISearchContext search, Guid id)
        {
            var prd = FindOne(search, id);

            if (prd == null) return null;
            var rtn = ToSearchResult(search, prd);
            return rtn;
        }

        public static PaginatedResult<ProductSearchResult> GetByCategory(this ISearchContext search, Guid catId, int pageCount, int pageSize)
        {
            var timer = new SearchTimer();

            var results = search.Products
                .ElligibleProducts()
                .Where(x => x.CategoryId == catId)
                .OrderByDescending(x => x.Price);

            var rtn = ToPaginatedResult(search, pageCount, pageSize, timer.Duration, results);
            return rtn;
        }

        public static PaginatedResult<ProductSearchResult> GetByDiscount(this ISearchContext search, int pageCount, int pageSize)
        {
            var timer = new SearchTimer();

            var results = search.Products
                .ElligibleProducts()
                .OrderByDescending(x => x.Discount);

            var rtn = ToPaginatedResult(search, pageCount, pageSize, timer.Duration, results);
            return rtn;
        }

        public static PaginatedResult<ProductSearchResult> GetByPrice(this ISearchContext search, decimal min, decimal max, int pageCount, int pageSize)
        {
            var timer = new SearchTimer();

            var results = search.Products
                .ElligibleProducts()
                .Where(x => x.Price >= min && x.Price <= max)
                .OrderByDescending(x => x.Price);

            var rtn = ToPaginatedResult(search, pageCount, pageSize, timer.Duration, results);
            return rtn;
        }

        public static PaginatedResult<ProductSearchResult> GetByStockLevels(this ISearchContext search, StockLevel[] stockLevels, int pageCount, int pageSize)
        {
            var timer = new SearchTimer();

            var results = search.Products
                .ElligibleProducts()
                .Where(x => stockLevels.Contains(x.StockLevel))
                .OrderByDescending(x => x.Price);

            var rtn = ToPaginatedResult(search, pageCount, pageSize, timer.Duration, results);
            return rtn;
        }

        public static PaginatedResult<ProductSearchResult> GetByText(this ISearchContext search, string text, int pageCount, int pageSize)
        {
            var timer = new SearchTimer();

            var results = search.Products
                .ElligibleProducts()
                .Where(x => x.Name.ToLower().Contains(text) || x.Description.ToLower().Contains(text))
                .OrderByDescending(x => x.Price);

            var rtn = ToPaginatedResult(search, pageCount, pageSize, timer.Duration, results);
            return rtn;
        }

        public static ProductReviewSearchResult GetWithReviews(this ISearchContext search, Guid productId)
        {
            var timer = new SearchTimer();

            var prd = FindOne(search, productId);
            if (prd == null) return null;

            var reviews = search.ProductReviews
                .Where(x => x.ProductId == productId)
                .Where(x => x.DeletedOn == null)
                .OrderBy(x => x.Score);

            var rtn = new ProductReviewSearchResult
            {
                Product = ToSearchResult(search, prd),
                ReviewAverage = reviews.Average(x => x.Score),
                ReviewCount = reviews.Count(),
                TopResults = reviews.Take(3).Select(x => ToReviewSearchResult(x)),
                BottomResults = reviews.Skip(reviews.Count() - 3).Take(3).Select(x => ToReviewSearchResult(x))
            };
            return rtn;
        }



        public static bool IsSkuInUse(this ISearchContext search, string sku)
        {
            var rtn = search.Products.Any(x => x.Sku == sku);
            return rtn;
        }

        private static IQueryable<ProductDataModel> ElligibleProducts(this DbSet<ProductDataModel> products)
        {
            return products.Where(x => x.DeletedOn == null || x.DeletedOn > DateTime.Now);
        }

        private static ProductDataModel FindOne(ISearchContext search, Guid id)
        {
            return search.Products.FirstOrDefault(x => x.Id == id && x.DeletedOn == null);
        }

        private static PaginatedResult<ProductSearchResult> ToPaginatedResult(ISearchContext search, int pageCount, int pageSize, long duration, IOrderedQueryable<ProductDataModel> results)
        {
            var rtn = new PaginatedResult<ProductSearchResult>
            (
                pageSize: pageSize,
                pageCount: pageCount,
                searchDuration: duration,
                totalResults: results.Count(),
                items: results.Paginate(pageCount, pageSize).Select(x => ToSearchResult(search, x))
            );

            return rtn;
        }

        private static ReviewSearchResult ToReviewSearchResult(ProductReviewDataModel model)
        {
            var rtn = new ReviewSearchResult
            {
                ReviewText = model.ReviewText,
                Score = model.Score,
                CreatedOn = model.CreatedOn
            };

            return rtn;
        }

        private static ProductSearchResult ToSearchResult(ISearchContext search, ProductDataModel prd)
        {
            var rtn = new ProductSearchResult
            {
                ProductId = prd.Id,
                ProductSku = prd.Sku,
                ProductName = prd.Name,
                ProductDescription = prd.Description,
                ProductDiscountAmount = prd.Discount,
                ProductDiscountText = string.Format("{0:C}", prd.Discount),
                ProductPriceAmount = prd.Price,
                ProductPriceText = string.Format("{0:C}", prd.Price),
                ProductCategoryId = prd.CategoryId,
                ProductCategoryName = search.ProductCategories.FirstOrDefault(x => x.Id == prd.CategoryId)?.Name ?? string.Empty,
                StockLevel = ToStockLevelText(prd.StockLevel),
                StockLevelId = (int)prd.StockLevel,
            };

            return rtn;
        }

        private static string ToStockLevelText(StockLevel stockLevel)
        {
            switch (stockLevel)
            {
                case StockLevel.Low: return "Only a few left";
                case StockLevel.SoldOutMoreNoRestock: return "Sold out";
                case StockLevel.SoldOutMoreSoon: return "Sold out, more comming soon!";
                default: return string.Empty;
            }
        }
    }
}
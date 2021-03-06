﻿using Acme.Caching;
using Acme.Data.Context;
using Acme.Data.DataModels;
using Acme.Products.Search;
using Acme.Web.Api.Ext;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Acme.Web.Api.Controllers
{
    /// <summary>
    /// Separate controllers for search and Create Update and Delete (CrUD)
    /// mean that search operations don’t need to instantiate Crud infrastructure.
    /// </summary>
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/products")]
    public class ProductSearchController : ControllerBase
    {
        private readonly ISearchContext _searchContext;
        private ICacheProvider _cacheProvider;

        public ProductSearchController(ISearchContext searchContext, ICacheProvider cacheProvider)
        {
            _searchContext = searchContext;
            _cacheProvider = cacheProvider;
        }

        /// <summary>
        /// Gets all products
        /// </summary>
        [HttpGet("{pageSize}/{pageCount}")]
        public IActionResult Get(int pageSize = 25, int pageCount = 0)
        {
            var result = _cacheProvider.Get(Request.Path.ToCacheKey(), () => // example of in memory cache, use on smaller sites only
            {
                return _searchContext.GetByDiscount(pageCount, pageSize); // called only if the cache fails
            },
            CacheDuration.Short);

            return Ok(result);
        }

        /// <summary>
        /// Gets all products in a category
        /// </summary>
        [HttpGet("category/{catId}/{pageSize?}/{pageCount?}")]
        public IActionResult GetByCategory(Guid catId, int pageSize = 25, int pageCount = 0)
        {
            return this.OkOrNotFound(_searchContext.GetByCategory(catId, pageCount, pageSize));
        }

        /// <summary>
        /// Gets a single product
        /// </summary>
        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            return this.OkOrNotFound(_searchContext.Get(id));
        }

        /// <summary>
        /// Gets a single product, with optional data tacked on
        /// </summary>
        [HttpGet("{id}/withreviews")]
        public IActionResult GetByIdWithReviews(Guid id)
        {
            var result = _cacheProvider.Get($"productwithreview_{id}", () => // example of in memory cache, use on smaller sites only
            {
                return _searchContext.GetWithReviews(id); // called only if the cache fails
            },
            CacheDuration.Short);

            return this.OkOrNotFound(_searchContext.GetWithReviews(id));
        }

        /// <summary>
        /// Gets all products between two prices
        /// </summary>
        [HttpGet("price/{min}/{max}/{pageSize?}/{pageCount?}")]
        public IActionResult GetByPrice(decimal min, decimal max, int pageSize = 0, int pageCount = 25)
        {
            return Ok(_searchContext.GetByPrice(min, max, pageCount, pageSize));
        }

        /// <summary>
        /// Gets all products by stock level
        /// </summary>
        [HttpGet("stocklevel/{stockLevel}/{pageSize?}/{pageCount?}")]
        public IActionResult GetByStockLevel(StockLevel stockLevel, int pageSize = 25, int pageCount = 0)
        {
            return Ok(_searchContext.GetByStockLevels(new[] { stockLevel }, pageCount, pageSize));
        }

        /// <summary>
        /// Gets all product catagories
        /// </summary>
        [HttpGet("categories/{pageSize?}/{pageCount?}")]
        public IActionResult GetCategory(int pageSize = 25, int pageCount = 0)
        {
            return Ok(_searchContext.GetCategories(pageCount, pageSize));
        }

        /// <summary>
        /// Gets all products, filtered by free text
        /// </summary>
        [HttpGet("{search}/{pageSize}/{pageCount}")]
        public IActionResult GetSearch(string search, int pageSize = 25, int pageCount = 0)
        {
            return Ok(_searchContext.GetByText(search, pageCount, pageSize));
        }
    }
}
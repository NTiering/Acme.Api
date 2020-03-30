using Acme.Data.Context;
using Acme.Data.DataModels;
using Acme.Data.Search.Product;
using Acme.Data.Search.ProductCatagory;
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

        public ProductSearchController(ISearchContext searchContext)
        {
            _searchContext = searchContext;
        }

        /// <summary>
        /// Gets all products , filtered by catagory
        /// </summary>

        /// <summary>
        /// Gets all products
        /// </summary>
        [HttpGet("{pageSize}/{pageCount}")]
        public IActionResult Get(int pageSize = 25, int pageCount = 0)
        {
            return Ok(_searchContext.GetByDiscount(pageCount, pageSize));
        }

        [HttpGet("category/{catId}/{pageSize?}/{pageCount?}")]
        public IActionResult GetByCategory(Guid catId, int pageSize = 25, int pageCount = 0)
        {
            return Ok(_searchContext.GetByCategory(catId, pageCount, pageSize));
        }

        /// <summary>
        /// Gets all products between two prices
        /// </summary>
        [HttpGet("price/{min}/{max}/{pageSize?}/{pageCount?}")]
        public IActionResult GetByPrice(decimal min, decimal max, int pageSize = 0, int pageCount = 25)
        {
            return Ok(_searchContext.GetByPrice(min, max, pageCount, pageSize));
        }

        [HttpGet("stocklevel/{stockLevel}/{pageSize?}/{pageCount?}")]
        public IActionResult GetBySockLevel(StockLevel stockLevel, int pageSize = 25, int pageCount = 0)
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
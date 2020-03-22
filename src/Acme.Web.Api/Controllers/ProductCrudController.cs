using Acme.Data.Context;
using Acme.Data.DataModels;
using Acme.Data.Search.Product;
using Acme.Web.Api.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Acme.Web.Api.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductCrudController : ControllerBase
    {
        private readonly IDataContext _dataContext;
        private readonly ISearchContext _searchContext;

        public ProductCrudController(IDataContext dataContext, ISearchContext searchContext)
        {
            _dataContext = dataContext;
            _searchContext = searchContext;
        }

        /// <summary>
        /// Gets a single product
        /// </summary>
        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            return Ok(_searchContext.Get(id));
        }
        /// <summary>
        /// Add a new product
        /// </summary>
        /// <remarks>Use /api/products/categories to get a vaild catagory</remarks>

        [HttpPost]
        public IActionResult Post([FromBody] AddProductModel model)
        {
            var dataModel = model.ToDataModel();
            _dataContext.Add(dataModel, User.Identity);
            string uri = Url.Action("GetById", new { id = dataModel.Id });
            return Created(uri, dataModel);
        }

        /// <summary>
        /// Update a product description
        /// </summary>

        [HttpPut("description")]
        public async Task<IActionResult> UpdateDescription(UpdateDescription model)
        {
            var dataModel = await _dataContext.Get<ProductDataModel>(model.Id);
            if (dataModel == null) return NotFound();
            model.UpdateDataModel(dataModel);
            await _dataContext.Modify(dataModel, User.Identity);
            string uri = Url.Action("GetById", new { id = dataModel.Id });
            return Created(uri, dataModel);
        }

        /// <summary>
        /// Update a product discount
        /// </summary>

        [HttpPut("updatediscount")]
        public async Task<IActionResult> UpdateDiscount(UpdateDiscount model)
        {
            var dataModel = await _dataContext.Get<ProductDataModel>(model.Id);
            if (dataModel == null) return NotFound();
            model.UpdateDataModel(dataModel);
            await _dataContext.Modify(dataModel, User.Identity);
            string uri = Url.Action("GetById", new { id = dataModel.Id });
            return Created(uri, dataModel);
        }

        /// <summary>
        /// Update a products stock level
        /// </summary>

        [HttpPut("Updatestocklevel")]
        public async Task<IActionResult> UpdateStockLevel(UpdateStockLevel model)
        {
            var dataModel = await _dataContext.Get<ProductDataModel>(model.Id);
            if (dataModel == null) return NotFound();
            model.UpdateDataModel(dataModel);
            await _dataContext.Modify(dataModel, User.Identity);
            string uri = Url.Action("GetById",  new { id = dataModel.Id });
            return Created(uri, dataModel);
        }
    }
}
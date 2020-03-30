using Acme.Data.Context;
using Acme.Data.DataModels;
using Acme.Web.Api.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Acme.Web.Api.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/products")]
    public class ProductCrudController : ControllerBase
    {
        private readonly IDataContext _dataContext;

        public ProductCrudController(IDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        /// <summary>
        /// Gets a single product
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            return Ok(await _dataContext.GetModel<ProductDataModel>(id));
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
        public async Task<IActionResult> UpdateDescription([FromBody] UpdateDescription model)
        {
            var dataModel = await _dataContext.GetModel<ProductDataModel>(model.Id);
            if (dataModel == null) return NotFound();
            model.UpdateDataModel(dataModel);
            await _dataContext.Modify(dataModel, User.Identity);
            string uri = Url.Action("GetById", new { id = dataModel.Id });
            return Created(uri, dataModel);
        }

        /// <summary>
        /// Update a product discount
        /// </summary>

        [HttpPut("discount")]
        public async Task<IActionResult> UpdateDiscount([FromBody] UpdateDiscount model)
        {
            var dataModel = await _dataContext.GetModel<ProductDataModel>(model.Id);
            if (dataModel == null) return NotFound();
            model.UpdateDataModel(dataModel);
            await _dataContext.Modify(dataModel, User.Identity);
            string uri = Url.Action("GetById", new { id = dataModel.Id });
            return Created(uri, dataModel);
        }

        /// <summary>
        /// Update a products stock level
        /// </summary>

        [HttpPut("stocklevel")]
        public async Task<IActionResult> UpdateStockLevel([FromBody] UpdateStockLevel model)
        {
            var dataModel = await _dataContext.GetModel<ProductDataModel>(model.Id);
            if (dataModel == null) return NotFound();
            model.UpdateDataModel(dataModel);
            await _dataContext.Modify(dataModel, User.Identity);
            string uri = Url.Action("GetById", new { id = dataModel.Id });
            return Created(uri, dataModel);
        }
    }
}
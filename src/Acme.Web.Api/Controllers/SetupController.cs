using Acme.Data.Context;
using Acme.Web.Api.Config;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Acme.Web.Api.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/setup")]
    public class SetupController : ControllerBase
    {
        private readonly IDataContext _dataContext;

        public SetupController(IDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        /// <summary>
        /// Sets up example data, should be run be default
        /// </summary>

        [HttpGet()]
        public async Task<IActionResult> Get()
        {
            await ExampleDataSetup.Setup(_dataContext, User.Identity);
            return Ok("Done");
        }
    }
}
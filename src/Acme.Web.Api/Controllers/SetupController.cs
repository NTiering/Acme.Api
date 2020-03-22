using Acme.Data.Context;
using Acme.Web.Api.Config;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Acme.Web.Api.Controllers
{
    [ApiController]
    [Route("api/setup")]
    public class SetupController : ControllerBase
    {
        private readonly IDataContext _dataContext;

        public SetupController(IDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet()]
        public async Task<IActionResult> Get()
        {
            await ExampleDataSetup.Setup(_dataContext, User.Identity);
            return Ok("Done");
        }
    }
}
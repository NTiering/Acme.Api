using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Acme.Web.Api.Ext
{
    public static class ControllerBaseEtx
    {
        public static IActionResult OkOrNotFound(this ControllerBase controller, object o)
        {
            if (o == null)
            {
                return controller.NotFound();
            }
            else
            {
                return controller.Ok(o);
            }
        }


    }
    public static class PathStringExt
    {
        public static string ToCacheKey(this PathString pathString)
        {
            var rtn = pathString.Value.ToLower().Trim();
            return rtn;
        }
    }
}
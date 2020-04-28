using Microsoft.AspNetCore.Http;

namespace Acme.Web.Api.Ext
{
    public static class PathStringExt
    {
        public static string ToCacheKey(this PathString pathString)
        {
            var rtn = pathString.Value.ToLower().Trim();
            return rtn;
        }
    }
}
using Orchard.ContentManagement;
using System.Web;
using System.Web.Mvc;
using Orchard.Mvc.Html;

namespace MainBit.Utility.Extensions
{
    public static class UrlExtensions
    {
        public static string ItemDisplayUrlAbsolute(this UrlHelper urlHelper, IContent content)
        {
            string relativeUrl = urlHelper.ItemDisplayUrl(content);

            var request = HttpContext.Current.Request;
            return string.Format("http{0}://{1}{2}",
                request.IsSecureConnection ? "s" : "",
                request.Url.Host,
                relativeUrl
            );
        }

        public static string ConvertToAbsoluteUrl(this UrlHelper urlHelper, string url)
        {
            if (url.StartsWith("http://") || url.StartsWith("https://"))
            {
                return url;
            }

            var request = urlHelper.RequestContext.HttpContext.Request;

            //return string.Format("{0}://{1}{2}",
            //                   requestUrl.Scheme,
            //                   requestUrl.Authority,
            //                   VirtualPathUtility.ToAbsolute(virtualPath));

            return string.Format("http{0}://{1}{2}",
                request.IsSecureConnection ? "s" : "",
                request.Url.Host,
                VirtualPathUtility.ToAbsolute(url)
            );
        }
    }
}
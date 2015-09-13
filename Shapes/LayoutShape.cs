using Orchard;
using Orchard.DisplayManagement;
using Orchard.DisplayManagement.Descriptors;
using Orchard.Environment;
using Orchard.Mvc;
using Orchard.Mvc.Filters;
using Orchard.UI.PageClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MainBit.Utility.Shapes
{
    public class LayoutShape : IShapeTableProvider
    {
        private readonly IWorkContextAccessor _wca;

        public LayoutShape(
            IWorkContextAccessor wca
            )
        {
            _wca = wca;
        }

        public void Discover(ShapeTableBuilder builder)
        {
            builder.Describe("Layout").OnDisplaying(displaying =>
            {
                var workContext = _wca.GetContext();
                var httpContext = workContext.HttpContext;

                // add name alternate
                if (!string.IsNullOrWhiteSpace(httpContext.Request["layout-name"]))
                {
                    displaying.ShapeMetadata.Alternates.Add("Layout__Name__" + EncodeAlternateElement(httpContext.Request["layout-name"]));
                }

                // add not found alternate and class
                if (httpContext.Response.StatusCode == 404)
                {
                    displaying.ShapeMetadata.Alternates.Add("Layout__NotFound");
                    var pageClassBuilder = workContext.Resolve<IPageClassBuilder>();
                    pageClassBuilder.AddClassNames("not-found");
                }

                // add home class
                if (httpContext.Request.Url.AbsolutePath.TrimEnd('/').Equals(httpContext.Request.ApplicationPath.TrimEnd('/'), StringComparison.InvariantCultureIgnoreCase))
                {
                    var pageClassBuilder = workContext.Resolve<IPageClassBuilder>();
                    pageClassBuilder.AddClassNames("home");
                }


                // add routing alternates
                //var routeValues = httpContext.Request.RequestContext.RouteData.Values;
                //displaying.ShapeMetadata.Alternates.Add(BuildShapeName(routeValues, "area"));
                //displaying.ShapeMetadata.Alternates.Add(BuildShapeName(routeValues, "area", "controller"));
                //displaying.ShapeMetadata.Alternates.Add(BuildShapeName(routeValues, "area", "controller", "action"));
            });
        }

        /// <summary>
        /// Encodes dashed and dots so that they don't conflict in filenames 
        /// </summary>
        /// <param name="alternateElement"></param>
        /// <returns></returns>
        private string EncodeAlternateElement(string alternateElement)
        {
            return alternateElement.Replace("-", "__").Replace(".", "_");
        }

        // add routing alternates
        //private static string BuildShapeName(System.Web.Routing.RouteValueDictionary values, params string[] names)
        //{
        //    return "Layout__" +
        //        string.Join("__",
        //            names.Select(s =>
        //                ((string)values[s] ?? "").Replace(".", "_")));
        //}
    }
}
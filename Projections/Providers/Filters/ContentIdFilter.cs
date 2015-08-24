using System;
using System.Linq;
using System.Web.Mvc;
using Orchard.ContentManagement.MetaData;
using Orchard.DisplayManagement;
using Orchard.Forms.Services;
using Orchard.Localization;
using Orchard.Projections.Descriptors.Filter;
using IFilterProvider = Orchard.Projections.Services.IFilterProvider;
using Orchard.Core.Common.Models;
using Orchard.Environment.Extensions;
using Orchard.ContentManagement;

namespace MainBit.Utility.Projections.Providers.Filters
{
    [OrchardFeature("MainBit.Utility.Projections")]
    public class ContentIdFilter : IFilterProvider
    {
        public ContentIdFilter()
        {
            T = NullLocalizer.Instance;
        }

        public Localizer T { get; set; }

        public void Describe(DescribeFilterContext describe)
        {
            // TODO: change an orchard form with a custom form that will be contain specific opertaions (one of many, all of many)
            describe.For("Common", T("Common"), T("Common"))
                .Element("ContentId", T("Content Id"), T("Specific content id"),
                    ApplyFilter,
                    DisplayFilter,
                    Orchard.Projections.FilterEditors.Forms.NumericFilterForm.FormName
                );

        }

        public void ApplyFilter(FilterContext context)
        {
            // TODO: change an orchard form with a custom form that will be contain specific opertaions (one of many, all of many)
            Action<IHqlExpressionFactory> predicate = Orchard.Projections.FilterEditors.Forms.NumericFilterForm.GetFilterPredicate(context.State, "Id");
            Action<IAliasFactory> alias = x => x.ContentItem();
            context.Query = context.Query.Where(alias, predicate);
        }

        public LocalizedString DisplayFilter(FilterContext context)
        {
            // TODO: change an orchard form with a custom form that will be contain specific opertaions (one of many, all of many)
            return Orchard.Projections.FilterEditors.Forms.NumericFilterForm.DisplayFilter("Content with id", context.State, T);
        }
    }
}
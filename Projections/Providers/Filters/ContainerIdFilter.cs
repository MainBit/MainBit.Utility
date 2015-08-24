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

namespace MainBit.Utility.Projections.Providers.Filters
{
    // TODO: change an orchard numeric form with a custom form that will be contain specific opertaions (one of many, all of many)
    // and use custom form here and in ContentIdFilter

    [OrchardFeature("MainBit.Utility.Projections")]
    public class ContainerIdFilter : IFilterProvider
    {
        public ContainerIdFilter()
        {
            T = NullLocalizer.Instance;
        }

        public Localizer T { get; set; }

        public void Describe(DescribeFilterContext describe)
        {
            describe.For("Common", T("Common"), T("Common"))
                .Element("ContainerId", T("Conteiner Id"), T("Specific container ids"),
                    ApplyFilter,
                    DisplayFilter,
                    "ContainerIdFilter"
                );

        }

        public void ApplyFilter(FilterContext context)
        {
            var strContainerIds = (string)context.State.ContainerIds;
            if (!String.IsNullOrEmpty(strContainerIds))
            {
                var containerIds = strContainerIds
                    .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(p => Convert.ToInt32(p))
                    .ToList();

                context.Query = context.Query.Where(
                    a => a.ContentPartRecord<CommonPartRecord>(),
                    p => p.In("Container.Id", containerIds)
                );
            }
        }

        public LocalizedString DisplayFilter(FilterContext context)
        {
            var strContainerIds = (string)context.State.ContainerIds;

            if (String.IsNullOrEmpty(strContainerIds))
            {
                return T("Any container id");
            }

            return T("Content with contaier id {0}", strContainerIds);
        }
    }

    public class ContainerIdFilterForm : IFormProvider
    {
        public const string FormName = "ContainerIdFilter";
        protected dynamic Shape { get; set; }
        public Localizer T { get; set; }

        public ContainerIdFilterForm(
            IShapeFactory shapeFactory,
            IContentDefinitionManager contentDefinitionManager)
        {
            Shape = shapeFactory;
            T = NullLocalizer.Instance;
        }

        public void Describe(DescribeContext context)
        {
            Func<IShapeFactory, object> form =
                shape =>
                {

                    var f = Shape.Form(
                        Id: ContainerIdFilterForm.FormName,
                        _Parts: Shape.TextBox(
                            Id: "containerids", Name: "ContainerIds",
                            Title: T("Container Ids"),
                            Description: T("A comma separated list of ids."),
                            Classes: new[] { "tokenized" }
                            )
                        );

                    return f;
                };

            context.Form(ContainerIdFilterForm.FormName, form);
        }
    }

    public class ContainerIdFilterFormValidation : FormHandler
    {
        public Localizer T { get; set; }

        public override void Validating(ValidatingContext context)
        {
            if (context.FormName == ContainerIdFilterForm.FormName)
            {
                var value = context.ValueProvider.GetValue("ContainerIds");

                if (value == null || String.IsNullOrWhiteSpace(value.AttemptedValue)) {
                    context.ModelState.AddModelError("ContainerIds", T("The field {0} is required.", T("Container Ids").Text).Text);
                }

                var values = value.AttemptedValue.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                int output;
                if (values.Any(p => !Int32.TryParse(p, out output)) && !IsToken(value.AttemptedValue))
                {
                    context.ModelState.AddModelError("ContainerIds", T("The field {0} should contain a valid numbers", T("Container Ids").Text).Text);
                }
            }
        }

        private bool IsToken(string value)
        {
            return value.StartsWith("{") && value.EndsWith("}");
        }
    }
}
using Orchard.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.Tokens;
using Orchard.Taxonomies.Fields;
using Orchard.Environment.Extensions;

namespace MainBit.Utility.Taxonomies.Tokens
{
    [OrchardFeature("MainBit.Utility.Taxonomies")]
    public class TaxonomyTokens : ITokenProvider
    {
        public TaxonomyTokens()
        {
            T = NullLocalizer.Instance;
        }

        public Localizer T { get; set; }

        public void Describe(DescribeContext context)
        {
            // Usage:
            // Content.Fields.Article.Categories.TermIds -> '23,45,36'
            // Content.Fields.Article.Categories.Terms:0 -> 'Science'

            // When used with an indexer, it can be chained with Content tokens
            // Content.Fields.Article.Categories.Terms:0.DisplayUrl -> http://...

            context.For("TaxonomyField")
                   .Token("TermIds", T("Term Ids"), T("The term ids (Content) associated with field."))
                   ;
        }

        public void Evaluate(EvaluateContext context)
        {
            context.For<TaxonomyField>("TaxonomyField")
                   .Token("TermIds", field => String.Join(",", field.Terms.Select(t => t.Id).ToArray()))
                   ;
        }
    }
}
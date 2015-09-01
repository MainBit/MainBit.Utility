using System;
using System.Web;
using Orchard.ContentManagement;
using Orchard.Localization;
using Orchard.Tokens;
using Orchard;

namespace MainBit.Utility.Tokens.Providers
{
    public class RequestTokens : ITokenProvider {
        private readonly IWorkContextAccessor _workContextAccessor;
        private readonly IContentManager _contentManager;

        public RequestTokens(IWorkContextAccessor workContextAccessor, IContentManager contentManager) {
            _workContextAccessor = workContextAccessor;
            _contentManager = contentManager;
            T = NullLocalizer.Instance;
        }

        public Localizer T { get; set; }

        public void Describe(DescribeContext context) {
            context.For("Request")
                .Token("UrlReferrer", T("Url referrer"), T("The url referrer"))
            ;
        }

        public void Evaluate(EvaluateContext context) {
            if (_workContextAccessor.GetContext().HttpContext == null)
            {
                return;
            }

            context.For("Request", _workContextAccessor.GetContext().HttpContext.Request)
                .Token("UrlReferrer",
                    (request) => { 
                        return request.UrlReferrer;
                    });
        }
    }
}
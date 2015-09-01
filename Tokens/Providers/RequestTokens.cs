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
                .Token("RawUrl", T("Raw url"), T("The raw url"))
            ;
        }

        public void Evaluate(EvaluateContext context) {
            if (_workContextAccessor.GetContext().HttpContext == null) {
                return;
            }

            context.For<HttpRequestBase>("Request")
                .Token("RawUrl",
                    (request) => { 
                        return request.RawUrl ?? string.Empty;
                    });
        }
    }
}
using Orchard.Environment.Extensions;
using Orchard.Events;
using Orchard.Mvc;
using Orchard.Widgets.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MainBit.Utility.Rules
{
    [OrchardFeature("MainBit.Utility.Rules")]
    public class QueryStringRuleProvider : IRuleProvider
    {
        private readonly IHttpContextAccessor _hta;

        public QueryStringRuleProvider(IHttpContextAccessor hta)
        {
            _hta = hta;
        }

        public void Process(RuleContext ruleContext)
        {
            if (!String.Equals(ruleContext.FunctionName, "querystring", StringComparison.OrdinalIgnoreCase))
                return;
            
            var key = Convert.ToString(ruleContext.Arguments[0]);
            var value = ruleContext.Arguments.Length > 1 ? Convert.ToString(ruleContext.Arguments[1]) : null;

            var qsValue = _hta.Current().Request.QueryString[key];
            ruleContext.Result = qsValue == null
                ? false
                : value == null
                    ? true
                    : qsValue.Equals(value, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
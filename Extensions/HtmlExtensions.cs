﻿using MainBit.Utility.Services;
using Orchard.ContentManagement;
using Orchard.Mvc.Html;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MainBit.Utility.Extensions
{
    public static class HtmlExtensions {
        public static ContentItem GetCurrentContentItem(this HtmlHelper htmlHelper) {
            var workContext = htmlHelper.GetWorkContext();
            var currentContentAccessor = workContext.Resolve<ICurrentContentAccessor>();
            return currentContentAccessor.CurrentContentItem;
        }
    }
}
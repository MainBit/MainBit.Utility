using Orchard.ContentManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using MainBit.Utility.Extensions;

namespace MainBit.Utility.Services
{
    public class CurrentContentAccessor : ICurrentContentAccessor
    {
        private readonly IContentManager _contentManager;
        private readonly RequestContext _requestContext;

        public CurrentContentAccessor(IContentManager contentManager, RequestContext requestContext)
        {
            _contentManager = contentManager;
            _requestContext = requestContext;
        }

        private ContentItem _currentContentItem = null;
        public ContentItem CurrentContentItem
        {
            get
            {
                if (_currentContentItem == null)
                {
                    _currentContentItem = GetCurrentContentItem();
                }
                return _currentContentItem;
            }
        }

        private ContentItem GetCurrentContentItem()
        {
            var contentId = _requestContext.RouteData.Values.GetContentItemId();
            return contentId == null ? null : _contentManager.Get(contentId.Value);
        }
    }
}
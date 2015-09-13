using Orchard.ContentManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;

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

        private List<string> _idKeys = new List<string> { "id", "blogId" };
        public virtual List<string> IdKeys
        {
            get { return _idKeys; }
        }

        private ContentItem GetCurrentContentItem()
        {
            var contentId = GetCurrentContentItemId();
            return contentId == null ? null : _contentManager.Get(contentId.Value);
        }

        private int? GetCurrentContentItemId()
        {

            object id = null;
            if (IdKeys.Any(idKey => _requestContext.RouteData.Values.TryGetValue(idKey, out id)))
            {
                int contentId;
                if (int.TryParse(id as string, out contentId))
                    return contentId;
            }

            return null;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace MainBit.Utility.Extensions
{
    public static class RouteValueDictionaryExtensions
    {
        public static readonly List<string> IdKeys = new List<string> { "id", "blogId" };
        public static int? GetContentItemId(this RouteValueDictionary routeValueDictionary)
        {
            object id = null;
            if (IdKeys.Any(idKey => routeValueDictionary.TryGetValue(idKey, out id)))
            {
                int contentId;
                if (int.TryParse(id as string, out contentId))
                    return contentId;
            }

            return null;
        }
    }
}
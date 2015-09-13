// http://skywalkersoftwaredevelopment.net/blog/getting-the-current-content-item-in-orchard

using System.Linq;
using System.Web.Routing;
using Orchard;
using Orchard.ContentManagement;
using Orchard.Core.Common.Utilities;
using System.Collections.Generic;

namespace MainBit.Utility.Services {
	public interface ICurrentContentAccessor : IDependency {
		ContentItem CurrentContentItem { get; }
	}
}
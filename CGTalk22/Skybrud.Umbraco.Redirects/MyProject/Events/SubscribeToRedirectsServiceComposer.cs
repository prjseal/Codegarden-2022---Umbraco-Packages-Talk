using Newtonsoft.Json;
using Skybrud.Umbraco.Redirects.Notifications;
using System;
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.DependencyInjection;
using Umbraco.Cms.Core.Events;

namespace MyProject.Composing
{
    public class SubscribeToRedirectsServiceComposer : IComposer
    {
        public void Compose(IUmbracoBuilder builder)
        {
            //builder.AddNotificationHandler<RedirectPreLookupNotification, RedirectsPreLookupNotificationHandler>();
            //builder.AddNotificationHandler<RedirectPostLookupNotification, RedirectsPostLookupNotificationHandler>();
        }
    }

    public class RedirectsPreLookupNotificationHandler 
        : INotificationHandler<RedirectPreLookupNotification>
    {
        public void Handle(RedirectPreLookupNotification notification)
        {
            throw new Exception(notification.RawUrl);
        }
    }

    public class RedirectsPostLookupNotificationHandler
        : INotificationHandler<RedirectPostLookupNotification>
    {
        public void Handle(RedirectPostLookupNotification notification)
        {
            throw new Exception(JsonConvert.SerializeObject(notification.Redirect));
        }
    }
}

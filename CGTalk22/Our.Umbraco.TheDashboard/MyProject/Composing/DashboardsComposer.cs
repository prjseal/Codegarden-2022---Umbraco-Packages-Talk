using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.Dashboards;
using Umbraco.Cms.Core.DependencyInjection;

namespace MyProject.Composing
{
    public class DashboardsComposer : IComposer
    {
        public void Compose(IUmbracoBuilder builder)
        {
            //builder.Dashboards().Remove<ContentDashboard>();
        }
    }
}

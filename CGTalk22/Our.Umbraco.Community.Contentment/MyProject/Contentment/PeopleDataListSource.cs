using System.Collections.Generic;
using System.Linq;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.PropertyEditors;
using Umbraco.Cms.Core.Web;
using Umbraco.Community.Contentment.DataEditors;
using Umbraco.Extensions;

namespace MyProject.Contentment
{
    public class PeopleDataListSource : IDataListSource
    {
        private readonly IUmbracoContextAccessor _umbracoContextAccessor;

        public PeopleDataListSource(IUmbracoContextAccessor umbracoContextAccessor)
        {
            _umbracoContextAccessor = umbracoContextAccessor;
        }

        public string Name => "Author Content Items";

        public string Description => "Use authors content items as a data source.";

        public string Icon => "icon-users";

        public OverlaySize OverlaySize => OverlaySize.Small;

        public Dictionary<string, object> DefaultValues => new Dictionary<string, object>();

        public IEnumerable<ConfigurationField> Fields => Enumerable.Empty<ConfigurationField>();

        public string Group => "Custom Data Sources";

        public IEnumerable<DataListItem> GetItems(Dictionary<string, object> config)
        {
            if(!_umbracoContextAccessor.TryGetUmbracoContext(out var umbracoContext)) return null;

            List<DataListItem> results = new List<DataListItem>();

            //get call content items which are using the document type alias of 'person'
            var people = umbracoContext.Content.GetByXPath(false, "//person");

            //make sure there are some people items
            if (people != null && people.Any())
            {

                //loop through the people itmes
                foreach (var person in people)
                {

                    //generate a udi from the key property of the content item
                    //we will use this to store as the value of the author picker
                    var udi = Udi.Create(Constants.UdiEntityType.Document, person.Key);
                    if (udi == null) break;

                    //create a new DataListItem object to store the data
                    var item = new DataListItem()
                    {
                        Name = person.Name,
                        Value = udi.ToString()
                    };

                    //check if the person record has a photo
                    if (person.HasValue("photo"))
                    {
                        var photo = person.Value<IPublishedContent>("photo");
                        item.Icon = photo.GetCropUrl(120, 120);
                    }

                    //add the item to our list of results
                    results.Add(item);
                }

                return results;
            }

            return Enumerable.Empty<DataListItem>();
        }

    }
}

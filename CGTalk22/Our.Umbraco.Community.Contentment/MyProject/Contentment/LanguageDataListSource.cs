using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.PropertyEditors;
using Umbraco.Cms.Core.Services;
using Umbraco.Community.Contentment.DataEditors;

namespace MyProject.Contentment
{
    public class LanguageDataListSource : IDataListSource
    {
        private ILocalizationService _localizationService;

        public LanguageDataListSource(ILocalizationService localizationService)
        {
            _localizationService = localizationService;
        }

        public string Name => "Current Languages";

        public string Description => "Use the current languages as a data source";

        public string Icon => "icon-globe-europe-africa";

        public OverlaySize OverlaySize => OverlaySize.Small;

        public Dictionary<string, object> DefaultValues => new Dictionary<string, object>();

        public IEnumerable<ConfigurationField> Fields => Enumerable.Empty<ConfigurationField>();

        public string Group => "Custom Data Sources";

        public IEnumerable<DataListItem> GetItems(Dictionary<string, object> config)
        {
            var allLanguages = _localizationService.GetAllLanguages();

            List<DataListItem> results = new List<DataListItem>();

            //make sure there are some language items
            var enumerable = allLanguages as ILanguage[] ?? allLanguages.ToArray();
            if (enumerable.Any())
            {
                foreach (var language in enumerable)
                {
                    //create a new DataListItem object to store the data
                    var item = new DataListItem()
                    {
                        Name = language.CultureName,
                        Value = language.IsoCode
                    };

                    results.Add(item);
                }

                return results;
            }

            return Enumerable.Empty<DataListItem>();
        }
    }
}

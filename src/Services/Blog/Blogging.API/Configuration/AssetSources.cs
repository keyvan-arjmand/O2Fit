using Lexical.Localization;
using Lexical.Localization.Asset;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blogging.API.Configuration
{
    public class AssetSources : List<IAssetSource>, ILibraryAssetSources
    {
        public readonly LineFileSource ExternalLocalizationSource =
            LineReaderMap.Default.FileAssetSource("localization.ini", throwIfNotFound: false);

        public AssetSources() : base()
        {
            // Add optional external localization source
            Add(ExternalLocalizationSource);
        }
    }
}

using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.Linq;
using KariyerAnalytics.Business;
using KariyerAnalytics.Business.Contract;
using KariyerAnalytics.Data.Repositories;

namespace KariyerAnalytics.DependencyInjection
{
    public static class MefLoader
    {
        public static CompositionContainer Init(ICollection<ComposablePartCatalog> catalogParts)
        {
            AggregateCatalog catalog = new AggregateCatalog();
            
            catalog.Catalogs.Add(new AssemblyCatalog(typeof(LogEngine).Assembly));
            catalog.Catalogs.Add(new AssemblyCatalog(typeof(ILogEngine).Assembly));
            catalog.Catalogs.Add(new AssemblyCatalog(typeof(LogRepository).Assembly));

            if (catalogParts != null)
            {
                catalog.Catalogs.Concat(catalogParts);
            }
            
            CompositionContainer container = new CompositionContainer(catalog, true);
            
            return container;
        }
        
    }
}
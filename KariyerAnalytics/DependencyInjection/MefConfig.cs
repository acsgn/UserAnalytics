using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.Reflection;
using KariyerAnalytics.Common.DependencyInjection;

namespace KariyerAnalytics.DependencyInjection
{
    public static class MefConfig
    {
        public static void RegisterMef()
        {
            ICollection<ComposablePartCatalog> catalogParts = new List<ComposablePartCatalog>();
            catalogParts.Add(new AssemblyCatalog(Assembly.GetExecutingAssembly()));
            ObjectBase.Container = MefLoader.Init(catalogParts);
            System.Web.Http.GlobalConfiguration.Configuration.DependencyResolver = new MefDependencyResolver(ObjectBase.Container);
        }
    }
}
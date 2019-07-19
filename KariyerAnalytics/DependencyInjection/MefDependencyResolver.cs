using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Web.Http.Dependencies;

namespace KariyerAnalytics
{
    public class MefDependencyResolver : IDependencyResolver
    {
        private readonly CompositionContainer _container;

        public MefDependencyResolver(CompositionContainer container)
        {
            _container = container;
        }

        public IDependencyScope BeginScope()
        {
            return this;
        }

        public object GetService(Type serviceType)
        {
            var export = _container.GetExports(serviceType, null, null).SingleOrDefault();

            return export?.Value;
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            var exports = _container.GetExports(serviceType, null, null);
            var createdObjects = new List<object>();

            if (exports.Any())
            {
                foreach (var export in exports)
                {
                    createdObjects.Add(export.Value);
                }
            }

            return createdObjects;
        }

        public void Dispose()
        {
            ;
        }
    }
}
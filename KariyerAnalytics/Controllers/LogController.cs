using System.Web;
using System.Web.Http;
using KariyerAnalytics.Service.Entities;
using System.ComponentModel.Composition;
using KariyerAnalytics.Business.Contract;
using KariyerAnalytics.Common.DependencyInjection;

namespace KariyerAnalytics.Controllers
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class LogController : ApiController
    {
        [Import]
        private ILogEngine _LogEngine;

        public LogController()
        {
            if (ObjectBase.Container != null)
            {
                ObjectBase.Container.SatisfyImportsOnce(this);
            }
        }

        [HttpPost]
        public void Create(LogInformation info)
        {
            info.IP = HttpContext.Current.Request.UserHostAddress;
            info.Timestamp = HttpContext.Current.Timestamp;
            _LogEngine.Add(info);
        }
    }
}

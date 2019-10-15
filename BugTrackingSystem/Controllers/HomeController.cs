using NLog;
using System.Web.Configuration;
using System.Web.Mvc;
using BugTrackingSystem.Repository;

namespace BugTrackingSystem.Controllers
{
    public class HomeController : Controller
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();
        public ActionResult Index()
        {
            return View();
        }

        // GET: Configure Connection Strings
        public ActionResult Configure()
        {
            ViewBag.ConnectionStrings = BugTrackingContext.GetConnectionString();
            return View();
        }

        // POST: Configure Connection Strings
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Configure(string connectionStrings)
        {
            var config = WebConfigurationManager.OpenWebConfiguration("~");
            if (BugTrackingContext.GetConnectionString() != connectionStrings)
            {
                _logger.Warn("Connection string was changed from {0} to {1}",
                    WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString,
                    connectionStrings);

                config.AppSettings.Settings["SessionConnectionStrings"].Value = connectionStrings;
                config.Save();
            }

            return RedirectToAction("Index");
        }
    }
}
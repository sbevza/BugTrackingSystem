using BugTrackingSystem.Models;
using BugTrackingSystem.Service;
using NLog;
using System.Data;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BugTrackingSystem.Controllers
{
    public class ProjectController : Controller
    {
        private IProjectService _projectService;
        private IUserTaskService _userTaskService;
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        public ProjectController(IProjectService projectService, IUserTaskService userTaskService)
        {
            _projectService = projectService;
            _userTaskService = userTaskService;
        }

        // GET: List
        public async Task<ActionResult> Index()
        {
            return View(await _projectService.GetAll());
        }

        // GET: Details
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                _logger.Error("Error receiving Details() of project. id == null");
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = await _projectService.Get((int)id);
            if (project == null)
            {
                _logger.Error("Error receiving Details() of project. The task is not found");
                return HttpNotFound();
            }
            var usersTasks = await _userTaskService.GetAll();
            usersTasks = usersTasks.Where(u => u.ProjectId == id);
            ViewBag.Project = project;
            
            return View(usersTasks);
        }

        // GET: Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name")] Project project)
        {
            if (ModelState.IsValid)
            {
                await _projectService.Add(project);
                return RedirectToAction("Index");
            }
            _logger.Warn("Create(). Validation error during project creation.");
            return View(project);
        }

        // GET: Edit
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                _logger.Error("Error receiving Edit() of project. id == null");
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = await _projectService.Get((int)id);
            if (project == null)
            {
                _logger.Error("Error receiving Edit() of project. The task is not found");
                return HttpNotFound();
            }
            return View(project);
        }

        // POST: Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name")] Project project)
        {
            if (ModelState.IsValid)
            {
                await _projectService.Update(project);
                return RedirectToAction("Index");
            }
            _logger.Warn("Edit(). Validation error during project updating.");
            return View(project);
        }

        // GET: Delete
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                _logger.Error("Error receiving Delete() of project. id == null");
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            await _projectService.Delete((int)id);
            return RedirectToAction("Index");
        }
    }
}

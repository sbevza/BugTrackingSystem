using BugTrackingSystem.Service;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using NLog;

namespace BugTrackingSystem.Models
{
    public class UsersTaskController : Controller
    {
        private IUserTaskService _userTaskService;
        private IProjectService _projectService;
        private IUserService _userService;
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        public UsersTaskController(IUserTaskService userTaskService, IProjectService projectService, IUserService userService)
        {
            _userTaskService = userTaskService;
            _projectService = projectService;
            _userService = userService;
        }

        // GET: List
        public async Task<ActionResult> Index()
        {
            return View(await _userTaskService.GetAll());
        }

        // GET: Details
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                _logger.Error("Error receiving Details() of task. id == null");
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UsersTask usersTask = await _userTaskService.Get((int)id);
            if (usersTask == null)
            {
                _logger.Error("Error receiving Details() of task. The task is not found");
                return HttpNotFound();
            }
            return View(usersTask);
        }

        // GET: Create
        public async Task<ActionResult> Create()
        {
            ViewBag.ProjectId = new SelectList(await _projectService.GetAll(), "Id", "Name");
            ViewBag.UserId = new SelectList(await _userService.GetAll(), "Id", "UserLogin");
            return View();
        }

        // POST: Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,ProjectId,Topic,Type,Priority,UserId")] UsersTask usersTask)
        {
            if (ModelState.IsValid)
            {
                await _userTaskService.Add(usersTask);
                return RedirectToAction("Index");
            }
            _logger.Warn("Create(). Validation error during task creation.");
            ViewBag.ProjectId = new SelectList(await _projectService.GetAll(), "Id", "Name", usersTask.ProjectId);
            ViewBag.UserId = new SelectList(await _userService.GetAll(), "Id", "UserLogin", usersTask.UserId);
            return View(usersTask);
        }

        // GET: Edit
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                _logger.Error("Error receiving Edit() of task. id == null");
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UsersTask usersTask = await _userTaskService.Get((int)id);
            if (usersTask == null)
            {
                _logger.Error("Error receiving Edit() of task. The task is not found");
                return HttpNotFound();
            }
            ViewBag.ProjectId = new SelectList(await _projectService.GetAll(), "Id", "Name", usersTask.ProjectId);
            ViewBag.UserId = new SelectList(await _userService.GetAll(), "Id", "UserLogin", usersTask.UserId);
            return View(usersTask);
        }

        // POST: Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,ProjectId,Topic,Type,Priority,UserId")] UsersTask usersTask)
        {
            if (ModelState.IsValid)
            {
                await _userTaskService.Update(usersTask);
                return RedirectToAction("Index");
            }
            _logger.Warn("Edit(). Validation error during task updating.");
            ViewBag.ProjectId = new SelectList(await _projectService.GetAll(), "Id", "Name", usersTask.ProjectId);
            ViewBag.UserId = new SelectList(await _userService.GetAll(), "Id", "UserLogin", usersTask.UserId);
            return View(usersTask);
        }

        // GET: Delete
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                _logger.Error("Error receiving Delete() of task. id == null");
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            await _userTaskService.Delete((int)id);
            return RedirectToAction("Index");
        }
    }
}

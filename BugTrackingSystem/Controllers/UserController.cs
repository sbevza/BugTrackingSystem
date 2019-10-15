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
    public class UserController : Controller
    {
        private IUserService _userService;
        private IUserTaskService _userTaskService;
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        public UserController(IUserService userService, IUserTaskService userTaskService)
        {
            _userService = userService;
            _userTaskService = userTaskService;
        }

        // GET: All Users
        public async Task<ActionResult> Index()
        {
            return View(await _userService.GetAll());
        }

        // GET: Details
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                _logger.Error("Error receiving Details() of user. id == null");
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = await _userService.Get((int)id);
            if (user == null)
            {
                _logger.Error("Error receiving Details() of user. The task is not found");
                return HttpNotFound();
            }
            var usersTasks = await _userTaskService.GetAll();
            usersTasks = usersTasks.Where(u => u.UserId == id);
            ViewBag.User = user;

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
        public async Task<ActionResult> Create([Bind(Include = "Id,UserLogin,UserName")] User user)
        {
            if (ModelState.IsValid)
            {
                await _userService.Add(user);
                return RedirectToAction("Index");
            }
            _logger.Warn("Create(). Validation error during user creation.");
            return View(user);
        }

        // GET: Edit
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                _logger.Error("Error receiving Edit() of user. id == null");
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = await _userService.Get((int)id);
            if (user == null)
            {
                _logger.Error("Error receiving Edit() of user. The task is not found");
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,UserLogin,UserName")] User user)
        {
            if (ModelState.IsValid)
            {
                await _userService.Update(user);
                return RedirectToAction("Index");
            }
            _logger.Warn("Edit(). Validation error during user updating.");
            return View(user);
        }

        // GET: Delete
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                _logger.Error("Error receiving Delete() of user. id == null");
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            await _userService.Delete((int)id);

            return RedirectToAction("Index");
        }
    }
}

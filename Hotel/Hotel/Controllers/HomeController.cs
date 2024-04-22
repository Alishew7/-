using System.Diagnostics;
using Hotel.Data;
using Hotel.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Hotel.Filters;

namespace Hotel.Controllers
{
    public class HomeController : Controller
    {
		private readonly AppDbcontext _db;

		private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, AppDbcontext context)
        {
            _logger = logger;
            _db = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
		public IActionResult Index(User model)
		{
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            User loggedUser = _db.User.SingleOrDefault(u=> u.UserName == model.UserName && u.Password == model.Password);

            if(loggedUser == null)
            {
                this.ModelState.AddModelError("AuthError", "Invalid username or password");
                return View(model);
            }

            string jsonData = JsonSerializer.Serialize(loggedUser);
            HttpContext.Session.SetString("loggedUser", jsonData);

			return RedirectToAction("Index", "Floor");
		}
        [AuthFilter]
        public IActionResult Logout()
        {
			HttpContext.Session.Remove("loggedUser");
			return RedirectToAction("Login", "Home");
        }
		public IActionResult Privacy()
        {
            
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
using Hotel.Data;
using Hotel.Filters;
using Hotel.Models.View_Models;
using Hotel.Models;
using Microsoft.AspNetCore.Mvc;

namespace Hotel.Controllers
{

    [AuthFilter]
    public class UserController : Controller
    {
       
            private readonly AppDbcontext _db;
            public UserController(AppDbcontext db)
            {
                _db = db;
            }

            public IActionResult Index()
            {
                IEnumerable<User> users = _db.User;
                return View(users);
            }

            [HttpGet]
            [RoleFilter]
            public IActionResult Create()
            {
                return View();
            }

            [HttpPost]
            public IActionResult Create(User model)
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                _db.User.Add(model);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            [HttpGet]
            public IActionResult Edit(int id)
            {
                User user = _db.User.SingleOrDefault(x => x.Id == id);
                if (user == null)
                {
                    return RedirectToAction("Index");
                }
                return View(user);

            }

            [HttpPost]
            public IActionResult Edit(User model)
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                _db.User.Update(model);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            public IActionResult Delete(int id)
            {
                //намира от базата данни етаж който има същото id като подаденото id
                User user = _db.User.SingleOrDefault(x => x.Id == id);
                if (user != null)
                {
                    _db.Remove(user);
                    _db.SaveChanges();
                }
                return RedirectToAction("Index");
            }

            public IActionResult Details(int id)
            {
                 return View();   
            }
        } 
}

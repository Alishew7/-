using Hotel.Data;
using Hotel.Filters;
using Hotel.Models;
using Microsoft.AspNetCore.Mvc;

namespace Hotel.Controllers  
{
	[AuthFilter]
	public class ClientController : Controller
    {
        private readonly AppDbcontext _db;

        public ClientController(AppDbcontext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Client> objListClients = _db.Clients;
            return View(objListClients);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Client model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            _db.Clients.Add(model);
            _db.SaveChanges();
            return RedirectToAction("Index");
		}

		[HttpGet]
        public IActionResult Edit(int id)
        {
            Client client = _db.Clients.SingleOrDefault(x => x.Id == id);
			if (client == null)
            {
                return RedirectToAction("Index");
            }
            return View(client);
        }

        [HttpPost]
        public IActionResult Edit(Client model)
        { 
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            _db.Clients.Update(model);
            _db.SaveChanges();
			return RedirectToAction("Index");
		}
		public IActionResult Delete(int id)
		{

			//намира от базата данни етаж който има същото id като подаденото id
			Client client = _db.Clients.SingleOrDefault(x => x.Id == id);
			if (client != null)
			{
				_db.Remove(client);
				_db.SaveChanges();
			}

			return RedirectToAction("Index");

		}

        public IActionResult Details(int id) {
            Client client = _db.Clients.SingleOrDefault(x => x.Id == id);
            if (client == null)
            {
                return RedirectToAction("Index");
            }

           
            return View(client);
        }
	}
}

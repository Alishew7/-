using Hotel.Data;
using Hotel.Filters;
using Hotel.Models;
using Microsoft.AspNetCore.Mvc;

namespace Hotel.Controllers
{
	[AuthFilter]
	public class RoomTypeController : Controller
	{
		private readonly AppDbcontext _db;
		public RoomTypeController(AppDbcontext db)
		{
			_db = db;
		}
		public IActionResult Index()
		{
			IEnumerable<RoomType> objListRoomTypes = _db.RoomType;
			return View(objListRoomTypes);
		}

		[HttpGet]
		[RoleFilter]
		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]

		public IActionResult Create(RoomType model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}
			_db.RoomType.Add(model);
			_db.SaveChanges();
			return RedirectToAction("Index");
		}

        [HttpGet]
		public IActionResult Edit(int id)
		{
			RoomType roomType = _db.RoomType.SingleOrDefault(x => x.Id == id);
			if (roomType == null)
			{
				return RedirectToAction("Index");
			}
			return View(roomType);
			
		}

		[HttpPost]
        public IActionResult Edit(RoomType model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            _db.RoomType.Update(model);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

		public IActionResult Delete(int id)
		{

			//намира от базата данни етаж който има същото id като подаденото id
			RoomType roomtype = _db.RoomType.SingleOrDefault(x => x.Id == id);
			if (roomtype != null)
			{
				_db.Remove(roomtype);
				_db.SaveChanges();
			}

			return RedirectToAction("Index");

		}

		public IActionResult Details(int id)
		{
			RoomType roomtype = _db.RoomType.SingleOrDefault(x => x.Id == id);
			if (roomtype == null)
			{
				return RedirectToAction("Index");
			}


			return View(roomtype);
		}
	}
}

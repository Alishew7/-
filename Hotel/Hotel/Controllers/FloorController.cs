using Hotel.Data;
using Hotel.Filters;
using Hotel.Models;
using Hotel.Models.View_Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Controllers
{
    [AuthFilter]
    public class FloorController : Controller
    {
        private readonly AppDbcontext _db;
        public FloorController(AppDbcontext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Floor> objListFloors = _db.Floors;
            return View(objListFloors);
        }

        [HttpGet]
        [RoleFilter]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Floor model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            _db.Floors.Add(model);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Floor floor = _db.Floors.SingleOrDefault(x => x.Id == id);
            if (floor == null)
            {
                return RedirectToAction("Index");
            }
            return View(floor);

        }

        [HttpPost]
        public IActionResult Edit(Floor model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            _db.Floors.Update(model);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            //намира от базата данни етаж който има същото id като подаденото id
            Floor floor = _db.Floors.SingleOrDefault(x => x.Id == id);
            if (floor != null)
            {
                _db.Remove(floor);
                _db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            List<Room> rooms = _db.Room.Include(r => r.RoomType).Include(r => r.Floor).Where(r => r.FloorId == id).ToList();

			List<FloorRoomVM> roomsVM = new List<FloorRoomVM>();
            ViewBag.FloorLvl = _db.Floors.FirstOrDefault(f => f.Id == id).Level;
			if (rooms.Count > 0)
			{
				DateTime now = DateTime.Now.Date;
				foreach (Room room in rooms)
				{
					FloorRoomVM vM = new FloorRoomVM();
					List<Reservation> reservations = _db.Reservations.Where(r => r.Room_id == room.Id).ToList();
					Reservation closestReservation = _db.Reservations
						.Include(r => r.Client)
						.Include(r => r.Room)
						.Include(r => r.ReservationStatus)
						.Where(r => r.Room_id == room.Id && r.RservationStart.Date <= now && r.RservationEnd.Date >= now)
						.OrderBy(r => r.RservationStart)
						.FirstOrDefault();
					if (closestReservation == null)
					{
						vM.Status = "Free";
					}
					else
					{
						vM.Status = closestReservation.ReservationStatus.Title.ToString();
					}
					vM.Room = room;
					roomsVM.Add(vM);
				}
			}
			
            return View(roomsVM);
        }
    }
}
